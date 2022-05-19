using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Mail;

namespace DigitalLibrary.Manager
{
    public sealed class sendMail
    {
        private sendMail()
        {

        }
        private static sendMail single_send_mail = null;
        public static sendMail createinstance
        {
            get
            {
                if (single_send_mail == null)
                {
                    return single_send_mail = new sendMail();
                }
                return single_send_mail;
            }
        }

        public int mail(string Email, string subject, string Body)
        {
            try
            {
                MailMessage message = new MailMessage();
                SmtpClient smtp = new SmtpClient();

                message.From = new MailAddress("info.MyDigitalLibrary@gmail.com");

                //Enter your email blow and also change in database too

                message.To.Add(new MailAddress(Email));
                message.Subject = subject;
                message.Body = Body;

                smtp.Port = 587;
                smtp.Host = "smtp.gmail.com";
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = true;
                smtp.Credentials = new NetworkCredential("info.MyDigitalLibrary@gmail.com", "DigitalLibrary123");
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Send(message);
                return 1;
            }
            catch (Exception)
            {

                return 0;
            }

        }

    }
}