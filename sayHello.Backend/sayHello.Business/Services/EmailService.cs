using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace sayHello.Business;

public class EmailService
{
    private static IConfigurationRoot? _configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
   
    public async Task SendConfirmationEmail(string recipientEmail, string confirmationLink ,bool restorePassword =false)
    {
        var emailFrom = _configuration.GetSection("Email").Value;
        var emailPassword = _configuration.GetSection("Password").Value;
        var smtpServer = _configuration.GetSection("Host").Value;
        var smtpPort = int.Parse(_configuration.GetSection("Port").Value);
        
        try
        {
            string emailBody = !restorePassword?$@"
                <html>
                <body>
                    <h1>Confirm Your Email</h1>
                    <p>Please confirm your email address by clicking the button below:</p>
                    <a href='{confirmationLink}' 
                       style='
                          display: inline-block;
                          padding: 10px 20px;
                          font-size: 16px;
                          color: white;
                          background-color: #632969;
                          text-decoration: none;
                          border-radius: 5px;'>
                        Confirm Email
                    </a>
                </body>
                </html>" :
                $@"
                <html>
                <body>
                <h1>Account Restored Successfully 👀 </h1>
                <p>We are glad to inform you that your account has been restored successfully.</p>
                <p>For security reasons, we strongly recommend that you update your password from your profile settings at the earliest opportunity.</p>
                    <a href='{confirmationLink}' 
                       style='
                          display: inline-block;
                          padding: 10px 20px;
                          font-size: 16px;
                          color: white;
                          background-color: #632969;
                          text-decoration: none;
                          border-radius: 5px;'>
                        Confirm Email
                    </a>
                </body>
                </html>";

            MailMessage mail = new MailMessage
            {
                From = new MailAddress(emailFrom),
                Subject = "Email Confirmation",
                Body = emailBody,
                IsBodyHtml = true 
            };

            mail.To.Add(recipientEmail);

            SmtpClient smtpClient = new SmtpClient(smtpServer, smtpPort)
            {
                Credentials = new NetworkCredential(emailFrom, emailPassword),
                EnableSsl = true
            };

            await smtpClient.SendMailAsync(mail);
            Console.WriteLine("Confirmation email sent successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to send email: {ex.Message}");
        }
    }
}