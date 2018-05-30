<%@ Page Title="Potwierdzenie konta" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Confirm.aspx.cs" Inherits="Lab5.Account.Confirm" Async="true" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <h2><%: Title %>.</h2>

    <div>
        <asp:PlaceHolder runat="server" ID="successPanel" ViewStateMode="Disabled" Visible="true">
            <p>
                Dziękujemy za potwierdzenie konta. Kliknij <asp:HyperLink ID="login" runat="server" NavigateUrl="~/Account/Login">tutaj</asp:HyperLink>  w celu zalogowania się             
            </p>
        </asp:PlaceHolder>
        <asp:PlaceHolder runat="server" ID="errorPanel" ViewStateMode="Disabled" Visible="false">
            <p class="text-danger">
                Wystąpił błąd.
            </p>
        </asp:PlaceHolder>
    </div>
</asp:Content>
