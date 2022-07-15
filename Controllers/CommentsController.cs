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
        public async Task<ActionResult<CommentQueryDTO>>              GetComment          (Guid commentID, CancellationToken cancellationToken)
        {
            return Ok(await _commentsService.GetComment(commentID, cancellationToken));
        }

        
        [HttpGet("post/{postID}")]
        public async Task<ActionResult<IEnumerable<CommentQueryDTO>>> GetPostComments     (Guid postID,    CancellationToken cancellationToken)
        {
            return Ok(await _commentsService.GetPostComments(postID, cancellationToken));
        }

        
        [HttpGet("post/{postID}/count")]
        public async Task<ActionResult<int>>                          GetPostCommentsCount(Guid postID,    CancellationToken cancellationToken)
        {
            return Ok(await _commentsService.GetPostCommentsCount(postID, cancellationToken));
        }


        [HttpPost("{postID}")]
        public async Task<ActionResult<CommentQueryDTO>>              PostComment         (CommentPOSTCommandDTO comment, Guid postID, CancellationToken cancellationToken)
        {
            CommentQueryDTO newComment = await _commentsService.AddComment(comment, postID, HttpContext.GetUserID(), cancellationToken);
            return CreatedAtAction("GetComment", new { commentID = newComment.Id }, newComment);
        }


        [HttpPut("post/{postID}/comment/{commentID}")]
        public async Task<IActionResult>                              PutComment          (CommentPOSTCommandDTO comment, Guid commentID, Guid postID, CancellationToken cancellationToken)
        {
            await _commentsService.EditComment(comment, commentID, postID, HttpContext.GetUserID(), cancellationToken);
            return NoContent();
        }

        
        [HttpDelete("{commentID}")]
        public async Task<IActionResult>                              DeleteComment       (Guid commentID, CancellationToken cancellationToken)
        {
            await _commentsService.DeleteComment(commentID, HttpContext.GetUserID(), cancellationToken);
            return NoContent();
        }
    }
}
