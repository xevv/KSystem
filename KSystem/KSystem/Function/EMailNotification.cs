using KModel.Entity;
using KSystem.WCF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;

namespace KSystem.Function
{
    public class EmailSettings
    {
        public string SmtpServer { get; set; }
        public int Port { get; set; }
        public string Login { get; set; }
        public string From { get; set; }
        public string Password { get; set; }
        public bool Ssl { get; set; }

        public EmailSettings()
        {
            SmtpServer = "smtp.gmail.com";
            Port = 587;
            Login = "***";
            From = "***";
            Password = "***";
            Ssl = true;
        }
    }
    public class EMailNotification : IEmailNotification, IDisposable
    {
        public EmailSettings EmailSettings { get; set; }
        public SmtpClient Client { get; set; }
        public MailMessage Mail { get; set; }

        public EMailNotification(EmailSettings settings)
        {
            EmailSettings = settings;
            Client = new SmtpClient();
            Mail = new MailMessage();
            Client.Host = EmailSettings.SmtpServer;
            Client.Port = EmailSettings.Port;
            Client.EnableSsl = EmailSettings.Ssl;
            Client.Credentials = new NetworkCredential(EmailSettings.Login, EmailSettings.Password);
            Client.DeliveryMethod = SmtpDeliveryMethod.Network;
        }
        public bool Send(IEnumerable<string> mailto, string subject, string message)
        {
            try
            {
                Mail.From = new MailAddress(EmailSettings.From);
                foreach (string item in mailto)
                {
                    Mail.To.Add(new MailAddress(item));
                }
                Mail.Subject = subject;
                Mail.Body = message;
                Client.Send(Mail);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public string CreateMessage(InputData data, SensorDry sensor, House house)
        {
            StringBuilder body = new StringBuilder();
            body.Append(house.Street);
            body.Append(house.Number);
            body.Append(sensor.Premises1.Name);
            body.Append(sensor.Door1.Name);
            body.Append(sensor.SensorDryType.Name);
            body.Append("изменил свое состояние на");
            if (data.Data == 1)
                body.Append(sensor.SensorDryType.SensorDryValueType.Value1);
            else
                body.Append(sensor.SensorDryType.SensorDryValueType.Value0);
            return body.ToString();
        }

        public void Dispose()
        {
            if (Client != null)
                Client.Dispose();
            if (Client != null)
                Mail.Dispose();
        }
    }
}