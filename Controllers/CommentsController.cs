﻿using CompanionApp.ModelsDTO;
using Microsoft.AspNetCore.Mvc;
using CompanionApp.Services.Contracts;


namespace CompanionApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        readonly ICommentsService _commentsService;

        public CommentsController(ICommentsService CommentsService)
        {
            _commentsService = CommentsService;
        }

        [HttpGet("{commentID}")]
        public async Task<ActionResult<CommentQueryDTO>>              GetComment          (Guid commentID)
        {
            return Ok(await _commentsService.GetComment(commentID));
        }

        
        [HttpGet("post/{postID}")]
        public async Task<ActionResult<IEnumerable<CommentQueryDTO>>> GetPostComments     (Guid postID)
        {
            return Ok(await _commentsService.GetPostComments(postID));
        }

        
        [HttpGet("post/{postID}/count")]
        public async Task<ActionResult<int>>                          GetPostCommentsCount(Guid postID)
        {
            return Ok(await _commentsService.GetPostCommentsCount(postID));
        }

        
        [HttpPost("{postID}/{userID}")]
        public async Task<ActionResult<CommentQueryDTO>>              PostComment         (Guid postID, Guid userID, CommentCommandDTO comment)
        {
            CommentQueryDTO newComment = await _commentsService.AddComment(postID, userID, comment);
            return CreatedAtAction("GetComment", new { commentID = newComment.Id }, newComment);
        }

        
        [HttpPut("{postID}/{commentID}/{userID}")]
        public async Task<IActionResult>                              PutComment          (Guid commentID, Guid postID, Guid userID, CommentCommandDTO comment)
        {
            await _commentsService.EditComment(commentID, postID, userID, comment);
            return NoContent();
        }

        
        [HttpDelete("{commentID}")]
        public async Task<IActionResult>                              DeleteComment       (Guid commentID)
        {
            await _commentsService.DeleteComment(commentID);
            return NoContent();
        }
    }
}
