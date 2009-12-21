<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<ContactManager.Services.ViewModels.ClientServicesViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <% using (Html.BeginForm())
       {%>
    <%= Html.Hidden("UserId", ViewData["UserId"])%>
    <fieldset>
        <legend>Services</legend>
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
            <input type="submit" value="Save" />
        </p>
    </fieldset>
    <%} %>
    <div>
        <%=Html.ActionLink("Back to List", "Index", "User", new {Area="Users"},null) %>
    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
