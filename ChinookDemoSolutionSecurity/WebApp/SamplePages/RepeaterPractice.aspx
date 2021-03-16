<%@ Page Title="Repeater Practice Homework" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RepeaterPractice.aspx.cs" Inherits="WebApp.SamplePages.RepeaterPractice" %>

<%@ Register Src="~/UserControls/MessageUserControl.ascx" TagPrefix="uc1" TagName="MessageUserControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Repeater Practice Homework</h1>
    <div class="row">
        <div class="offset-2">
            <uc1:MessageUserControl runat="server" ID="MessageUserControl" />
        </div>
    </div>
    <div class="row">
        <div class="offset-2">
            <asp:Repeater ID="TrackAlbumsList" runat="server" 
                DataSourceID="TrackAlbumsListODS" 
                ItemType="ChinookSystem.ViewModels.TrackAlbums">
                <HeaderTemplate>
                    <h3>Albums with 25 or More Tracks</h3>
                </HeaderTemplate>
                <ItemTemplate>
                    <br /><br />
                    <%# Item.Title %> by <%# Item.Artist %><br /><br />
                    <asp:Repeater ID="TracksList" runat="server" 
                        DataSource="<%# Item.Songs %>" 
                        ItemType="ChinookSystem.ViewModels.TrackDetails">
                        <ItemTemplate>
                            Song Name: <%# Item.Name %>&nbsp;&nbsp;
                            Length: <%# Item.Length %> seconds<br />
                        </ItemTemplate>
                    </asp:Repeater>
                </ItemTemplate>
            </asp:Repeater>
            <asp:ObjectDataSource ID="TrackAlbumsListODS" runat="server" 
                OldValuesParameterFormatString="original_{0}" 
                SelectMethod="Album_FindAlbumsWithOver25Songs" 
                OnSelected="SelectCheckForException"
                TypeName="ChinookSystem.BLL.AlbumController"></asp:ObjectDataSource>
        </div>
    </div>
</asp:Content>
