using System;
using System.IO;
using System.Diagnostics;
using System.Windows.Forms;
using System.Net.Mail;

namespace RPGame
{
    public partial class CrashForm : Form
    {
        Global global = new Global();
        CrashHandler handle = new CrashHandler();
        public CrashForm()
        {
           InitializeComponent();
        }

        private void Email_Click(object sender, EventArgs e)
        {
            try
            {
                MailMessage message = new MailMessage("wSilentWEntertainment@gmail.com", "wSilentWEntertainment@gmail.com");
                SmtpClient Client = new SmtpClient("smtp.gmail.com", 587);

                Client.DeliveryMethod = SmtpDeliveryMethod.Network;
                Client.EnableSsl = true;
                Client.UseDefaultCredentials = false;
                Client.Credentials = new System.Net.NetworkCredential("wSilentWEntertainment@gmail.com", "Olufunke1337");

                message.Subject = ("Crash Report");
                message.Body = textBox4.Text;
                message.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;

                try
                {
                    Attachment attachment = new Attachment(Path.Combine(global.getErrorPath(), "errors.txt"));
                    message.Attachments.Add(attachment);
                }
                catch(Exception ex) { global.ErrorHandling(ex.Message, GetType().Name, ex); }

                Client.Send(message);

                Environment.Exit(0);
                }
            catch (Exception ex) { global.ErrorHandling(ex.Message, GetType().Name, ex);
                if (ex.InnerException != null)
                {
                    Debug.WriteLine(ex.InnerException);
                }
            }
        }
    }
}
