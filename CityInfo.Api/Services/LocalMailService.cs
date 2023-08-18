namespace CityInfo.Api.Services
{
    public class LocalMailService : IMailService
    {

        private readonly string _mailTo = String.Empty;
        private readonly string _mailFrom = String.Empty;


        public LocalMailService(IConfiguration configuration)
        {
            _mailTo = configuration["mailSettings:mailTo"];
            _mailFrom = configuration["mailSettings:mailFrom"];


        }


        public void Send(string subject, string message)
        {
            //send mail - output to console window

            Console.WriteLine($"mail from {_mailFrom} to {_mailTo}, " +
                $"with {nameof(LocalMailService)}");

            Console.WriteLine($"Subject: {subject}");
            Console.WriteLine($"Message {message}");


        }


    }
}
