using SafePass.Data;

namespace SafePass.Services
{
    public static class SecurityQuestionChainBuilder
    {
        public static ISecurityQuestionHandler BuildChain(List<SecurityQuestionRequest> requests)
        {
            ISecurityQuestionHandler firstHandler = null;
            ISecurityQuestionHandler currentHandler = null;

            foreach (var request in requests)
            {
                var handler = new SecurityQuestionHandler();

                if (firstHandler == null)
                {
                    firstHandler = handler;
                }
                else
                {
                    currentHandler.SetNext(handler);
                }

                currentHandler = handler;
            }

            return firstHandler;
        }
    }
}
