using System.Collections.Generic;

namespace Games.Application.ViewModel.Output
{
    public class ValidateRequiredFieldsViewModelOutput
    {
        public ValidateRequiredFieldsViewModelOutput(IEnumerable<string> errors) => Errors = errors;

        public IEnumerable<string> Errors { get; private set; }
    }
}