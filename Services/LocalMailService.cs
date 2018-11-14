using System.Diagnostics;
using Microsoft.Extensions.Logging;

namespace hello_world_web.Services
{
    public class LocalMailService: IMailService
    {
        private ILogger<LocalMailService> _logger;

        public LocalMailService(ILogger<LocalMailService> logger)
        {
            _logger = logger;
        }

        private string _mailTo = Startup.Configuration["mailSettings:mailToAddress"];
        private string _mailFrom = Startup.Configuration["mailSettings:mailFromAddress"];

        public void Send(string subject, string message)
        {
            Debug.WriteLine($"Mail from {_mailFrom} to {_mailTo}, with LocalMailservice");
            Debug.WriteLine($"subject: {subject}");
            Debug.WriteLine($"message: {message}");

            _logger.LogInformation($"Mail from {_mailFrom} to {_mailTo}, with LocalMailservice");
            _logger.LogInformation($"subject: {subject}");
            _logger.LogInformation($"message: {message}");
        }

    }
}