//Рассылка электронных сообщений по списку адресов. Класс, реализующий рассылку не знает, каким способом эта рассылка будет выполняться.

using System.Text.RegularExpressions;
using Microsoft.Extensions.DependencyInjection;
using ProductStarSharp;

var services = new ServiceCollection();
services.AddSingleton<ISender, SenderConsole>();
var serviceProvider = services.BuildServiceProvider();

MailDto mailDto = new MailDto();
Console.Write("Введите адрес отправителя: ");
mailDto.FromAddress = Console.ReadLine();
while (!IsValidEmail(mailDto.FromAddress))
{
    Console.Write("Неверный адрес! Введите адрес отправителя еще раз: ");
    mailDto.FromAddress = Console.ReadLine();
}

Console.Write("Введите адрес получателей через запятую: ");
var toAddress = Console.ReadLine().Split([',']).ToList();
mailDto.ToAddress = new List<string>();
bool emptyToAddress = true;
while (emptyToAddress)
{
    foreach (var address in toAddress)
    {
        if (IsValidEmail(address))
        {
            mailDto.ToAddress.Add(address);
            emptyToAddress = false;
        }
        else
        {
            Console.WriteLine("Неверный адрес " + address);
            if (emptyToAddress)
            {
                Console.Write("Введите адрес получателей через запятую: ");
                toAddress = Console.ReadLine().Split([',']).ToList();
            }
        }
    }
}
Console.Write("Введите тему сообщения: ");
mailDto.Subject = Console.ReadLine();

Console.Write("Введите текст сообщения: ");
mailDto.Body = Console.ReadLine();

// MailDto mailDto = new MailDto()
// {
//     FromAddress = "zaikin@mail.ru",
//     ToAddress = new List<string>() {"proverka@mail.ru", "test1@mail.ru", "test2@mail.ru", "test3@mail.ru"},
//     Subject = "tema",
//     Body = "texxxt"
// };

var senderService = serviceProvider.GetRequiredService<ISender>();
await senderService.SendAsync(mailDto);


bool IsValidEmail(string email)
{
    string pattern = "[.\\-_a-z0-9]+@([a-z0-9][\\-a-z0-9]+\\.)+[a-z]{2,6}";
    Match isMatch = Regex.Match(email, pattern, RegexOptions.IgnoreCase);
    return isMatch.Success;
}