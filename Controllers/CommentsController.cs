using CompanionApp.ModelsDTO;
using Microsoft.AspNetCore.Mvc;
using CompanionApp.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using CompanionApp.Extensions;

namespace CompanionApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
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


        [HttpPost("{postID}")]
        public async Task<ActionResult<CommentQueryDTO>>              PostComment         (CommentPOSTCommandDTO comment, Guid postID)
        {
            CommentQueryDTO newComment = await _commentsService.AddComment(comment, postID, HttpContext.GetUserID());
            return CreatedAtAction("GetComment", new { commentID = newComment.Id }, newComment);
        }


        [HttpPut("post/{postID}/comment/{commentID}")]
        public async Task<IActionResult>                              PutComment          (CommentPOSTCommandDTO comment, Guid commentID, Guid postID)
        {
            await _commentsService.EditComment(comment, commentID, postID, HttpContext.GetUserID());
            return NoContent();
        }

        
        [HttpDelete("{commentID}")]
        public async Task<IActionResult>                              DeleteComment       (Guid commentID)
        {
            await _commentsService.DeleteComment(commentID, HttpContext.GetUserID());
            return NoContent();
        }
    }
}
