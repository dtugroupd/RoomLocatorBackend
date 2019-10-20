using AutoMapper;
using RoomLocator.Domain.Models;
using RoomLocator.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace RoomLocator.Domain.Config.AutoMapperConfigs
{
    public class SurveyAnswerMapping : Profile
    {
        public SurveyAnswerMapping()
        {
            CreateMap<SurveyAnswerViewModel, SurveyAnswer>().ReverseMap();
            CreateMap<SurveyAnswerSubmitViewModel, SurveyAnswer>().ReverseMap();
        }
    }
}
