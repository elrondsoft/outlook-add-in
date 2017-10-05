using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace Helios.Tests.Email
{

    public class SendGridProvider
    {
        public  void Send()
        {
            // var apiKey = Environment.GetEnvironmentVariable();
            var client = new SendGridClient("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");

            // Send a Single Email using the Mail Helper
            var from = new EmailAddress("test@example.com", "Example User");
            var subject = "Hello World from the SendGrid CSharp Library Helper!";
            var to = new EmailAddress("ultramarine256@gmail.com", "Example User");
            var plainTextContent = "AAA [SendSingleEmailAsync]!";
            var htmlContent = "<strong>Hello, Email from the helper! [SendSingleEmailAsync]</strong>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);

            var response =  client.SendEmailAsync(msg).Result;

            Console.WriteLine(msg.Serialize());
            Console.WriteLine(response.StatusCode);
            Console.WriteLine(response.Headers);
            Console.WriteLine("\n\nPress <Enter> to continue.");
            Console.ReadLine();
        }
    }

    [TestFixture]
    public sealed class EmailTests
    {
        [Test]
        [Ignore("Real Email")]
        public void EmailSync__ShouldSend()
        {
            var sendGridProvider = new SendGridProvider();
            sendGridProvider.Send();
        }
    }
}
