namespace BookstoreAPI.Services;

public sealed class PasswordService
{
    public  string HashPassword(string password)
    {
        return  BCrypt.Net.BCrypt.HashPassword(password);
    }

    public  bool VerifyPassword(string password, string hashedPassword)
    {
        return  BCrypt.Net.BCrypt.Verify(password, hashedPassword);
    }

    public string GenerateOtp()
    {
        var random = new Random();
        int otp = random.Next(1000, 9999); // Generate a 4-digit OTP
        return otp.ToString();
    }
        
    public string GenerateRandomTrackingId(int length)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        Random random = new Random();
        return new string(Enumerable.Repeat(chars, length)
            .Select(s => s[random.Next(s.Length)]).ToArray());
    }

}