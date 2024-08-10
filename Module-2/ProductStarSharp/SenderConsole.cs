namespace ProductStarSharp;

public class SenderConsole : ISender
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
        Console.WriteLine("Отправляется письмо...");
        Console.WriteLine("От кого: " + fromAddress);
        Console.WriteLine("Кому: " + toAddress);
        Console.WriteLine("Тема: " + subject);
        Console.WriteLine("Сообщение: " + body);
        Console.WriteLine("Письмо отправлено!");
    }
}