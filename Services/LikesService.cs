﻿using CompanionApp.Models;
using CompanionApp.ModelsDTO;
using CompanionApp.Extensions;
using Microsoft.EntityFrameworkCore;
using CompanionApp.Services.Contracts;
using EntityFramework.Exceptions.Common;
using CompanionApp.Exceptions.LikesExceptions;
using CompanionApp.Exceptions.PostExceptions;
using CompanionApp.Exceptions.ProfileExceptions;

namespace CompanionApp.Services
{
    public class LikesService : ILikesService
    {
        readonly CompanionAppDBContext _context;
        readonly DbSet<Like>           _dbSetLikes;
        readonly DbSet<Profile>        _dbSetProfile;
        readonly DbSet<Post>           _dbSetPost;

        public LikesService(CompanionAppDBContext context)
        {
            _context = context;
            _dbSetLikes = _context.Likes;
            _dbSetProfile = _context.Profiles;
            _dbSetPost = _context.Posts;
        }

        public async Task<IEnumerable<LikeDTOUsers>> GetPostLikes     (Guid postID)
        {
            if (!await _dbSetPost.PostExists(postID.ToString()))
            {
                throw new PostNotFoundException();
            }

            IEnumerable<LikeDTOUsers> likes = await _dbSetLikes
                .Include(like => like.User)
                .Where  (like => like.PostId == postID)
                .Select (like => like.ToLikeDTOUsers())
                .ToListAsync();

            if (!likes.Any())
            {
                throw new NoLikesFoundException();
            }
            return likes;
        }
        public async Task<int>                       GetPostLikesCount(Guid postID)
        {
            if (!await _dbSetPost.PostExists(postID.ToString()))
            {
                throw new PostNotFoundException();
            }

            int likes = await _dbSetLikes
                .Include(like => like.User)
                .Where(like => like.PostId == postID)
                .CountAsync();

            if (likes == 0)
            {
                throw new NoLikesFoundException();
            }
            return likes;
        }
        public async Task<LikeDTO>                   LikePost         (LikePOSTDTO like)
        {
            try
            {
                if (!await _dbSetPost.PostExists(like.PostId))
                {
                    throw new PostNotFoundException();
                }
                if (!await _dbSetProfile.ProfileExists(like.UserId))
                {
                    throw new ProfileNotFoundException();
                }

                Like newlike = like.ToLike();
                _dbSetLikes.Add(newlike);
                await _context.SaveChangesAsync();
                return newlike.ToLikeDTO();
            }
            catch (UniqueConstraintException)
            {
                throw new ProfileAlreadyLikedPostException();
            }
        }
        public async Task                            UnlikePost       (Guid postID, Guid userID)
        {
            if (!await _dbSetPost.PostExists(postID.ToString()))
            {
                throw new PostNotFoundException();
            }
            if (!await _dbSetProfile.ProfileExists(userID))
            {
                throw new ProfileAlreadyExistsException();
            }
            if (!await _dbSetLikes.LikeExists(postID, userID))
            {
                throw new LikeNotFoundException();
            }

            _dbSetLikes.Remove(new Like() { PostId = postID, UserId = userID });
            await _context.SaveChangesAsync();
        }
    }
}
