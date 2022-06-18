using InOut.API.Models;

namespace InOut.API.Builders
{
    public class ResponseModelBuilder
    {
        private object? _data;
        private string _message = string.Empty;
        private bool _success;

        public ResponseModelBuilder WithData(object? data)
        {
            _data = data;
            return this;
        }

        public ResponseModelBuilder WithMessage(string message)
        {
            _message = message;
            return this;
        }

        public ResponseModelBuilder WithSuccess(bool success)
        {
            _success = success;
            return this;
        }

        public ResponseModel Build()
        {
            return new ResponseModel
            {
                Data = _data,
                Message = _message,
                Success = _success,
            };
        }
    }
}
