using CompanionApp.ModelsDTO;
using Microsoft.AspNetCore.Mvc;
using CompanionApp.Services.Contracts;
using CompanionApp.Exceptions.PostExceptions;
using CompanionApp.Exceptions.LikesExceptions;
using CompanionApp.Exceptions.ProfileExceptions;

namespace CompanionApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LikesController : ControllerBase
    {
        readonly ILikesService _likeService;

        public LikesController(ILikesService likeService)
        {
            _likeService = likeService;
        }
        

        [HttpGet("{postID}")]
        public async Task<ActionResult<IEnumerable<LikeDTOUsers>>> GetPostLikes       (Guid postID)
        {
            try
            {
                return Ok(await _likeService.GetPostLikes(postID));
            }
            catch (Exception ex) when (ex is NoLikesFoundException || ex is PostNotFoundException)
            {
                return NotFound(ex.Message);
            }
            catch (Exception)
            {
                throw;
            }
        }


        [HttpGet("counter/{postID}")]
        public async Task<ActionResult<int>>                       GetPostLikesCounter(Guid postID)
        {
            try
            {
                return Ok(await _likeService.GetPostLikesCount(postID));
            }
            catch (Exception ex) when (ex is NoLikesFoundException || ex is PostNotFoundException)
            {
                return NotFound(ex.Message);
            }
            catch (Exception)
            {
                throw;
            }
        }


        [HttpPost]
        public async Task<ActionResult<LikeDTO>>                   PostLike           (LikePOSTDTO like)
        {
            try
            {
                LikeDTO newlike = await _likeService.LikePost(like);
                return newlike;
            }
            catch (Exception ex) when (ex is ProfileNotFoundException || ex is PostNotFoundException)
            {
                return NotFound(ex.Message);
            }
            catch(ProfileAlreadyLikedPostException ex)
            {
                return Conflict(ex.Message);
            }
            catch(Exception)
            {
                throw;
            }
        }


        [HttpDelete("{postID}/{userID}")]
        public async Task<IActionResult>                           DeleteLike         (Guid postID, Guid userID)
        {
            try
            {
                await _likeService.UnlikePost(postID, userID);
                return NoContent();
            }
            catch (Exception ex) when (ex is ProfileNotFoundException || ex is PostNotFoundException || ex is LikeNotFoundException)
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
