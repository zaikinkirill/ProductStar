namespace ProductStarSharp;

public interface ISender
{
    Task SendAsync(MailDto mailDto);
}