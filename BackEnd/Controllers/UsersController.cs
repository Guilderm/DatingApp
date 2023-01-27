using API.DTOs;
using API.Entities;
using API.Extensions;
using API.Helpers;
using API.Interfaces;

using AutoMapper;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Authorize]
public class UsersController : BaseApiController
{
	private readonly IMapper _mapper;
	private readonly IPhotoService _photoService;
	private readonly IUnitOfWork _uow;
	public UsersController(IUnitOfWork uow, IMapper mapper,
		IPhotoService photoService)
	{
		_uow = uow;
		_photoService = photoService;
		_mapper = mapper;
	}

	[HttpGet]
	public async Task<ActionResult<PagedList<MemberDto>>> GetUsers([FromQuery] UserParams userParams)
	{
		AppUser currentUser = await _uow.UserRepository.GetUserByUsernameAsync(User.GetUsername());
		userParams.CurrentUsername = currentUser.UserName;

		if (string.IsNullOrEmpty(userParams.Gender))
		{
			userParams.Gender = currentUser.Gender == "male" ? "female" : "male";
		}

		PagedList<MemberDto> users = await _uow.UserRepository.GetMembersAsync(userParams);

		Response.AddPaginationHeader(new PaginationHeader(users.CurrentPage, users.PageSize,
			users.TotalCount, users.TotalPages));

		return Ok(users);
	}

	[HttpGet("{username}")]

	public async Task<ActionResult<MemberDto>> GetUser(string username) => await _uow.UserRepository.GetMemberAsync(username);

	[HttpPut]
	public async Task<ActionResult> UpdateUser(MemberUpdateDto memberUpdateDto)
	{
		AppUser user = await _uow.UserRepository.GetUserByUsernameAsync(User.GetUsername());

		if (user == null)
		{
			return NotFound();
		}

		_ = _mapper.Map(memberUpdateDto, user);

		if (await _uow.Complete())
		{
			return NoContent();
		}

		return BadRequest("Failed to update user");
	}

	[HttpPost("add-photo")]
	public async Task<ActionResult<PhotoDto>> AddPhoto(IFormFile file)
	{
		AppUser user = await _uow.UserRepository.GetUserByUsernameAsync(User.GetUsername());

		if (user == null)
		{
			return NotFound();
		}

		CloudinaryDotNet.Actions.ImageUploadResult result = await _photoService.AddPhotoAsync(file);

		if (result.Error != null)
		{
			return BadRequest(result.Error.Message);
		}

		var photo = new Photo
		{
			Url = result.SecureUrl.AbsoluteUri,
			PublicId = result.PublicId
		};

		if (user.Photos.Count == 0)
		{
			photo.IsMain = true;
		}

		user.Photos.Add(photo);

		if (await _uow.Complete())
		{
			return CreatedAtAction(nameof(GetUser),
				new { username = user.UserName }, _mapper.Map<PhotoDto>(photo));
		}

		return BadRequest("Problem adding photo");
	}

	[HttpPut("set-main-photo/{photoId}")]
	public async Task<ActionResult> SetMainPhoto(int photoId)
	{
		AppUser user = await _uow.UserRepository.GetUserByUsernameAsync(User.GetUsername());

		if (user == null)
		{
			return NotFound();
		}

		Photo photo = user.Photos.FirstOrDefault(x => x.Id == photoId);

		if (photo == null)
		{
			return NotFound();
		}

		if (photo.IsMain)
		{
			return BadRequest("this is already your main photo");
		}

		Photo currentMain = user.Photos.FirstOrDefault(x => x.IsMain);
		if (currentMain != null)
		{
			currentMain.IsMain = false;
		}

		photo.IsMain = true;

		if (await _uow.Complete())
		{
			return NoContent();
		}

		return BadRequest("Problem setting the main photo");
	}

	[HttpDelete("delete-photo/{photoId}")]
	public async Task<ActionResult> DeletePhoto(int photoId)
	{
		AppUser user = await _uow.UserRepository.GetUserByUsernameAsync(User.GetUsername());

		Photo photo = user.Photos.FirstOrDefault(x => x.Id == photoId);

		if (photo == null)
		{
			return NotFound();
		}

		if (photo.IsMain)
		{
			return BadRequest("You cannot delete your main photo");
		}

		if (photo.PublicId != null)
		{
			CloudinaryDotNet.Actions.DeletionResult result = await _photoService.DeletePhotoAsync(photo.PublicId);
			if (result.Error != null)
			{
				return BadRequest(result.Error.Message);
			}
		}

		_ = user.Photos.Remove(photo);

		if (await _uow.Complete())
		{
			return Ok();
		}

		return BadRequest("Problem deleting photo");
	}
}