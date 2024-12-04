using SafePass.Data;
public interface ISecurityQuestionHandler
{
    void SetNext(ISecurityQuestionHandler nextHandler);
    bool Handle(SecurityQuestionRequest request);
}
