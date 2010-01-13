<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<ContactManager.Models.Client>" %>

<%@ Import Namespace="ContactManager.Web.Helpers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <fieldset class="fields">
        <legend>
            <%= Html.Resource("Web_Resources, Site_View_Account_Index_UserData")%></legend>
        <p>
            <%= Html.Resource("Web_Resources, Site_View_Account_Index_UserName")%>
            <%= Html.Encode(Model.UserName) %>
        </p>
        <p>
            <%= Html.Resource("Web_Resources, Site_View_Account_Index_Email")%>
            <%= Html.Encode(Model.Email) %>
        </p>
        <p>
            <%= Html.Resource("Web_Resources, Site_View_Account_Index_Status")%>
            <%= Html.Encode(Model.Status.DisplayName) %>
        </p>
        <p>
            <%= Html.Resource("Web_Resources, Site_View_Account_Index_Balance")%>
            <%= Html.Encode(String.Format("{0:F}", Model.Balance)) %>
        </p>
        <p>
            <%= Html.Resource("Web_Resources, Site_View_Account_Index_FullName")%>
            <%= Html.Encode(Model.GetFullName()) %>
        </p>
        <p>
            <%= Html.Resource("Web_Resources, Site_View_Account_Index_TariffName")%>
            <%= Html.Encode(Model.GetProfileName()) %>
        </p>
    </fieldset>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
