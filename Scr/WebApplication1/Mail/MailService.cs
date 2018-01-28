﻿using System;
using System.Net.Mail;

namespace WebApplication1.Mail
{
    public class MailService : IMailService
    {
        public void SendEmail(string email, string nickName)
        {
            try
            {
                SmtpClient SmtpServer = new SmtpClient("smtp.live.com");
                var mail = new MailMessage
                {
                    From = new MailAddress("tictatctoe@hotmail.com ")
                };

                mail.To.Add(email);
                mail.Subject = "Tic tac toe";
                mail.Body = "Hello "+ nickName + " it's your turn";
                SmtpServer.Port = 587;

                SmtpServer.UseDefaultCredentials = false;
                SmtpServer.Credentials = new System.Net.NetworkCredential("tictatctoe@hotmail.com", "emelie1234");
                SmtpServer.EnableSsl = true;
                SmtpServer.Send(mail);
            }
            catch (Exception)
            {
                //ignore
            }
       }
    }
}
