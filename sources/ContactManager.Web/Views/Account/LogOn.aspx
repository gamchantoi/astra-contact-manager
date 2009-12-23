<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>
<%@ Import Namespace="ContactManager.Web.Helpers"%>

<asp:Content ID="loginHead" ContentPlaceHolderID="head" runat="server">
    <title><%= Html.Resource("Web_Resources, Site_View_Account_LogOn_Title")%></title>
</asp:Content>

<asp:Content ID="loginContent" ContentPlaceHolderID="MainContent" runat="server">
    <%= Html.ValidationSummary() %>

    <% using (Html.BeginForm()) { %>
        <div>
            <fieldset>
                <legend><%= Html.Resource("Web_Resources, Site_View_Account_LogOn_AccountInformation")%></legend>
                <p>
                    <label for="username"><%= Html.Resource("Web_Resources, Site_View_Account_LogOn_Username")%></label>
                    <%= Html.TextBox("username") %>
                    <%= Html.ValidationMessage("username") %>
                </p>
                <p>
                    <label for="password"><%= Html.Resource("Web_Resources, Site_View_Account_LogOn_Password")%></label>
                    <%= Html.Password("password") %>
                    <%= Html.ValidationMessage("password") %>
                </p>
                <p style="display:none;">
                    <%= Html.CheckBox("rememberMe") %> <label class="inline" for="rememberMe"><%= Html.Resource("Web_Resources, Site_View_Account_LogOn_RememberMe")%></label>
                </p>
                <p class="submit">
                    <input type="submit" value=<%= Html.Resource("Web_Resources, Site_View_Account_LogOn_Button_LogOn") %> />
                </p>
            </fieldset>
        </div>
    <% } %>
</asp:Content>
