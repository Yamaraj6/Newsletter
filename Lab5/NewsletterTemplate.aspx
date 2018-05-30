<%@ Page Title="Szablon newslettera" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="NewsletterTemplate.aspx.cs" Inherits="Lab5.NewsletterTemplate" %> 
<asp:content id="BodyContent" contentplaceholderid="MainContent" runat="server">    
    <div class="jumbotron">   
        <h1>Usługi Microsoft Azure</h1>      
        <h2>Serwis Newsletter - automatyczne powiadomienia email</h2>    
        <p class="lead">    
            Wypełnij szablon newslettera i roześlij do wszystkich subskrybentów.    

        </p>    

    </div>   
    <div class="panel panel-default">    
        <div class="panel-heading">Formularz zgłoszeniowy</div>    
        <div class="panel-body">         
            <asp:Label runat="server" ID="infoLabel" CssClass="label label-success"></asp:Label><br />   
            <asp:Label runat="server" ID="errorLabel" CssClass="label label-danger"></asp:Label><br />       
            <asp:ValidationSummary runat="server" ValidationGroup="Validation" CssClass="alert alert-danger" />  
            <asp:RequiredFieldValidator runat="server" ValidationGroup="Validation" Display="None"           
                ErrorMessage="Treść wiadomości jest wymagana" ControlToValidate="fileUploadContent" />      
            <asp:RegularExpressionValidator runat="server" ControlToValidate="fileUploadContent"        
                ErrorMessage="Proszę wybrać plik .txt lub .html" ValidationGroup="Validation" Display="None"    
                ValidationExpression="(.*\.([Tt][Xx][Tt]|[Hh][Tt][Mm][Ll])$)"></asp:RegularExpressionValidator>  
                <div class="form-group">      
                    <asp:Label runat="server" CssClass="control-label col-md-3" AssociatedControlID="textboxTopic" Text="Temat wiadomości:" />        
    <div class="col-md-9">            
        <asp:TextBox runat="server" ID="textboxTopic" CssClass="form-control" />           

    </div>      

                </div>        
            <div class="form-group">    
                <asp:Label runat="server" CssClass="control-label col-md-3" AssociatedControlID="fileUploadContent" Text="Treść wiadomości" />                
                <div class="col-md-9">              
                    <asp:FileUpload runat="server" ID="fileUploadContent" CssClass="form-control" />      

                </div>            

            </div>       
            <div class="form-group">   
                <div class="col-md-offset-3 col-md-9">  
                    <asp:Button runat="server" ID="buttonSend" ValidationGroup="Validation" CssClass="btn btn-default" Text="Wyślij" OnClick="ButtonSend_OnClick" />     

                </div>       

            </div>     

        </div>   

    </div> 

</asp:content>
