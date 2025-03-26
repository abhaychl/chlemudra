using log4net;
using System;
using System.Collections.Generic;
using System.Net.Mail;


namespace CHLeMudra.Data
{
    public class EmailSender
    {

        public bool SendEmail(string Fromemail, string FromName, string Toemail, string bcc, string Subject, String body)
        {
            try
            {
                MailMessage mail = new MailMessage();
#pragma warning disable CS0618 // 'MailMessage.ReplyTo' is obsolete: 'ReplyTo is obsoleted for this type.  Please use ReplyToList instead which can accept multiple addresses. http://go.microsoft.com/fwlink/?linkid=14202'
                mail.ReplyTo = new MailAddress(Fromemail);
#pragma warning restore CS0618 // 'MailMessage.ReplyTo' is obsolete: 'ReplyTo is obsoleted for this type.  Please use ReplyToList instead which can accept multiple addresses. http://go.microsoft.com/fwlink/?linkid=14202'

                //string To = ConfigurationManager.AppSettings["EnquiryEmail"];
                //mail.From = new MailAddress(To, FromName);
                //   mail.From = new MailAddress(Fromemail, FromName);
                mail.To.Add(Toemail);
                if (!string.IsNullOrEmpty(bcc))
                    mail.Bcc.Add(bcc);
                mail.Subject = Subject;
                mail.Body = body;
                mail.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient();
                smtp.Send(mail);
                //var message = new MailMessage();
                //message.From = new MailAddress(Fromemail);
                //message.To.Add(Toemail);

                //message.Subject = Subject;
                //message.Body = body;
                //message.IsBodyHtml = true;

                //using (var smtpClient = new SmtpClient())
                //{
                //    smtpClient.UseDefaultCredentials = false;
                //    smtpClient.Host = "mail.crystalhues.com";
                //    smtpClient.Port = 587;
                //    smtpClient.EnableSsl =false;
                //    smtpClient.Credentials = new System.Net.NetworkCredential("om@crystalhues.com", "oCHL@2232s");
                //    smtpClient.Send(message);
                //}
                //SmtpClient smtp = new SmtpClient
                //{
                //    Host = "mail.crystalhues.com", // smtp server address here…
                //    Port = 587,

                //    EnableSsl = false,
                //    DeliveryMethod = SmtpDeliveryMethod.Network,
                //    Credentials = new System.Net.NetworkCredential("om@crystalhues.com", "oCHL@2232s"),
                //    Timeout = 30000,
                //};
                //MailMessage message = new MailMessage(Fromemail, Toemail, Subject, body);
                //smtp.Send(message);
            }
            catch (Exception ex)
            {
                ILog Logger = LogManager.GetLogger(System.Environment.MachineName);
                Logger.Error("Email Sending Failed : " + ex.Message, ex);
                return false;
            }
            return true;
        }

