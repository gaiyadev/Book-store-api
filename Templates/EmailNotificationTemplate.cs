using BookstoreAPI.Enums;

namespace BookstoreAPI.Templates
{
    public class EmailNotificationTemplate
    {
        public string GetEmailTemplate(string verificationLink, string otp, DeviceType deviceType )
        {
            if (deviceType == DeviceType.Web)
            {
                return GetVerificationEmailTemplate(verificationLink);
            }
            else if (deviceType == DeviceType.Mobile)
            {
                return GetOtpEmailTemplate(otp);
            }
            else
            {
                return GetVerificationEmailTemplate(verificationLink);
            }
        }

        private string GetVerificationEmailTemplate(string verificationLink)
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

        private string GetOtpEmailTemplate(string otp)
        {
            // Customize the OTP email template as needed
            return $@"
            <!DOCTYPE html>
            <html lang=""en"">
            <head>
                <meta charset=""UTF-8"">
                <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
                <title>OTP Confirmation</title>
            </head>
            <body>
                <h1>OTP Confirmation</h1>
                <p>Dear user,</p>
                <p>Your OTP code is: {otp}</p>
                <p>If you didn't request this OTP, please contact support.</p>
                <p>Regards,<br>Your Company Name</p>
            </body>
            </html>
            ";
        }
    }
}
