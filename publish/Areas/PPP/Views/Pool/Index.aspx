<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<ContactManager.Models.Pool>>" %>
<%@ Import Namespace="ContactManager.Web.Helpers"%>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <%= Html.ValidationSummary(Html.Resource("PPP_Resources, PPP_View_Pool_Index_Delete"))%>
    <table class="data-table" cellpadding="0" cellspacing="0">
        <thead>
            <tr>
                <th>
                    <%= Html.Resource("PPP_Resources, PPP_View_Pool_Index_Delete")%>
                </th>
                <th>
                    <%= Html.Resource("PPP_Resources, PPP_View_Pool_Index_Name")%>
                </th>
                <th>
                    <%= Html.Resource("PPP_Resources, PPP_View_Pool_Index_Addresses")%>
                </th>
                <th>
                    <%= Html.Resource("PPP_Resources, PPP_View_Pool_Index_NextPool")%>
                </th>
            </tr>
        </thead>
        <tbody>
        <% foreach (var item in Model)
           { %>
        
            <tr>
                <td>
                    <a href='<%= Url.Action("Delete", new {id=item.PoolId}) %>'><%= Html.Resource("PPP_Resources, PPP_View_Pool_Index_Delete")%></a>
                </td>
                <td>
                    <a href='<%= Url.Action("Edit", new {id=item.PoolId}) %>'>
                        <%= Html.Encode(item.Name) %></a>
                </td>
                <td>
                    <%= Html.Encode(item.Addresses) %>
                </td>
                <td>
                    <%= Html.Encode(item.NextPoolName) %>
                </td>
            </tr>
            <% } %>
        </tbody>
    </table>
    <p>
        <%= Html.ActionLink(Html.Resource("PPP_Resources, PPP_View_Pool_Index_CreateNew"), "Create") %> | 
        <%= Html.ActionLink(Html.Resource("PPP_Resources, PPP_View_Pool_Index_DeleteAll"), "DeleteAll")%>
    </p>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