        public bool SendEmail(string Toemail, string bcc, string Subject, String body)
        {
            try
            {
                MailMessage mailMsg = new MailMessage();
                //mailMsg.From = new MailAddress(FromAddress);
                mailMsg.To.Add(new MailAddress(Toemail));
                if (!string.IsNullOrEmpty(bcc))
                    mailMsg.Bcc.Add(bcc);
                mailMsg.Subject = Subject;
                mailMsg.BodyEncoding = System.Text.Encoding.GetEncoding("utf-8");
                System.Net.Mail.AlternateView plainView = System.Net.Mail.AlternateView.CreateAlternateViewFromString
                (System.Text.RegularExpressions.Regex.Replace(body, @"<(.|\n)*?>", string.Empty), null, "text/plain");
                System.Net.Mail.AlternateView htmlView = System.Net.Mail.AlternateView.CreateAlternateViewFromString(body, null, "text/html");

                mailMsg.AlternateViews.Add(plainView);
                mailMsg.AlternateViews.Add(htmlView);

                // Smtp configuration
                SmtpClient smtp = new SmtpClient();
                // smtp.EnableSsl = true;
                smtp.Send(mailMsg);


            }
            catch (Exception ex)
            {
                ILog Logger = LogManager.GetLogger(System.Environment.MachineName);
                Logger.Error("Email Sending Failed : " + ex.Message, ex);
                return false;
            }
            return true;
        }
        public bool SendEmail(string Toemail, string cc, string bcc, string Subject, String body)
        {
            try
            {
                //MailMessage mail = new MailMessage();
                //mail.To.Add(Toemail);

                //if (!string.IsNullOrEmpty(cc))
                //    mail.CC.Add(cc);
                //if (!string.IsNullOrEmpty(bcc))
                //    mail.Bcc.Add(bcc);

                //mail.Subject = Subject;
                //mail.Body = body;
                //mail.IsBodyHtml = true;
                //SmtpClient smtp = new SmtpClient();
                //smtp.Send(mail);

                MailMessage mailMsg = new MailMessage();
                mailMsg.To.Add(new MailAddress(Toemail));
                if (!string.IsNullOrEmpty(cc))
                    mailMsg.CC.Add(cc);



                if (!string.IsNullOrEmpty(bcc))
                    mailMsg.Bcc.Add(bcc);

                mailMsg.Subject = Subject;
                mailMsg.BodyEncoding = System.Text.Encoding.GetEncoding("utf-8");
                System.Net.Mail.AlternateView plainView = System.Net.Mail.AlternateView.CreateAlternateViewFromString
                (System.Text.RegularExpressions.Regex.Replace(body, @"<(.|\n)*?>", string.Empty), null, "text/plain");
                System.Net.Mail.AlternateView htmlView = System.Net.Mail.AlternateView.CreateAlternateViewFromString(body, null, "text/html");

                mailMsg.AlternateViews.Add(plainView);
                mailMsg.AlternateViews.Add(htmlView);

                // Smtp configuration
                SmtpClient smtp = new SmtpClient();
                //smtp.EnableSsl = true;
                smtp.Send(mailMsg);


            }
            catch (Exception ex)
            {
                ILog Logger = LogManager.GetLogger(System.Environment.MachineName);
                Logger.Error("Email Sending Failed : " + ex.Message, ex);
                return false;
            }
            return true;
        }
        public bool SendEmail(string Toemail, List<string> cc, string bcc, string Subject, String body)
        {
            try
            {
                //MailMessage mail = new MailMessage();
                //mail.To.Add(Toemail);

                //if (!string.IsNullOrEmpty(cc))
                //    mail.CC.Add(cc);
                //if (!string.IsNullOrEmpty(bcc))
                //    mail.Bcc.Add(bcc);

                //mail.Subject = Subject;
                //mail.Body = body;
                //mail.IsBodyHtml = true;
                //SmtpClient smtp = new SmtpClient();
                //smtp.Send(mail);

                MailMessage mailMsg = new MailMessage();
                if (!string.IsNullOrEmpty(Toemail))
                    mailMsg.To.Add(new MailAddress(Toemail));
                if (cc != null && cc.Count > 0)
                {
                    foreach (string e in cc)
                    {
                        if (!string.IsNullOrEmpty(e))
                            mailMsg.CC.Add(e);
                    }

                }
                if (!string.IsNullOrEmpty(bcc))
                    mailMsg.Bcc.Add(bcc);

                mailMsg.Subject = Subject;
                mailMsg.BodyEncoding = System.Text.Encoding.GetEncoding("utf-8");
                System.Net.Mail.AlternateView plainView = System.Net.Mail.AlternateView.CreateAlternateViewFromString
                (System.Text.RegularExpressions.Regex.Replace(body, @"<(.|\n)*?>", string.Empty), null, "text/plain");
                System.Net.Mail.AlternateView htmlView = System.Net.Mail.AlternateView.CreateAlternateViewFromString(body, null, "text/html");

                mailMsg.AlternateViews.Add(plainView);
                mailMsg.AlternateViews.Add(htmlView);

                // Smtp configuration
                SmtpClient smtp = new SmtpClient();
                //smtp.EnableSsl = true;
                smtp.Send(mailMsg);


            }
            catch (Exception ex)
            {
                ILog Logger = LogManager.GetLogger(System.Environment.MachineName);
                Logger.Error("Email Sending Failed : " + ex.Message, ex);
                return false;
            }
            return true;
        }
    }
}