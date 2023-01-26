using API.DTOs;
using API.Entities;
using API.Extensions;
using API.Helpers;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class LikesController : BaseApiController
{
    private readonly IUserRepository _userRepository;
    private readonly ILikesRepository _likesRepository;

    public LikesController(IUserRepository userRepository, ILikesRepository likesRepository)
    {
        _likesRepository = likesRepository;
        _userRepository = userRepository;
    }

    [HttpPost("{username}")]
    public async Task<ActionResult> AddLike(string username)
    {
        int sourceUserId = User.GetUserId();
        AppUser likedUser = await _userRepository.GetUserByUsernameAsync(username);
        AppUser sourceUser = await _likesRepository.GetUserWithLikes(sourceUserId);

        if (likedUser == null)
        {
            return NotFound();
        }

        if (sourceUser.UserName == username)
        {
            return BadRequest("You cannot like yourself");
        }

        UserLike userLike = await _likesRepository.GetUserLike(sourceUserId, likedUser.Id);

        if (userLike != null)
        {
            return BadRequest("You already like this user");
        }

        userLike = new UserLike
        {
            SourceUserId = sourceUserId,
            TargetUserId = likedUser.Id
        };

        sourceUser.LikedUsers.Add(userLike);

        return await _userRepository.SaveAllAsync() ? Ok() : BadRequest("Failed to like user");
    }

    [HttpGet]
    public async Task<ActionResult<PagedList<LikeDto>>> GetUserLikes([FromQuery] LikesParams likesParams)
    {
        likesParams.UserId = User.GetUserId();

        PagedList<LikeDto> users = await _likesRepository.GetUserLikes(likesParams);

        Response.AddPaginationHeader(new PaginationHeader(users.CurrentPage,
            users.PageSize, users.TotalCount, users.TotalPages));

        return Ok(users);
    }
}