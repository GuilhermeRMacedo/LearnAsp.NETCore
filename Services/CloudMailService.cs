using System.Diagnostics;

namespace hello_world_web.Services
{
    public class CloudMailService: IMailService
    {
        private string _mailTo = "admin@mycompany.com";
        private string _mailFrom = "noreply@mycompany.com";

        public void Send(string subject, string message)
        {
            Debug.WriteLine($"Mail from {_mailFrom} to {_mailTo}, eith ClouldMailservice");
            Debug.WriteLine($"subject: {subject}");
            Debug.WriteLine($"message: {message}");
        }
    }
}