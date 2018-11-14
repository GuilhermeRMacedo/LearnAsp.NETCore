using System.Diagnostics;

namespace hello_world_web.Services
{
    public class CloudMailService: IMailService
    {
        private string _mailTo = Startup.Configuration["mailSettings:mailToAddress"];
        private string _mailFrom = Startup.Configuration["mailSettings:mailFromAddress"];

        public void Send(string subject, string message)
        {
            Debug.WriteLine($"Mail from {_mailFrom} to {_mailTo}, with ClouldMailservice");
            Debug.WriteLine($"subject: {subject}");
            Debug.WriteLine($"message: {message}");
        }
    }
}