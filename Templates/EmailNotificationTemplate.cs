using BookstoreAPI.Enums;

namespace BookstoreAPI.Templates
{
    public class EmailNotificationTemplate
    {
        public string GetEmailTemplate(string verificationLink, string otp, DeviceType deviceType )
        {
            switch (deviceType)
            {
                case DeviceType.Web:
                    return GetVerificationEmailTemplate(verificationLink);
                case DeviceType.Mobile:
                    return GetOtpEmailTemplate(otp);
                default:
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
        
        public string ForgotPasswordEmailTemplate(string otp)
        {
            return $@"
            <!DOCTYPE html>
            <html lang=""en"">
            <head>
                <meta charset=""UTF-8"">
                <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
                <title>Forgot Password OTP</title>
            </head>
            <body>
                <h1>Forgot Password OTP</h1>
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
