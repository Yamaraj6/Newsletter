<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Lab5._Default" %>
<asp:content id="BodyContent" contentplaceholderid="MainContent" runat="server">
    <div class="jumbotron"> 
        <h1>Usługi Microsoft Azure</h1>    
        <h2>Serwis Newsletter - automatyczne powiadomienia e-mail</h2>  
        <p class="lead">       
            Dopisz swój e-mail, aby na bieżąco otrzymywać nowe wiadomości i powiadomienia       
            lub anuluj subskrypcję jeśli nie chcesz otrzymywać wiadomości.     

        </p> 
    </div>   
    <div class="panel panel-default">     
        <div class="panel-heading">Formularz zgłoszeniowy</div> 
        <div class="panel-body">        
            <asp:Label runat="server" ID="infoLabel" CssClass="label label-success"></asp:Label><br />     
            <asp:Label runat="server" ID="errorLabel" CssClass="label label-danger"></asp:Label><br />   
            <asp:ValidationSummary runat="server" ValidationGroup="Validation" CssClass="alert alert-danger" />     
            <asp:RequiredFieldValidator runat="server" Display="None" ErrorMessage="Email jest wymagany"          
                ControlToValidate="textBoxEmail" ValidationGroup="Validation" />        
            <asp:RegularExpressionValidator runat="server"            
                ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([.]\w+)*"     
                ControlToValidate="textBoxEmail" ErrorMessage="Niepoprawny adres email"     
                Display="None" ValidationGroup="Validation" />        
            <asp:RequiredFieldValidator runat="server" Display="None" ErrorMessage="Kod jest wymagany"  
                ControlToValidate="captchaTextBox" ValidationGroup="Validation" />       
            <div class="form-group">          
                <asp:Label runat="server" CssClass="control-label col-md-3" AssociatedControlID="textBoxEmail" Text="Podaj email:" />         
                <div class="col-md-9">                 
                    <asp:TextBox runat="server" ID="textBoxEmail" CssClass="form-control" TextMode="Email" />  

                </div>      

            </div>     
            <div class="form-group">     
                <asp:Label runat="server" CssClass="control-label col-md-3" AssociatedControlID="captchaTextBox" Text="Przepisz kod z obrazka:" />     
                <div class="col-md-9">              
                    <asp:TextBox ID="captchaTextBox" runat="server" CssClass="form-control"></asp:TextBox>      

                </div>        

            </div>        
            <div class="form-group">           
                <div class="col-md-offset-3 col-md-9">     
                    <asp:Image ID="captchaImage" runat="server" ImageUrl="~/CaptchaImage.aspx" />         
                    <asp:Label ID="captchaLabel" runat="server" CssClass="label label-danger"></asp:Label>      

                </div>            

            </div>           
            <div class="form-group">     
                <div class="col-md-offset-3 col-md-9">    
                    <asp:Button runat="server" ID="buttonAdd" ValidationGroup="Validation" CssClass="btn btn-default" Text="Subskrybuj" OnClick="ButtonAdd_OnClick" />      
                    <asp:Button runat="server" ID="buttonRemove" ValidationGroup="Validation" CssClass="btn btn-default" Text="Anuluj subskrypcję" OnClick="ButtonRemove_OnClick" />    

                </div>   

            </div>     

        </div>   

    </div> 

</asp:content>
