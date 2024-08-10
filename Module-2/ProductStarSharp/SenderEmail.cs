using System.Net;
using System.Net.Mail;

namespace ProductStarSharp;

public class SenderEmail : ISender
{
    public async Task SendAsync(MailDto mailDto)
    {
        List<Task> tasks = new List<Task>();
        foreach (var toAddress in mailDto.ToAddress)
        {
            //await Task.Run(() => Send(mailDto.FromAddress, toAddress, mailDto.Subject, mailDto.Body));
            tasks.Add(Send(mailDto.FromAddress, toAddress, mailDto.Subject, mailDto.Body));
        }

        await Task.WhenAll(tasks);
    }
    
    private async Task Send(string fromAddress, string toAddress, string subject, string body)
    {
        MailAddress from = new MailAddress(fromAddress);
        MailAddress to = new MailAddress(toAddress);
        MailMessage m = new MailMessage(from, to);
        m.Subject = subject;
        m.Body = body;
        SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
        smtp.Credentials = new NetworkCredential("somemail@gmail.com", "mypassword");
        smtp.EnableSsl = true;
        await smtp.SendMailAsync(m);
        Console.WriteLine("Письмо отправлено");
    }
}