using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Lab5.Account
{
    public partial class AdminLogin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }


        protected void LogIn(object sender, EventArgs e)
        {
            if (IsValid)
            {
                var manager = Context.GetOwinContext()
                    .GetUserManager<ApplicationUserManager>();
                var signinManager = Context.GetOwinContext()
                    .GetUserManager<ApplicationSignInManager>();
                var result = signinManager.PasswordSignIn(Login.Text, Password.Text, RememberMe.Checked, shouldLockout: false);
                switch (result)
                {
                    case SignInStatus.Success:
                        Response.Redirect("~/Subscriptions.aspx");
                        break;
                    case SignInStatus.LockedOut:
                        Response.Redirect("/Account/Lockout.aspx");
                        break;
                    case SignInStatus.RequiresVerification:
                        Response.Redirect(String.Format("/Account/TwoFactorAuthenticationSignIn?" +
                                                        "Return" +
                                                        "Url={0}&RememberMe={1}", Request.QueryString["ReturnUrl"], RememberMe.Checked), true);
                        break;
                    default:
                        FailureText.Text = "Nieudana próba logowania. Spróbuj ponownie.";
                        ErrorMessage.Visible = true;
                        break;
                }
            }
        }
    }
}