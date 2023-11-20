using System.Net;
using System.Net.Mail;

namespace KnightTournament.Helpers
{
    public class EmailHelper
    {
        private readonly SmtpClient _smtpClient;

        public EmailHelper()
        {
            _smtpClient = ConfigureSmtpClient();
        }

        public async Task<Result<bool>> SendMessageAsync(string emailAddress, string message, string theme = null, List<FileInfo> files = null)
        {
            var mailMessage = CreateMailMessage(emailAddress, theme, message, files);

            try
            {
                _smtpClient.Send(mailMessage);
                return new Result<bool>(true);
            }
            catch (Exception ex)
            {
                return new Result<bool>(false, "Error: " + ex.Message);
            }
        }

        private SmtpClient ConfigureSmtpClient()
        {
            var smtpClient = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential("beliykharkov.n@gmail.com", "ttgbjidbvrtaxwdh"),
                DeliveryMethod = SmtpDeliveryMethod.Network,
                EnableSsl = true
            };
            return smtpClient;
        }

        private MailMessage CreateMailMessage(string emailAddress, string theme, string message, List<FileInfo> files)
        {
            var mailMessage = new MailMessage
            {
                From = new MailAddress("beliykharkov.n@gmail.com"),
                Subject = theme,
                Body = message,
            };
            mailMessage.To.Add(emailAddress);
            if (files is not null)
            {
                foreach (var file in files)
                {
                    if (file is not null)
                    {
                        mailMessage.Attachments.Add(new Attachment(file.FullName));
                    }
                }
            }

            return mailMessage;
        }
    }
}
