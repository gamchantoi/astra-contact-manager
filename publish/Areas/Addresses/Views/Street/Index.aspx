<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<ContactManager.Models.Street>>" %>
<%@ Import Namespace="ContactManager.Web.Helpers"%>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <table>
        <tr>
            <th></th>

            <th>
                <%= Html.Resource("Addresses_Resources, Addresses_Street_Index_Name")%>
            </th>
            <th>
                <%= Html.Resource("Addresses_Resources, Addresses_Street_Index_Tag")%>
            </th>
        </tr>

    <% foreach (var item in Model) { %>
    
        <tr>
            <td>
                <%= Html.ActionLink(Html.Resource("Addresses_Resources, Addresses_Street_Index_Edit"), "Edit", new { id=item.StreetId }) %>
               
            </td>
            <td>
                <%= Html.Encode(item.Name) %>
            </td>
            <td>
                <%= Html.Encode(item.Tag) %>
            </td>
        </tr>
    
    <% } %>

    </table>

    <p>
        <%= Html.ActionLink(Html.Resource("Addresses_Resources, Addresses_Street_Index_CreateNew"), "Create") %>
    </p>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>

