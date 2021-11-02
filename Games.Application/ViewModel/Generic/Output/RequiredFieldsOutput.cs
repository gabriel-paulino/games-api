using System.Collections.Generic;

namespace Games.Application.ViewModel.Generic.Output
{
    public class RequiredFieldsOutput
    {
        public RequiredFieldsOutput(IEnumerable<string> errors) => Errors = errors;

        public IEnumerable<string> Errors { get; private set; }
    }
}