<%@ Page Title="Subskrypcje" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Subscriptions.aspx.cs" Inherits="Lab5.Subscriptions" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="jumbotron">
        <h1>Usługi Microsoft Azure</h1>
        <h2>Serwis Newsletter - automatyczne powiadomienia email</h2>
    </div>
    <div class="panel panel-default">
        <div class="panel-heading">Lista subskrybentów serwisu.</div>  
        <div class="panel-body">
            <asp:Label runat="server" ID="infoLabel" CssClass="label labelsuccess"></asp:Label><br />
            <asp:Label runat="server" ID="errorLabel" CssClass="label labeldanger"></asp:Label><br />
            <asp:GridView runat="server" ID="gridViewSubscriptions" AutoGenerateColumns="False" DataKeyNames="id" AllowPaging="True" PageSize="5" CssClass="table table-striped table-bordered">
                <Columns>
                    <asp:TemplateField HeaderText="#">
                        <ItemTemplate><%# Container.DataItemIndex + 1 %> </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="userName" HeaderText="Użytkownik" />
                    <asp:BoundField DataField="email" HeaderText="Email" />
                    <asp:CommandField CancelText="Anuluj" DeleteText="Usuń" ShowDeleteButton="True" EditText="Edytuj" ShowEditButton="True" UpdateText="Zapisz" />
                </Columns>
            </asp:GridView>
        </div>
    </div>
</asp:Content>
