namespace SingleResponsibility.GoodDesign.Interfaces
{
    public interface IEmailSender
    {
        void SendEmail(string subject, string body, string to);
    }
}
