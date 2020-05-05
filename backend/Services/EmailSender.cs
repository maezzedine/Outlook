using Microsoft.AspNetCore.Identity.UI.Services;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using System.Net.Mail;
using System.Net;
using Microsoft.Extensions.Configuration;

namespace backend.Services
{
    public class EmailSender : IEmailSender
    {
        public EmailSender(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public Task SendEmailAsync(string email, string subject, string message)
        {
            var server = Configuration["SMTP:Server"];
            var sender = Configuration["EmailVerification:Sender"];
            var password = Configuration["EmailVerification:Password"];

            var client = new SmtpClient
            {
                Host = server,
                Port = 8889,
                Credentials = new NetworkCredential(sender, password)
            };

            var smtpMessage = new MailMessage(sender, email)
            {
                Subject = subject,
                Body = message,
                IsBodyHtml = true
            };

            return client.SendMailAsync(smtpMessage);
        }

        public static string EmailVerificationHtmlMessage(string link)
        {
            #region html message
            var message = "<html>" +
            "	<head>" +
            "		<meta name='viewport' content='width = device - width,initial - scale = 1.0'>" +
                    "	</head>" +
            "	<body>" +
            "		<div class='title'>Outlook  آوتلوك</div>" +
            "		<div class='details'>" +
            "			<div class='en'>" +
            "				<div class='greetings'>Welcome to AUB Outlook</div>" +
            $"				Please confirm your account by <a href='{link}'>clicking here</a>." +
            "				<div class='salutation'>~ AUB Outlook Team</div>" +
            "			</div>" +
            "			<div class='ar'>" +
            "				<div class='greetings'>أهلًا بكم في آوتلوك</div>" +
            $"				يُرجى تأكيد الحساب عبر <a href='{link}'>الضغط هنا</a>." +
            "				<div class='salutation'>~ فريق عمل آوتلوك</div>" +
            "			</div>" +
            "		</div>" +
            "		<style>" +
            "			body {" +
            "				background: #f7f9fa;" +
            "			}" +
            "            .title {" +
            "                display: flex;" +
            "                flex-direction: row;" +
            "                justify-content: center;" +
            "				 text-align: center;" +
            "                font-size: 30px;" +
            "                color: #547d8c;" +
            "            }" +
            "            .details {" +
            "                background: #f2f2f5;" +
            "				margin: 5%;" +
            "				padding: 5%;" +
            "            }" +
            "            .greetings {" +
            "				font-size: large;" +
            "				line-height: 40px;" +
            "            }" +
            "            .en {" +
            "                padding-bottom: 10%;" +
            "                border-bottom: .5px solid #547d8c;" +
            "            }" +
            "            .ar {" +
            "                direction: rtl;" +
            "				padding-top: 5%;" +
            "            }" +
            "            .salutation {" +
            "				margin-top: 5%;" +
            "				text-align: end;" +
            "            }" +
            "		</style>" +
            "	</body>" +
            "</html>";
            #endregion
            return message;
        }
    }
}
