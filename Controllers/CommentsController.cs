using CompanionApp.Models;
using CompanionApp.ModelsDTO;
using Microsoft.AspNetCore.Mvc;
using CompanionApp.Services.Contracts;
using CompanionApp.Exceptions.CommentExceptions;
using CompanionApp.Exceptions.PostExceptions;
using CompanionApp.Exceptions.ProfileExceptions;

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
            try
            {
                return Ok(await _commentsService.GetComment(commentID));
            }
            catch (CommentNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception)
            {
                throw;
            }
        }

        
        [HttpGet("post/{postID}")]
        public async Task<ActionResult<IEnumerable<CommentQueryDTO>>> GetPostComments     (Guid postID)
        {
            try
            {
                return Ok(await _commentsService.GetPostComments(postID));
            }
            catch (Exception ex)
                when (ex is PostNotFoundException || ex is NoCommentsFoundException)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                throw;
            }
        }

        
        [HttpGet("post/{postID}/count")]
        public async Task<ActionResult<int>>                          GetPostCommentsCount(Guid postID)
        {
            try
            {
                return Ok(await _commentsService.GetPostCommentsCount(postID));
            }
            catch (Exception ex)
                when (ex is PostNotFoundException || ex is NoCommentsFoundException)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                throw;
            }
        }

        
        [HttpPost("{postID}/{userID}")]
        public async Task<ActionResult<CommentQueryDTO>>              PostComment         (Guid postID, Guid userID, CommentCommandDTO comment)
        {
            try
            {
                CommentQueryDTO newComment = await _commentsService.AddComment(postID, userID, comment);
                return CreatedAtAction("GetComment", new { commentID = newComment.Id }, newComment);
            }
            catch (Exception ex)
                when (ex is PostNotFoundException || ex is ProfileNotFoundException)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                throw;
            }
        }

        
        [HttpPut("{postID}/{commentID}/{userID}")]
        public async Task<IActionResult>                              PutComment          (Guid commentID, Guid postID, Guid userID, CommentCommandDTO comment)
        {
            try
            {
                await _commentsService.EditComment(commentID, postID, userID, comment);
                return NoContent();
            }
            catch (Exception ex)
                when (ex is CommentNotFoundException
                    || ex is PostNotFoundException
                    || ex is ProfileNotFoundException
                )
            {
                return BadRequest();
            }
            catch (Exception)
            {
                throw;
            }
        }

        
        [HttpDelete("{commentID}")]
        public async Task<IActionResult>                              DeleteComment       (Guid commentID)
        {
            try
            {
                await _commentsService.DeleteComment(commentID);
                return NoContent();
            }
            catch (CommentNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
