<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<ContactManager.Models.Profile>>" %>
<%@ Import Namespace="ContactManager.Web.Helpers"%>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <%= Html.ValidationSummary(Html.Resource("PPP_Resources, PPP_View_Profile_Index_ValidationSummary"))%>
    <table class="data-table" cellpadding="0" cellspacing="0">
        <thead>
            <tr>
                <th>
                    <%= Html.Resource("PPP_Resources, PPP_View_Profile_Index_Delete")%>
                </th>
                <th>
                    <%= Html.Resource("PPP_Resources, PPP_View_Profile_Index_Name")%>
                </th>
                <th>
                    <%= Html.Resource("PPP_Resources, PPP_View_Profile_Index_LocalAddress")%>
                </th>
                <th>
                    <%= Html.Resource("PPP_Resources, PPP_View_Profile_Index_Pool")%>
                </th>
                <th>
                    <%= Html.Resource("PPP_Resources, PPP_View_Profile_Index_RateLimit")%>
                </th>
                <th>
                    <%= Html.Resource("PPP_Resources, PPP_View_Profile_Index_Cost")%>
                </th>
            </tr>
        </thead>
        <tbody>
        <% foreach (var item in Model)
           { %>
            <tr>
                <td>
                    <a href='<%= Url.Action("Delete", new {id=item.ProfileId}) %>'>
                        <%= Html.Resource("PPP_Resources, PPP_View_Profile_Index_Delete")%></a>
                </td>
                <td>
                    <a href='<%= Url.Action("Edit", new {id=item.ProfileId}) %>'>
                        <%= Html.Encode(item.DisplayName) %></a>
                </td>
                <td>
                    <%= Html.Encode(item.LocalAddress) %>
                </td>
                <td>
                    <%= Html.Encode(item.PoolName) %>
                </td>
                <td>
                    <%= Html.Encode(item.RateLimit) %>
                </td>
                <td>
                    <%= Html.Encode(String.Format("{0:F}", item.Cost)) %>
                </td>
            </tr>        
        <% } %>
        </tbody>
    </table>
    <p>
        <%= Html.ActionLink(Html.Resource("PPP_Resources, PPP_View_Profile_Index_CreateNew"), "Create") %>    |
        <%= Html.ActionLink(Html.Resource("PPP_Resources, PPP_View_Profile_Index_DeleteProfiles"), "DeleteUnused")%>
    </p>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
