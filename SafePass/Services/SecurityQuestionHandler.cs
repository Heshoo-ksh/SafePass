using SafePass.Data;

namespace SafePass.Services
{
    public class SecurityQuestionHandler : ISecurityQuestionHandler
    {
        private ISecurityQuestionHandler _nextHandler;

        public void SetNext(ISecurityQuestionHandler nextHandler)
        {
            _nextHandler = nextHandler;
        }

        public bool Handle(SecurityQuestionRequest request)
        {
            if (!BCrypt.Net.BCrypt.Verify(request.SubmittedAnswer, request.ExpectedAnswerHash))
            {
                Console.WriteLine("Validation failed for a security question.");
                return false;
            }

            Console.WriteLine("Validation succeeded for a security question.");
            return _nextHandler?.Handle(request) ?? true;
        }
    }
}
