<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<ContactManager.Services.ViewModels.ClientServicesViewModel>" %>
<%@ Import Namespace="ContactManager.Web.Helpers"%>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <% using (Html.BeginForm())
       {%>
    <%= Html.Hidden("UserId", ViewData["UserId"])%>
    <fieldset>
        <legend><%= Html.Resource("Services_Resources, Services_View_ClientServices_Services")%></legend>
        <table>
            <% foreach (var item in Model.ListServices)
               { %>
            <tr>
                <td>
                    <label for="<%= Html.Encode(item.ServiceId) %>">
                        <%= Html.Encode(item.Name)%>:</label>
                    <%= Html.CheckBox(item.ServiceId.ToString(), Model.UserServices.Where(s => s.ServiceId == item.ServiceId).Count() > 0)%>
                </td>
            </tr>
            <% } %>
        </table>
        <p>
            <input type="submit" value=<%= Html.Resource("Services_Resources, Services_View_ClientServices_Save")%> />
        </p>
    </fieldset>
    <%} %>
    <div>
        <%=Html.ActionLink(Html.Resource("Services_Resources, Services_View_ClientServices_BackToList"), "Index", "User", new {Area="Users"},null) %>
    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
