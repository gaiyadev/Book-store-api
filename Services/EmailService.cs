using System.Net;
using BookstoreAPI.CustomExceptions.Exceptions;
using MailKit.Net.Smtp;
using MimeKit;

namespace BookstoreAPI.Services;

public sealed class EmailService
{
    private readonly ILogger<EmailService> _logger;
    private readonly string _mailTrapHost = Environment.GetEnvironmentVariable("MAILTRAP_HOST") ?? throw new InvalidOperationException();
    private readonly string _mailTrapPort = Environment.GetEnvironmentVariable("MAILTRAP_PORT") ?? throw new InvalidOperationException();
    private readonly string _mailTrapPassword = Environment.GetEnvironmentVariable("MAILTRAP_PASSWORD") ?? throw new InvalidOperationException();
    private readonly string _mailTrapUsername = Environment.GetEnvironmentVariable("MAILTRAP_USERNAME") ?? throw new InvalidOperationException();

    public EmailService(ILogger<EmailService> logger
    )
    {
        _logger = logger;
    }
    
    public async Task SendEmail(string recipientEmail, string subject, string body)
    {
        try
        {
            // Create a new MimeMessage.
            var message = new MimeMessage();

            // Set the sender and recipient addresses.
            message.From.Add(new MailboxAddress("BookStore", "no-reply@example.com"));
            message.To.Add(new MailboxAddress("", recipientEmail)); // Leave the recipient name empty

            // Set the subject and body of the email.
            message.Subject = subject;

            // Create a text part for the email body.
            var bodyBuilder = new BodyBuilder();
            bodyBuilder.HtmlBody = body;

            // Attach the text part to the message.
            message.Body = bodyBuilder.ToMessageBody();

            // Create a new SmtpClient instance.
            using var smtpClient = new SmtpClient();

            await smtpClient.ConnectAsync(_mailTrapHost, int.Parse(_mailTrapPort), false);

            // Get the username and password from the appsettings.json file.
            string username = _mailTrapUsername;
            string password = _mailTrapPassword;

            // Authenticate with the SMTP server.
            await  smtpClient.AuthenticateAsync(username, password);

            // Send the email.
            await  smtpClient.SendAsync(message);

            // Disconnect from the SMTP server.
            await smtpClient.DisconnectAsync(true);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            throw new InternalServerException(ex.Message, HttpStatusCode.InternalServerError);
        }
    }
}