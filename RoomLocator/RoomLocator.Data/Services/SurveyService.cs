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

        public async Task<IEnumerable<SurveyViewModel>> GetAll()
        {
            return await _context.Surveys.ProjectTo<SurveyViewModel>(_mapper.ConfigurationProvider).ToListAsync();
        }

        public async Task<SurveyViewModel> CreateSurvey(SurveyCreateViewModel survey)
        {
            if (survey == null)
                throw new InvalidRequestException("Invalid request", "Can not create survey as survey is null.");

            var questions = _mapper.Map<IEnumerable<Question>>(survey.Questions.Where(q => !string.IsNullOrWhiteSpace(q.Text)));

            if (!questions.Any())
                throw new InvalidRequestException("Invalid request", "The survey must contain one or more questions.");

            var surveyToCreate = new Survey();
            survey.Title = surveyToCreate.Title;
            await _context.AddAsync(surveyToCreate);
            await _context.SaveChangesAsync();

            var section = _context.MazeMapSections.FirstOrDefault(x => x.Id == survey.SectionId);
            section.SurveyId = surveyToCreate.Id;
            _context.Update(section);

            foreach (var q in questions)
            {
                q.SurveyId = surveyToCreate.Id;
            }
            survey.DateTimeOfSurveyCreation = DateTime.Now;
            await _context.AddRangeAsync(questions);
            await _context.SaveChangesAsync();

            return _mapper.Map<SurveyViewModel>(surveyToCreate);
        }

        public async Task<SurveyAnswerViewModel> SubmitAnswer(SurveyAnswerSubmitViewModel viewModel)
        {
            var survey = await _context.Surveys.FirstOrDefaultAsync(x => x.Id == viewModel.SurveyId);

            if (survey == null)
                throw new InvalidRequestException("Invalid request", "Can not submit answer as survey doesn't exist.");

            if (viewModel == null)
                throw new InvalidRequestException("Invalid request", "Can not submit answer as answer is null.");

            if (!viewModel.QuestionAnswers.Any())
                throw new InvalidRequestException("Invalid request", "The answer must contain one or more question answers.");

            foreach(var qa in viewModel.QuestionAnswers)
            {
                var question = await _context.Questions.FirstOrDefaultAsync(q => q.Id == qa.QuestionId);
                if(question == null)
                    throw new InvalidRequestException("Invalid request", "All question answers must reference an existing question.");
            }

            var surveyAnswerToCreate = new SurveyAnswer { SurveyId = viewModel.SurveyId };
            await _context.AddAsync(surveyAnswerToCreate);
            await _context.SaveChangesAsync();

            var questionAnswersToCreate = viewModel.QuestionAnswers.Select(x => new QuestionAnswer
            {
                QuestionId = x.QuestionId,
                SurveyAnswerId = surveyAnswerToCreate.Id,
                Text = x.Text,
                Score = x.Score
            });
            viewModel.DateTimeOfAnswers = DateTime.Now;
            await _context.AddRangeAsync(questionAnswersToCreate);
            await _context.SaveChangesAsync();

            surveyAnswerToCreate.QuestionAnswers = questionAnswersToCreate;

            return _mapper.Map<SurveyAnswerViewModel>(surveyAnswerToCreate);
        }
    }
}
