using DeepIn.Emailing.API.Configurations;
using System.Net.Mail;

namespace DeepIn.Emailing.API.Application.Services
{
    public class SmtpEmailSender : IEmailSender
    {
        private readonly ILogger _logger;
        private readonly SmtpOtions _smtpOtions;
        public SmtpEmailSender(ILogger<SmtpEmailSender> logger, SmtpOtions options)
        {
            _logger = logger;
            _smtpOtions = options;
        }
        public async Task SendAsync(string to, string subject, string body, bool isBodyHtml)
        {
            MailMessage mailMessage = new MailMessage();
            mailMessage.To.Add(new MailAddress(to));
            mailMessage.From = new MailAddress(_smtpOtions.FromAddress, _smtpOtions.FromDisplayName);
            if (!string.IsNullOrEmpty(_smtpOtions.ReplyTo))
            {
                mailMessage.ReplyToList.Add(new MailAddress(_smtpOtions.ReplyTo));
            }
            mailMessage.Subject = subject;

            mailMessage.Body = body;
            mailMessage.IsBodyHtml = isBodyHtml;

            // 添加附件
            //string file = "D:\\1.txt";
            //Attachment data = new Attachment(file, MediaTypeNames.Application.Octet);
            // mailMessage.Attachments.Add(data);

            //邮件推送的SMTP地址和端口
            //SmtpClient smtpClient = new SmtpClient("smtpdm.aliyun.com", 25);
            //C#官方文档介绍说明不支持隐式TLS方式，即465端口，需要使用25或者80端口(ECS不支持25端口)，另外需增加一行 smtpClient.EnableSsl = true; 故使用SMTP加密方式需要修改如下：
            SmtpClient smtpClient = new SmtpClient(_smtpOtions.Host, _smtpOtions.Port);
            smtpClient.EnableSsl = true;
            // 使用SMTP用户名和密码进行验证
            System.Net.NetworkCredential credentials = new System.Net.NetworkCredential(_smtpOtions.FromAddress, _smtpOtions.Password);
            smtpClient.Credentials = credentials;
            await smtpClient.SendMailAsync(mailMessage);
        }
    }
}
