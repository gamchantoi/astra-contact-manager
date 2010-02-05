<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<ContactManager.Users.ViewModels.ClientViewModel>" %>

<%@ Import Namespace="ContactManager.Web.Helpers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <table class="container">
        <tr>
            <td>
                <fieldset class="fields">
                    <legend>
                        <%= Html.Resource("Web_Resources, Site_View_Account_Index_UserData")%></legend>
                    <%=Html.BuildItem(Html.Resource("Web_Resources, Site_View_Account_Index_UserName"), Model.UserName)%>
                    <%=Html.BuildItem(Html.Resource("Web_Resources, Site_View_Account_Index_Email"), Model.Email)%>
                    <%=Html.BuildItem(Html.Resource("Web_Resources, Site_View_Account_Index_Status"), Model.StatusDisplayName)%>
                    <%=Html.BuildItem(Html.Resource("Web_Resources, Site_View_Account_Index_Balance"), Model.Balance.ToString("C"))%>
                    <%=Html.BuildItem(Html.Resource("Users_Resources, Users_User_Edit_Credit") + ":", Model.Credit.ToString("C"))%>
                    <%=Html.BuildItem(Html.Resource("Web_Resources, Site_View_Account_Index_TariffName"), Model.ProfileDisplayName)%>
                </fieldset>
            </td>
            <td>
                <%Html.RenderPartial("DetailsUserControl", Model); %>
            </td>
        </tr>
        <tr>
            <td>
                <%Html.RenderPartial("AddressUserControl", Model); %>
            </td>
            <td>
                <%Html.RenderPartial("ContractUserControl", Model); %>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
