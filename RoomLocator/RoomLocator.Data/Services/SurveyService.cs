using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using CsvHelper;
using Microsoft.EntityFrameworkCore;
using RoomLocator.Data.Config;
using RoomLocator.Domain.CsvModels;
using RoomLocator.Domain.InputModels;
using RoomLocator.Domain.Models;
using RoomLocator.Domain.ViewModels;
using Shared;

namespace RoomLocator.Data.Services
{
    /// <summary>
    ///     <author>Thomas Lien Christensen, s165242</author>
    /// </summary>
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
        /// <summary>
        ///     <author>Hadi Horani, s144885</author>
        ///     <author>Thomas Lien Christensen, s165242</author>
        /// </summary>
        public async Task<SurveyViewModel> CreateSurvey(SurveyInputModel viewModel)
        {
            if (viewModel == null)
                throw new InvalidRequestException("Invalid request", "Can not create survey as survey is null.");

            if (viewModel.Title == null)
                throw new InvalidRequestException("Invalid request", "Can not create survey without a title.");

            var questions = _mapper.Map<IEnumerable<Question>>(viewModel.Questions.Where(q => !string.IsNullOrWhiteSpace(q.Text)));

            if (!questions.Any())
                throw new InvalidRequestException("Invalid request", "The survey must contain one or more questions.");

            var surveyToCreate = new Survey
            {
                Title = viewModel.Title,
                Description = viewModel.Description,
                CreatedDate = DateTime.Now
            };

            await _context.AddAsync(surveyToCreate);
            await _context.SaveChangesAsync();

            var section = _context.MazeMapSections.FirstOrDefault(x => x.Id == viewModel.SectionId);
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

        public async Task<SurveyAnswerViewModel> SubmitAnswer(SurveyAnswerInputModel inputModel)
        {
            var survey = await _context.Surveys.FirstOrDefaultAsync(x => x.Id == inputModel.SurveyId);

            if (survey == null)
                throw new InvalidRequestException("Invalid request", "Can not submit answer as survey doesn't exist.");

            if (inputModel == null)
                throw new InvalidRequestException("Invalid request", "Can not submit answer as answer is null.");

            if (!inputModel.QuestionAnswers.Any())
                throw new InvalidRequestException("Invalid request", "The answer must contain one or more question answers.");

            foreach(var qa in inputModel.QuestionAnswers)
            {
                var question = await _context.Questions.FirstOrDefaultAsync(q => q.Id == qa.QuestionId);
                if(question == null)
                    throw new InvalidRequestException("Invalid request", "All question answers must reference an existing question.");
            }

            var surveyAnswerToCreate = new SurveyAnswer {
                SurveyId = inputModel.SurveyId,
                Comment = inputModel.Comment,
                TimeStamp = DateTime.Now
            };

            await _context.AddAsync(surveyAnswerToCreate);
            await _context.SaveChangesAsync();

            var questionAnswersToCreate = inputModel.QuestionAnswers.Select(x => new QuestionAnswer
            {
                QuestionId = x.QuestionId,
                SurveyAnswerId = surveyAnswerToCreate.Id,
                Text = x.Text,
                Score = x.Score
            });
            
            await _context.AddRangeAsync(questionAnswersToCreate);
            await _context.SaveChangesAsync();

            surveyAnswerToCreate.QuestionAnswers = questionAnswersToCreate;

            return _mapper.Map<SurveyAnswerViewModel>(surveyAnswerToCreate);
        }

        public async Task<MemoryStream> GetSurveyAnswersCsvMemoryStream(int surveyId)
        {
            var survey = _context.Surveys
                .Include(s => s.SurveyAnswers)
                    .ThenInclude(s => s.QuestionAnswers)
                .FirstOrDefault(s => s.Id == surveyId);

            var surveyAnswers = _mapper.Map<List<SurveyAnswerCsvModel>>(survey.SurveyAnswers);

            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            var csvWriter = new CsvWriter(writer);
            csvWriter.Configuration.HasHeaderRecord = false;

            // Explicitly define comma separator
            csvWriter.WriteField("SEP=,");
            await csvWriter.NextRecordAsync();

            // Write header fields
            csvWriter.WriteField("SurveyId");
            csvWriter.WriteField("TimeStamp");
            csvWriter.WriteField("Comment");

            foreach(var question in surveyAnswers[0].QuestionAnswers)
            {
                csvWriter.WriteField($"Question {question.QuestionId} score");
            }

            await csvWriter.NextRecordAsync();

            // Write data entries
            foreach (var answer in surveyAnswers)
            {
                csvWriter.WriteField(answer.SurveyId);
                csvWriter.WriteField(answer.TimeStamp);
                csvWriter.WriteField(answer.Comment);

                foreach (var question in answer.QuestionAnswers)
                {
                    csvWriter.WriteField(question.Score);
                }

                await csvWriter.NextRecordAsync();
            }

            await writer.FlushAsync();
            stream.Position = 0;
            return stream;
        }
    }
}

