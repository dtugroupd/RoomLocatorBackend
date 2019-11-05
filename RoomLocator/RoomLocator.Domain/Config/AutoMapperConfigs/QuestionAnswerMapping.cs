using AutoMapper;
using RoomLocator.Domain.Models;
using RoomLocator.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace RoomLocator.Domain.Config.AutoMapperConfigs
{
    /// <summary>
    ///     <author>Thomas Lien Christensen, s165242</author>
    /// </summary>
    public class QuestionAnswerMapping : Profile
    {
        public QuestionAnswerMapping()
        {
            CreateMap<QuestionAnswerViewModel, QuestionAnswer>().ReverseMap();
        }
    }
}
