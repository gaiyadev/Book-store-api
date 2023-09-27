namespace BookstoreAPI.Templates;

public class EmailNotificationTemplate
{
    public string EmailVerificationTemplate(string verificationLink)
    {
        return $@"
        <!DOCTYPE html>
        <html lang=""en"">
        <head>
            <meta charset=""UTF-8"">
            <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
            <title>Email Verification</title>
        </head>
        <body>
            <h1>Email Verification</h1>
            <p>Dear user,</p>
            <p>Thank you for registering with our service. To verify your email address, please click the link below:</p>
            <p><a href=""{verificationLink}"">Verify your email</a></p>
            <p>If you didn't request this verification, please ignore this email.</p>
            <p>Regards,<br>Your Company Name</p>
        </body>
        </html>
        ";
    }
}
