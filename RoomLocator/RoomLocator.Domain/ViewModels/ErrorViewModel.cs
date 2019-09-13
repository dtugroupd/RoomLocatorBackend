using System;
using Shared;

namespace RoomLocator.Domain.ViewModels
{
    public class ErrorViewModel
    {
        public string Title { get; set; }
        public string Message { get; set; }

        public ErrorViewModel() { }

        public ErrorViewModel(string title, string message)
        {
            Title = title;
            Message = message;
        }

        public ErrorViewModel(Exception ex)
        {
            if (ex is BaseException bex)
            {
                Title = bex.Title;
                Message = bex.Message;
            }
            else
            {
                Title = "Unexpected Error";
                Message = ex.Message;
            }
        }
    }
}