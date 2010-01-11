<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>
<%@ Import Namespace="ContactManager.Web.Helpers"%>

<asp:Content ID="changePasswordHead" ContentPlaceHolderID="head" runat="server">
    <title><%= Html.Resource("Web_Resources, Site_View_Account_ChangePassword_Title")%></title>
</asp:Content>

<asp:Content ID="changePasswordContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%= Html.Resource("Web_Resources, Site_View_Account_ChangePassword_Title")%></h2>
    <p>
        <%= Html.Resource("Web_Resources, Site_View_Account_ChangePassword_P1")%> 
    </p>
    <p>
        <%= Html.Resource("Web_Resources, Site_View_Account_ChangePassword_P2")%><%--New passwords are required to be a minimum of <%=Html.Encode(ViewData["PasswordLength"])%> characters in length.--%>
    </p>
    <%= Html.ValidationSummary() %>

    <% using (Html.BeginForm()) { %>
        <div>
            <fieldset>
                <legend><%= Html.Resource("Web_Resources, Site_View_Account_ChangePassword_AccountInformation")%></legend>
                <p>
                    <label for="currentPassword"><%= Html.Resource("Web_Resources, Site_View_Account_ChangePassword_CurrentPassword")%></label>
                    <%= Html.Password("currentPassword") %>
                    <%= Html.ValidationMessage("currentPassword") %>
                </p>
                <p>
                    <label for="newPassword"><%= Html.Resource("Web_Resources, Site_View_Account_ChangePassword_NewPassword")%></label>
                    <%= Html.Password("newPassword") %>
                    <%= Html.ValidationMessage("newPassword") %>
                </p>
                <p>
                    <label for="confirmPassword"><%= Html.Resource("Web_Resources, Site_View_Account_ChangePassword_ConfirmNewPassword")%></label>
                    <%= Html.Password("confirmPassword") %>
                    <%= Html.ValidationMessage("confirmPassword") %>
                </p>
                <p>
                    <input type="submit" value=<%= Html.Resource("Web_Resources, Site_View_Account_ChangePassword_ChangePassword")%> />
                </p>
            </fieldset>
        </div>
    <% } %>
</asp:Content>
