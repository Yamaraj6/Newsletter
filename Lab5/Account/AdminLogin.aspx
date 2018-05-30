<%@ Page Title="Zaloguj się"
    Language="C#"
    MasterPageFile="~/Site.Master"
    AutoEventWireup="true" 
    CodeBehind="AdminLogin.aspx.cs"
    Inherits="Lab5.Account.AdminLogin"
    Async="true" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <h2>Zaloguj się przy użyciu lokalnego konta.</h2>
    <div class="row">
        <div class="col-md-8">
            <section id="loginForm">
                <div class="form-horizontal">
                    <h3>Na potrzeby testów dane do logowania to:<br />
                        Login - Admin, Hasło - P4$$word</h3>
                    <hr />
                    <asp:PlaceHolder runat="server" ID="ErrorMessage" Visible="false">
                        <p class="text-danger">
                            <asp:Literal runat="server" ID="FailureText" />
                        </p>
                    </asp:PlaceHolder>
                    <div class="form-group">
                        <asp:Label runat="server" AssociatedControlID="Login" CssClass="col-md-2 control-label">Login</asp:Label>
                        <div class="col-md-10">
                            <asp:TextBox runat="server" ID="Login" CssClass="form-control" />
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="Login" CssClass="text-danger" ErrorMessage="Login jest wymagany." />
                        </div>
                    </div>
                    <div class="form-group">
                        <asp:Label runat="server" AssociatedControlID="Password" CssClass="col-md-2 controllabel">Hasło</asp:Label>
                        <div class="col-md-10">
                            <asp:TextBox runat="server" ID="Password" TextMode="Password" CssClass="form-control" />
                            <asp:RequiredFieldValidator runat="server"
                                ControlToValidate="Password" CssClass="text-danger" ErrorMessage="Hasło jest wymagane." />
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="col-md-offset-2 col-md-10">
                            <div class="checkbox">
                                <asp:CheckBox runat="server" ID="RememberMe" />
                                <asp:Label runat="server" AssociatedControlID="RememberMe">Zapamiętaj mnie</asp:Label>
                            </div>
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="col-md-offset-2 col-md-10">
                            <asp:Button runat="server" OnClick="LogIn" Text="Zaloguj się" CssClass="btn btn-default" />
                        </div>
                    </div>
                </div>
            </section>
        </div>
    </div>
</asp:Content>
