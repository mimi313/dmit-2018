<%@ Page Title="Player Parent OLTP" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PlayerParentOLTP.aspx.cs" Inherits="PracticeForExam.PracticePages.PlayerParentOLTP" %>

<%@ Register Src="~/UserControls/MessageUserControl.ascx" TagPrefix="uc1" TagName="MessageUserControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <div class="offset-2">
    <h1>Player Parent OLTP</h1>
    <uc1:MessageUserControl runat="server" id="MessageUserControl" />
            </div>
    </div>

        <div class="row">
        <div class="offset-2">
                <asp:Label ID="Label1" runat="server" Text="Gardian First Name"></asp:Label>
    <asp:TextBox ID="GardianFirstName" runat="server"></asp:TextBox><br />
    <asp:Label ID="Label2" runat="server" Text="Gardian Last Name"></asp:Label>
    <asp:TextBox ID="GardianLastName" runat="server"></asp:TextBox><br />
    <asp:Label ID="Label3" runat="server" Text="Emargency Phone Number"></asp:Label>
    <asp:TextBox ID="EmargencyPhoneNumber" runat="server"></asp:TextBox><br />
    <asp:Label ID="Label4" runat="server" Text="Email Address"></asp:Label>
    <asp:TextBox ID="EmailAddress" runat="server"></asp:TextBox>
    <br />
    <br />
            </div>
    </div>


        <div class="row">
        <div class="offset-2">
                <asp:Label ID="Label5" runat="server" Text="Team Name"></asp:Label>
    <asp:TextBox ID="TeamName" runat="server"></asp:TextBox><br />
    <asp:Label ID="Label6" runat="server" Text="First Name"></asp:Label>
    <asp:TextBox ID="FirstName" runat="server"></asp:TextBox><br />
    <asp:Label ID="Label7" runat="server" Text="Last Name"></asp:Label>
    <asp:TextBox ID="LastName" runat="server"></asp:TextBox><br />
    <asp:Label ID="Label8" runat="server" Text="Age"></asp:Label>
    <asp:TextBox ID="Age" runat="server"></asp:TextBox><br />
    <asp:Label ID="Label11" runat="server" Text="Gender"></asp:Label>
    <asp:RadioButtonList ID="Gender" runat="server">
        <asp:ListItem Value="M">Male</asp:ListItem>
        <asp:ListItem Value="F">Femal</asp:ListItem>
    </asp:RadioButtonList><br />
    <asp:Label ID="Label9" runat="server" Text="Alberta Health Care Number"></asp:Label>
    <asp:TextBox ID="AHCN" runat="server"></asp:TextBox><br />
    <asp:Label ID="Label10" runat="server" Text="Medical Aleart Details"></asp:Label>
    <asp:TextBox ID="MedicalAleart" runat="server"></asp:TextBox><br />
            </div>
    </div>


    <br />
            <div class="row">
        <div class="offset-2">
            <asp:Button ID="Register" runat="server" Text="Register" OnClick="Register_Click" />&nbsp;&nbsp;
            <asp:Button ID="Clear" runat="server" Text="Clear" OnClientClick="return confirm('Are you sure you want to clear all forms?')" OnClick="Clear_Click" /> 
                        </div>
    </div>
</asp:Content>
