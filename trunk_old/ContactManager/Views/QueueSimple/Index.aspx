<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<ContactManager.Models.QueueSimple>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <%=Html.Encode(ViewData["Message"])%>
    <%= Html.ValidationSummary("Action was unsuccessful. Please view details and try again.")%>
   <table class="data-table" cellpadding="0" cellspacing="0">
   <thead>
        <tr>            
            <th>Edit</th>
            <th>Delete</th>
            <th>
                QueueName
            </th>
            <th>
                Comment
            </th>
            <th>
                TargetAddress
            </th>
        </tr>
    </thead>
    <tbody>
    <% foreach (var item in Model) { %>
    
        <tr>
            <td>
                <a href='<%= Url.Action("Edit", new {id=item.QueueId}) %>'><img src="Content/Edit.png" alt="Edit" /></a>
            </td>
            <td>
                <a href='<%= Url.Action("Delete", new {id=item.QueueId}) %>'><img src="Content/Delete.png" alt="Delete" /></a>
            </td>
            <td>
                <%= Html.Encode(item.QueueName) %>
            </td>
            <td>
                <%= Html.Encode(item.Comment) %>
            </td>
            <td>
                <%= Html.Encode(item.TargetAddress) %>
            </td>
        </tr>
    
    <% } %>
    </tbody>
    </table>

    <p>
        <%= Html.ActionLink("Create New", "Create") %>
    </p>
    <p>
    <%= Html.ActionLink("Delete All free", "DeleteAll") %>
    </p>
</asp:Content>

