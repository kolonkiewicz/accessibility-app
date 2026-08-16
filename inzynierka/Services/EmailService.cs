using System.Net;
using System.Net.Mail;

public class EmailService
{
    private readonly IConfiguration _config;

    public EmailService(IConfiguration config)
    {
        _config = config;
    }

    public virtual async Task SendEmailAsync(string to, string subject, string body)
    {
        var email = new MailMessage();
        email.From = new MailAddress(_config["Email:From"]);
        email.To.Add(to);
        email.Subject = subject;
        email.Body = body;
        email.IsBodyHtml = true;

        using var smtp = new SmtpClient(_config["Email:Smtp"], int.Parse(_config["Email:Port"]))
        {
            Credentials = new NetworkCredential(_config["Email:From"], _config["Email:Password"]),
            EnableSsl = true
        };

        await smtp.SendMailAsync(email);
    }
}
