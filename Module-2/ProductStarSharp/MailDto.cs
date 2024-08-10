namespace ProductStarSharp;

public class MailDto
{
    public string FromAddress { get; set; }
    
    public List<string> ToAddress { get; set; }
    
    public string Subject { get; set; }
    
    public string Body { get; set; }
}