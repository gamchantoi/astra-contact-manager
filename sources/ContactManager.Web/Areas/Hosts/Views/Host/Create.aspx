<%@ Page Title="" Language="C#"  Inherits="System.Web.Mvc.ViewPage<ContactManager.Models.Host>" %>
<%@ Import Namespace="ContactManager.Web.Helpers"%>

    <%= Html.ValidationSummary( Html.Resource("Hosts_Resources, Hosts_View_Create_ValidationSummary")) %>
    
    <% using (Html.BeginForm()) {%>

        <fieldset>
            <legend><%= Html.Resource("Hosts_Resources, Hosts_View_Create_CreateNewHost") %></legend>
            <p>
                <label for="Address"><%= Html.Resource("Hosts_Resources, Hosts_View_Create_Address") %></label>
                <%= Html.TextBox("Address") %>
                <%= Html.ValidationMessage("Address", "*") %>
            </p>
            <p>
                <label for="UserName"><%= Html.Resource("Hosts_Resources, Hosts_View_Create_UserName") %></label>
                <%= Html.TextBox("UserName") %>
                <%= Html.ValidationMessage("UserName", "*") %>
            </p>
            <p>
                <label for="UserPassword"><%= Html.Resource("Hosts_Resources, Hosts_View_Create_UserPassword") %></label>
                <%= Html.TextBox("UserPassword") %>
                <%= Html.ValidationMessage("UserPassword", "*") %>
            </p>
            <p>
                <%--<input type="submit" value=<%= Html.Resource("Hosts_Resources, Hosts_View_Create_Create")%> />--%>
                 <input onclick="SubmitHost('<%= Url.Content("~/Hosts/Host/Create")%>');"
            type="button" value="<%= Html.Resource("Hosts_Resources, Hosts_View_Create_Create")%>" />
            </p>
        </fieldset>

    <% } %>
