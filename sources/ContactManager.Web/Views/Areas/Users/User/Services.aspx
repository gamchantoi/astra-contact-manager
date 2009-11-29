<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<ContactManager.Models.Service>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<%var userServices = (List<ContactManager.Models.ClientServiceActivitiy>)ViewData["ClientServices"]; %>
    <% using (Html.BeginForm("Services", "User"))
       {%>
    <%= Html.Hidden("UserId", ViewData["UserId"])%>
    <fieldset>
        <legend>Services</legend>
        <% foreach (var item in Model)
           { %>
        <p>
            <label for="<%= Html.Encode(item.ServiceId) %>">
                <%= Html.Encode(item.Name)%>:</label>
            <%= Html.CheckBox(item.ServiceId.ToString(), userServices.Where(a => a.ServiceId == item.ServiceId).FirstOrDefault() != null)%>
            <%= Html.Encode(item.Comment)%>
        </p>
        <% } %>
        <p>
            <input type="submit" value="Save" />
        </p>
    </fieldset>
    <%} %>
    <div>
        <%=Html.ActionLink("Back to List", "Index") %>
    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
