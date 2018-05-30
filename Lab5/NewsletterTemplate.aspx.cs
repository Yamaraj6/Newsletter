using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;

namespace Lab5
{
    public partial class NewsletterTemplate : System.Web.UI.Page
    {
        #region Private Definitions  
        /// <summary>    
        /// /// Obiekt odpowiedzialny za zarządzanie tabelą subskrybentów   
        /// /// </summary>     
        private readonly SubscriptionsDataContext dataContext = new SubscriptionsDataContext();
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!User.IsInRole("administrator"))
            {
                Server.Transfer("Account/AdminLogin.aspx");
            }

            if (!IsPostBack)
            {
                infoLabel.Text = string.Empty; errorLabel.Text = string.Empty;
            }

        }
        protected void ButtonSend_OnClick(object sender, EventArgs e)
        {
            infoLabel.Text = string.Empty; errorLabel.Text = string.Empty;
            if (!fileUploadContent.HasFile)
            {
                errorLabel.Text = "Plik z treścią newslettera jest wymagany";
                return;
            }
            HttpPostedFile file = fileUploadContent.PostedFile;
            if (file.ContentLength > 1024 * 1024)
            {
                errorLabel.Text = "Plik może mieć max. 1MB";
                return;
            }
            string fileContent; using (var reader = new StreamReader(file.InputStream)) { fileContent = reader.ReadToEnd(); }
            if (string.IsNullOrWhiteSpace(fileContent)) { errorLabel.Text = "Plik nie może być pusty"; return; }
            string extension = Path.GetExtension(file.FileName).ToLower();
            var message = new MailMessage
            {
                Subject = textboxTopic.Text,
                Body = fileContent,
                IsBodyHtml = extension == ".html"
            };
            var client = new SmtpClient();
            var mailErrors = new List<MailError>();
            foreach (var subscription in dataContext.Subscriptions)
            {
                try
                {
                    message.To.Clear();
                    message.To.Add(subscription.email);
                    client.Send(message);
                }
                catch (Exception ex)
                {
                    var mailError = new MailError()
                    {
                        Mail = subscription.email,
                        UserName = subscription.username,
                        ErrorMessage = ex.Message

                    }; mailErrors.Add(mailError);
                }
            }
            infoLabel.Text = string.Format("Newsletter został rozesłany. Poprawnie wysłanych wiadomości: {0}. Błędów podczas wysyłania: {1}.",
                dataContext.Subscriptions.Count() - mailErrors.Count, mailErrors.Count);
            //Zapis raportu wysyłki do pliku XML      
            SaveMailErrors(mailErrors);
        }
        private void SaveMailErrors(IEnumerable<MailError> mailErrors)
        {
            var newsletter = new XElement("Newsletter",
                new XAttribute("SendDate",
                DateTime.Now.ToString("yyyy-mm-dd HH:mm")));
            foreach (MailError mailError in mailErrors)
            {
                var error = new XElement("MailError",
                new XAttribute("email", mailError.Mail),
                new XAttribute("username", mailError.UserName));
                error.SetValue(mailError.ErrorMessage);
                newsletter.Add(error);
            }

            string file = Server.MapPath("~/App_Data/NewsletterReport.xml");
            if (File.Exists(file))
            {
                var xDocument = XDocument.Load(file);
                var xRoot = xDocument.Element("Report");
                if (xRoot != null)
                {
                    xRoot.Add(newsletter);

                }
                xDocument.Save(file);
            }
            else
            {
                var xRoot = new XElement("Report");
                xRoot.Add(newsletter);
                var xDocument = new XDocument();
                xDocument.Add(xRoot);
                xDocument.Save(file);
            }
        }
    }
}