namespace SendEmailAPI.Helpers
{
    public interface IEmailSender
    {
        Task<string> SendEmailAsync(string recipientEmail,string recipientFirstName,string Link);
    }
}
