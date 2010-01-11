<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>
<%@ Import Namespace="ContactManager.Web.Helpers"%>

<asp:Content ID="registerHead" ContentPlaceHolderID="head" runat="server">
    <title><%= Html.Resource("Web_Resources, Site_View_Account_Register_Title")%></title>
</asp:Content>

<asp:Content ID="registerContent" ContentPlaceHolderID="MainContent" runat="server">
    <p>
        <%= Html.Resource("Web_Resources, Site_View_Account_Register_P1")%> 
    </p>
    <p>
        <%= Html.Resource("Web_Resources, Site_View_Account_Register_P2")%>
    </p>
    <%= Html.ValidationSummary() %>

    <% using (Html.BeginForm()) { %>
        <div>
            <fieldset>
                <legend><%= Html.Resource("Web_Resources, Site_View_Account_Register_AccountInformation")%></legend>
                <p>
                    <label for="username"><%= Html.Resource("Web_Resources, Site_View_Account_Register_Username")%></label>
                    <%= Html.TextBox("username") %>
                    <%= Html.ValidationMessage("username") %>
                </p>
                <p>
                    <label for="email"><%= Html.Resource("Web_Resources, Site_View_Account_Register_Email")%></label>
                    <%= Html.TextBox("email") %>
                    <%= Html.ValidationMessage("email") %>
                </p>
                <p>
                    <label for="password"><%= Html.Resource("Web_Resources, Site_View_Account_Register_Password")%></label>
                    <%= Html.Password("password") %>
                    <%= Html.ValidationMessage("password") %>
                </p>
                <p>
                    <label for="confirmPassword"><%= Html.Resource("Web_Resources, Site_View_Account_Register_ConfirmPassword")%></label>
                    <%= Html.Password("confirmPassword") %>
                    <%= Html.ValidationMessage("confirmPassword") %>
                </p>
                <p class="submit">
                    <input type="submit" value=<%= Html.Resource("Web_Resources, Site_View_Account_Register_Register")%> />
                </p>
            </fieldset>
        </div>
    <% } %>
</asp:Content>
