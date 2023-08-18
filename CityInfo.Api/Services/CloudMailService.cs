namespace CityInfo.Api.Services
{
    public class CloudMailService : IMailService
    {
        private readonly string _mailTo = String.Empty;
        private readonly string _mailFrom = String.Empty;


        public CloudMailService(IConfiguration configuration)
        {
            _mailTo = configuration["mailSettings:mailTo"];
            _mailFrom = configuration["mailSettings:mailFrom"];
        }

       

        public void Send(string subject, string message)
        {
            //send mail - output to console window

            Console.WriteLine($"mail from {_mailFrom} to {_mailTo}, " +
                $"with {nameof(CloudMailService)}");

            Console.WriteLine($"Subject: {subject}");
            Console.WriteLine($"Message {message}");


        }

    }
}
