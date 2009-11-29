<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<ContactManager.Models.Client>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<%--<script src="Scripts/PaggingGrid.js" type="text/javascript"></script>--%>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <%=Html.Encode(ViewData["Message"])%>
    <%= Html.ValidationSummary("Operation was unsuccessful. Please view details and try again.")%>
    <% var deleted = ViewData["Deleted"].ToString().Equals("True"); %>
    
    <table class="data-table" cellpadding="0" cellspacing="0">
        <thead>
            <tr>
                <th style="width: 40px;">
                    <a href='<%= Url.Action("Index", new {deleted=!deleted}) %>'><%=Html.Encode(deleted ? "Active" : "Deleted")%></a>
                </th>
                <th style="width: 40px;">
                    Money
                </th>
                <th>
                    UserName(<%=Html.Encode(ViewData["TotalUsers"])%>)
                </th>
                <th>
                    FullName
                </th>
                <th>
                    Tariff
                </th>
                <th>
                    Services
                </th>
                <th>
                    Balance(<%=Html.Encode(ViewData["TotalBalance"])%>)
                </th>
                <th style="width: 40px;">
                    Status
                </th>
            </tr>
        </thead>
        <tbody>
        <% foreach (var item in Model) { %>
            <tr class="<%= Html.Encode(item.SecretStatus)%> <%= Html.Encode(item.Role) %>">
                <td>
                    <a href='<%= Url.Action("Status", new {id=item.UserId, status=deleted}) %>'><%=Html.Encode(deleted ? "Activate" : "Delete")%></a>
                </td>
                <td>
                    <a href='<%= Url.Action("Load", new {id=item.UserId}) %>'>Load</a>
                </td>
                <td>
                    <a href='<%= Url.Action("Edit", new {id=item.UserId}) %>'><%= Html.Encode(item.UserName) %></a>
                </td>
                <td>
                    <%= Html.Encode(item.FullName)%>
                </td>
                <td>
                    <%= Html.Encode(item.ProfileName) %>
                </td> 
                <td>
                    <a href='<%= Url.Action("Services", new {id=item.UserId}) %>'>View</a>
                </td>
                <td>
                    <%= Html.Encode(String.Format("{0:F}", item.Balance)) %>
                </td>
                <td>
                <%if (item.mkt_PPPSecretsReference.Value != null)
                  {%>
                    <%= Html.Encode(item.mkt_PPPSecretsReference.Value.Status == 1 ? "Active" : "Disabled")%>
                    <%} %>
                </td>
            </tr>
        
           <%} %>
       </tbody>

    </table>
    <%--<div id="topic-grid"></div>--%>
    <p>
        <% if (!deleted)
           {%>
        <%= Html.ActionLink("Create New", "Create")%>&nbsp;|&nbsp;        
        <%= Html.ActionLink("Delete All Users", "DeleteAll")%>        
        <%} %>        
    </p>
    <div style='text-align:right;'>
        <%= Html.ActionLink("Clear database", "ClearAllData")%>
        </div>
</asp:Content>

