﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using RoomLocator.Data.Config;
using RoomLocator.Domain;
using RoomLocator.Domain.InputModels;
using RoomLocator.Domain.Models;
using RoomLocator.Domain.ViewModels;
using Shared;

namespace RoomLocator.Data.Services
{
    /// <summary>
    ///     <author>Hamed Kadkhodaie, s083485</author>
    /// </summary>
    /// 
    public class FeedbackService : BaseService
    {
        public FeedbackService(RoomLocatorContext context, IMapper mapper) : base(context, mapper) { }

        public async Task<FeedbackViewModel> Get(string id)
        {
            return await _context.Feedbacks.ProjectTo<FeedbackViewModel>(_mapper.ConfigurationProvider).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<FeedbackViewModel> GetCurrentUserFeedback(string userId)
        {
            var feedback = await _context.Feedbacks
                .ProjectTo<FeedbackViewModel>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(x => x.UserId == userId && (DateTime.Now - x.TimeStamp).TotalHours < 2);

            return feedback;
        }

        //public async Task<FeedbackViewModel> GetByDownvote(bool downvote)
        //{
        //    var downvotes = await _context.Feedbacks
        //        .Include(x => x.Vote == true)
        //        .ProjectTo<FeedbackViewModel>(_mapper.ConfigurationProvider)
        //        .FirstOrDefaultAsync(x => x.downvote == downvote);

        //    return downvotes;
        //}

        //public async Task<FeedbackViewModel> GetByUpvote(bool upvote)
        //{
        //    var upvotes = await _context.Feedbacks
        //        .Include(x => x.Vote == false)
        //        .ProjectTo<FeedbackViewModel>(_mapper.ConfigurationProvider)
        //        .FirstOrDefaultAsync(x => x.upvote == upvote);

        //    return upvotes;
        //}


        public async Task<FeedbackViewModel> Create(FeedbackInputModel input)
        {
            if (input == null)
            {
                throw new InvalidCastException("Input should not be null");
            }

            var feedback = _mapper.Map<Feedback>(input);
            await _context.Feedbacks.AddAsync(feedback);
            await _context.SaveChangesAsync();
            return _mapper.Map<FeedbackViewModel>(feedback);
        }

    }

}