using System;
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

        public async Task<FeedbackViewModel> GetByDownvote(bool downvote)
        {
            var downvotes = await _context.Feedbacks
                .Include(x => x.Downvote)
                .ProjectTo<FeedbackViewModel>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(x => x.downvote == downvote);

            if (downvotes == null) return null;

            return downvotes;
        }

        public async Task<FeedbackViewModel> GetByUpvote(bool upvote)
        {
            var upvotes = await _context.Feedbacks
                .Include(x => x.Upvote)
                .ProjectTo<FeedbackViewModel>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(x => x.upvote == upvote);

            if (upvotes == null) return null;

            return upvotes;
        }


        public async Task<FeedbackViewModel> Create(FeedbackInputModel input)
        {
            if (input == null)
            {
                throw new InvalidCastException("Input should not be null");
            }

            var feedbacks = _mapper.Map<Feedback>(input);
            await _context.Feedbacks.AddAsync(feedbacks);
            await _context.SaveChangesAsync();
            return _mapper.Map<FeedbackViewModel>(feedbacks);
        }

    }

}