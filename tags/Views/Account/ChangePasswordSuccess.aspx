<%@Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>
<%@ Import Namespace="ContactManager.Web.Helpers"%>

<asp:Content ID="changePasswordSuccessHead" ContentPlaceHolderID="head" runat="server">
    <title><%= Html.Resource("Web_Resources, Site_View_Account_ChangePassword_Title")%></title>
</asp:Content>

<asp:Content ID="changePasswordSuccessContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%= Html.Resource("Web_Resources, Site_View_Account_ChangePassword_Title")%></h2>
    <p>
        <%= Html.Resource("Web_Resources, Site_View_Account_ChangePasswordSuccess_P1")%>
    </p>
</asp:Content>
