<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<ContactManager.Models.Address>>" %>
<%@ Import Namespace="ContactManager.Web.Helpers"%>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <table class="data-table" cellpadding="0" cellspacing="0">
        <tr>
            <th></th>
            <th>
                <%= Html.Resource("Addresses_Resources, Addresses_View_Index_City")%>
            </th>
            <th>
                <%= Html.Resource("Addresses_Resources, Addresses_View_Index_Building")%>
            </th>
            <th>
                <%= Html.Resource("Addresses_Resources, Addresses_View_Index_Details")%>
            </th>
            <th>
                <%= Html.Resource("Addresses_Resources, Addresses_View_Index_Room")%>
            </th>
        </tr>

    <% foreach (var item in Model) { %>
    
        <tr>
            <td>
                <%= Html.ActionLink(Html.Resource("Addresses_Resources, Addresses_View_Index_Edit"), "Edit", new { id = item.AddressId })%> |
                <%= Html.ActionLink(Html.Resource("Addresses_Resources, Addresses_View_Index_Details"), "Details", new { id = item.AddressId })%>
            </td>
            <td>
                <%= Html.Encode(item.City) %>
            </td>
            <td>
                <%= Html.Encode(item.Building) %>
            </td>
            <td>
                <%= Html.Encode(item.Details) %>
            </td>
            <td>
                <%= Html.Encode(item.Room) %>
            </td>
        </tr>
    
    <% } %>

    </table>

    <p>
        <%= Html.ActionLink(Html.Resource("Addresses_Resources, Addresses_View_Index_CreateNew"), "Create")%>
    </p>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>

