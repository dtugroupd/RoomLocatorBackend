using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using RoomLocator.Data.Config;
using RoomLocator.Domain.Models;
using RoomLocator.Domain.ViewModels;
using Shared;

namespace RoomLocator.Data.Services
{
    public class SurveyService : BaseService
    {
        public SurveyService(RoomLocatorContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public async Task<SurveyViewModel> Get(int id)
        {
            return await _context.Surveys.ProjectTo<SurveyViewModel>(_mapper.ConfigurationProvider).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<SurveyViewModel> CreateSurvey(SurveyCreateViewModel survey)
        {
            if (survey == null)
                throw new InvalidRequestException("Invalid request", "Can not create survey as survey is null.");

            var questions = _mapper.Map<IEnumerable<Question>>(survey.Questions);

            if (!questions.Any())
                throw new InvalidRequestException("Invalid request", "The survey must contain one or more questions.");

            var surveyToCreate = new Survey();
            await _context.AddAsync(surveyToCreate);
            await _context.SaveChangesAsync();

            var section = _context.MazeMapSections.FirstOrDefault(x => x.Id == survey.SectionId);
            section.SurveyId = surveyToCreate.Id;
            _context.Update(section);

            foreach (var q in questions)
            {
                q.SurveyId = surveyToCreate.Id;
            }

            await _context.AddRangeAsync(questions);
            await _context.SaveChangesAsync();

            return _mapper.Map<SurveyViewModel>(surveyToCreate);
        }
    }
}
