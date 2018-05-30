<%@ Page Title="Zmieniono hasło" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ResetPasswordConfirmation.aspx.cs" Inherits="Lab5.Account.ResetPasswordConfirmation" Async="true" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <h2><%: Title %>.</h2>
    <div>
        <p>Zmieniono hasło. Kliknij <asp:HyperLink ID="login" runat="server" NavigateUrl="~/Account/Login">tutaj</asp:HyperLink> w celu zalogowania się </p>
    </div>
</asp:Content>
