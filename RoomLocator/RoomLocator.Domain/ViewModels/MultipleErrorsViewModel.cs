using System;
using System.Collections.Generic;
using System.Linq;
using Shared;

namespace RoomLocator.Domain.ViewModels
{
    public class MultipleErrorsViewModel
    {
        public IEnumerable<ErrorViewModel> Errors { get; }

        public MultipleErrorsViewModel() { }
        public MultipleErrorsViewModel(params ErrorViewModel[] errors) => Errors = errors;
        public MultipleErrorsViewModel(IEnumerable<ErrorViewModel> errors) => Errors = errors;

        public MultipleErrorsViewModel(params string[] errors) =>
            Errors = errors.Select(x => new ErrorViewModel("Unexpected Error", x));

        public MultipleErrorsViewModel(IEnumerable<BaseException> errors) =>
            Errors = errors.Select(x => new ErrorViewModel(x));

        public MultipleErrorsViewModel(IEnumerable<Exception> errors) =>
            Errors = errors.Select(x => new ErrorViewModel(x));
    }
}