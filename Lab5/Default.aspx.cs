using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Lab5
{
    public partial class _Default : Page
    {
        #region Private Definitions  
        private readonly SubscriptionsDataContext dataContext = new SubscriptionsDataContext();
        private string RootAbsolutePath
        {
            get
            {
                return HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Authority + HttpContext.Current.Request.ApplicationPath;
            }
        }
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (User.IsInRole("administrator"))
            {
                Server.Transfer("Subscriptions.aspx");
            }
            if (!IsPostBack)
            {
                infoLabel.Text = string.Empty;
                errorLabel.Text = string.Empty;
            }
        }

        protected void ButtonAdd_OnClick(object sender, EventArgs e)
        {
            if (captchaTextBox.Text == Session["CaptchaImageText"].ToString())
            {
                captchaLabel.Text = string.Empty;
                captchaTextBox.Text = string.Empty;
            }
            else
            {
                captchaLabel.Text = "Niepoprawny kod!!!";
                captchaTextBox.Text = string.Empty;
                return;
            }
            string username = User.Identity.Name;
            try
            {
                var subscription = dataContext.Subscriptions.FirstOrDefault(n => n.username == username && n.email == textBoxEmail.Text);
                if (subscription != null)
                {
                    infoLabel.Text = string.Empty;
                    errorLabel.Text = string.Format("Adres e-mail {0} istnieje już w bazie subskrybentów", textBoxEmail.Text);
                    return;
                }
                else
                {
                    subscription = new Subscription()
                    {
                        email = textBoxEmail.Text,
                        username = username
                    };
                    dataContext.Subscriptions.Add(subscription);
                    errorLabel.Text = string.Empty;
                    infoLabel.Text = string.Format("Adres e-mail {0} dodano do newslettera", textBoxEmail.Text);
                }
            }
            catch (Exception z)
            {
                infoLabel.Text = string.Empty;
                errorLabel.Text = string.Format("Wystąpił błąd podczas próby dodania adresu e-mail {0}. Spróbuj ponownie później.", textBoxEmail.Text);
                return;
            }
            try
            {
                dataContext.SaveChanges();
                var message = new MailMessage
                {
                    Subject = "Newsletter",
                    Body = string.Format(
                        "Twój adres e - mail został dodany do subskrypcji newslettera."
                        + " Jeśli chcesz zrezygnować z otrzymywania wiadomości wejdź na stronę: {0}, podaj swój e - mail i wybierz opcję" +
                        " 'Anuluj subskrypcję'.", RootAbsolutePath)
                };
                var client = new SmtpClient(); message.To.Add(textBoxEmail.Text);
                client.Send(message);
            }
            catch (Exception z)
            {
                infoLabel.Text = string.Empty;
                errorLabel.Text = string.Format("Adres e-mail {0} dodano do newslettera, ale wystąpił błąd podczas wysyłania potwierdzenia.", textBoxEmail.Text);
            }
        }
        protected void ButtonRemove_OnClick(object sender, EventArgs e)
        {
            if (captchaTextBox.Text == Session["CaptchaImageText"].ToString())
            {
                captchaLabel.Text = string.Empty;
                captchaTextBox.Text = string.Empty;
            }
            else
            {
                captchaLabel.Text = "Niepoprawny kod!!!";
                captchaTextBox.Text = string.Empty;

                return;
            }
            string username = User.Identity.Name;
            Subscription subscription = null;
            try
            {
                subscription = dataContext.Subscriptions.FirstOrDefault(n => n.username == username && n.email == textBoxEmail.Text);
                if (subscription != null)
                {
                    dataContext.Subscriptions.Remove(subscription);
                    errorLabel.Text = string.Empty;
                    infoLabel.Text = string.Format("Adres email {0} usunięto z newslettera", textBoxEmail.Text);

                }
                else
                {
                    infoLabel.Text = string.Empty;
                    errorLabel.Text = string.Format("Adres e-mail {0} nie istnieje w bazie subskrybentów", textBoxEmail.Text);

                }
            }
            catch (Exception)
            {
                infoLabel.Text = string.Empty;
                errorLabel.Text = string.Format("Wystąpił błąd podczas próby usunięcia adresu e-mail {0}. Spróbuj ponownie później.", textBoxEmail.Text);
            }
            if (subscription != null)
            {
                try
                {
                    dataContext.SaveChanges();
                    var message = new MailMessage
                    {
                        Subject = "Newsletter",
                        Body = string.Format("Twój adres e-mail został usunięty z subskrypcji newslettera.) " +
                        "Jeśli ponownie chcesz otrzymywać wiadomości wejdź na stronę: {0}, podaj swój e-mail i wybierz opcję 'Subskrybuj'.", RootAbsolutePath)
                    };
                    var client = new SmtpClient(); message.To.Add(textBoxEmail.Text);
                    client.Send(message);
                }
                catch (Exception)
                {
                    infoLabel.Text = string.Empty;
                    errorLabel.Text = string.Format("Adres e-mail {0} usunięto z newslettera, ale wystąpił błąd podczas wysyłania potwierdzenia.", textBoxEmail.Text);
                }
            }
        }
    }
}