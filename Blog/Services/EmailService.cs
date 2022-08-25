using System.Net;
using System.Net.Mail;

namespace Blog.Services;

public class EmailService
{
    public bool Enviar(
        string toNome,
        string toEmail,
        string assunto,
        string corpo,
        string fromNome = "Gabriel Souza Campos",
        string fromEmail = "gabriel.s.campos@hotmail.com"
    )
    {
        var smtpClient = new SmtpClient(Configuration.Smtp.Host, Configuration.Smtp.Port);

        smtpClient.Credentials = new NetworkCredential(Configuration.Smtp.UserName, Configuration.Smtp.Password);
        smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
        smtpClient.EnableSsl = true;
        var mail = new MailMessage();

        mail.From = new MailAddress(fromEmail, fromNome);
        mail.To.Add(new MailAddress(toEmail, toNome));
        mail.Subject = assunto;
        mail.Body = corpo;
        mail.IsBodyHtml = true;

        try
        {
            smtpClient.Send(mail);
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
}
