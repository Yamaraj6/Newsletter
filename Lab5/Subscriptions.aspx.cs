using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Lab5
{
    public partial class Subscriptions : System.Web.UI.Page
    {
        #region Private Definitions  
        private readonly SubscriptionsDataContext dataContext = new SubscriptionsDataContext();
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {         
            if (!User.IsInRole("administrator"))
            {
                Server.Transfer("~/Account/AdminLogin.aspx");

            }
            if (!IsPostBack)
            {
                infoLabel.Text = string.Empty;
                errorLabel.Text = string.Empty;
                PopulateGridViewSubscriptions();

            }        
            gridViewSubscriptions.RowEditing +=
                GridViewSubscriptionsOnRowEditing;
            gridViewSubscriptions.RowDeleting +=
                GridViewSubscriptionsOnRowDeleting;
            gridViewSubscriptions.RowUpdating +=
                GridViewSubscriptionsOnRowUpdating;
            gridViewSubscriptions.RowCancelingEdit +=
                GridViewSubscriptionsOnRowCancelingEdit;
            gridViewSubscriptions.PageIndexChanging +=
                GridViewSubscriptionsOnPageIndexChanging;
        }
        private void GridViewSubscriptionsOnRowCancelingEdit(object sender, GridViewCancelEditEventArgs gridViewCancelEditEventArgs)
        {
            gridViewSubscriptions.EditIndex = -1;
            PopulateGridViewSubscriptions();
        }
        private void GridViewSubscriptionsOnRowUpdating(object sender, GridViewUpdateEventArgs gridViewUpdateEventArgs)
        {
            var subscriptionId = (int)gridViewUpdateEventArgs.Keys["id"];
            var email = (string)gridViewUpdateEventArgs.NewValues["email"];
            var username = (string)gridViewUpdateEventArgs.NewValues["userName"];
            try
            {            
                var subscription = dataContext.Subscriptions.
                    First(n => n.Id == subscriptionId);
                subscription.email = email;
                subscription.username = username;
                dataContext.SaveChanges();

                gridViewSubscriptions.EditIndex = -1;
                PopulateGridViewSubscriptions();
                errorLabel.Text = string.Empty;
                infoLabel.Text = "Dane zostały zapisane";
            }
            catch (Exception)

            {
                infoLabel.Text = string.Empty;
                errorLabel.Text = string.Format("Wystąpił błąd podczas próby aktualizacji danych związanych z adresem email {0}. Spróbuj ponownie później.", email);
                return;
            }
            try
            {            
                var message = new MailMessage
                {
                    Subject = "Newsletter",
                    Body = string.Format("Twoje dane, związane z subskrybcją newslettera, zostały zaktualizowane.")
                };             
                var client = new SmtpClient(); message.To.Add(email); client.Send(message);
            }
            catch (Exception)
            {
                infoLabel.Text = string.Empty;
                errorLabel.Text = string.Format("Wystąpił błąd podczas próby wysłania powiadomienia o aktualizacji danych związanych z adresem email {0}. Spróbuj ponownie później.", email);
            }
        }
        private void PopulateGridViewSubscriptions()
        {
            gridViewSubscriptions.DataSource = dataContext.Subscriptions.ToList();
            gridViewSubscriptions.DataBind();
        }
        private void GridViewSubscriptionsOnRowDeleting(object sender, GridViewDeleteEventArgs gridViewDeleteEventArgs)
        {
            var subscriptionId = (int)gridViewDeleteEventArgs.Keys["id"];
            var email = (string)gridViewDeleteEventArgs.Values["email"];
            try
            {
                var subscription = dataContext.Subscriptions.
                    First(n => n.Id == subscriptionId);
                dataContext.Subscriptions.Remove(subscription);
                dataContext.SaveChanges();
                PopulateGridViewSubscriptions();
                errorLabel.Text = string.Empty;
                infoLabel.Text = string.Format("Adres email {0} usunięto z newslettera.", subscription.email);
            }
            catch (Exception)
            {
                infoLabel.Text = string.Empty;
                errorLabel.Text = string.Format("Wystąpił błąd podczas próby usunięcia danych związanych z adresem email {0}. Spróbuj ponownie później.", email);
                return;
            }
            try
            {
                var message = new MailMessage
                {
                    Subject = "Newsletter",
                    Body = string.Format("Twój adres email {0} został usunięty z subskrypcji newslettera.", email)
                };
                var client = new SmtpClient(); message.To.Add(email); client.Send(message);
            }
            catch (Exception)
            {
                infoLabel.Text = string.Empty;
                errorLabel.Text = string.Format("Wystąpił błąd podczas próby wysłania powiadomienia o usunięciu danych związanych z adresem email {0}. Spróbuj ponownie później.", email);
            }
        }
        private void GridViewSubscriptionsOnRowEditing(object sender, GridViewEditEventArgs gridViewEditEventArgs)
        {
            gridViewSubscriptions.EditIndex = gridViewEditEventArgs.NewEditIndex;
            PopulateGridViewSubscriptions();
        }
        private void GridViewSubscriptionsOnPageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gridViewSubscriptions.PageIndex = e.NewPageIndex;
            PopulateGridViewSubscriptions();
        }
    }
}