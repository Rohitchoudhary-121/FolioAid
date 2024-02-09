namespace Application.Interface
{
    public interface IEmailTemplate
    {
        public void SendActivationLink(string recipientEmail, string activationToken);
        public void SendResetPasswordLink(string recipientEmail,string token);
    }
}
