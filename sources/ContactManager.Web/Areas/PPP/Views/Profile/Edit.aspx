<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage<ContactManager.Models.Profile>" %>

<%@ Import Namespace="ContactManager.Web.Helpers" %>
<%= Html.ValidationSummary(Html.Resource("PPP_Resources, PPP_View_Profile_Edit_ValidationSummary"))%>
<% using (Html.BeginForm())
   {%>
<%= Html.Hidden("ProfileId", Model.ProfileId)%>
<%--<%= Html.Hidden("Name", Model.Name)%>--%>
<fieldset class="fields">
    <legend>
        <%= Html.Resource("PPP_Resources, PPP_View_Profile_Edit_EditProfile")%>:
        <%=Html.Encode(Model.Name)%></legend>
    <p>
        <label for="Name">
            <%= Html.Resource("PPP_Resources, PPP_View_Profile_Edit_Name")%>:</label>
        <%= Html.TextBox("Name", Model.Name)%>
        <%= Html.ValidationMessage("Name", "*")%>
    </p>
    <p>
        <label for="DisplayName">
            <%= Html.Resource("PPP_Resources, PPP_View_Profile_Edit_DisplayName")%>:</label>
        <%= Html.TextBox("DisplayName", Model.DisplayName)%>
        <%= Html.ValidationMessage("DisplayName", "*")%>
    </p>
    <p>
        <label for="PoolId">
            <%= Html.Resource("PPP_Resources, PPP_View_Profile_Edit_Pool")%>:</label>
        <%= Html.DropDownList("PoolId", (SelectList)ViewData["Pools"])%>
        <%= Html.ValidationMessage("PoolId", "*")%>
    </p>
    <p>
        <label for="LocalAddress">
            <%= Html.Resource("PPP_Resources, PPP_View_Profile_Edit_LocalAddress")%>:</label>
        <%= Html.TextBox("LocalAddress", Model.LocalAddress) %>
        <%= Html.ValidationMessage("LocalAddress", "*") %>
    </p>
    <p>
        <label for="RateLimit">
            <%= Html.Resource("PPP_Resources, PPP_View_Profile_Edit_RateLimit")%>:</label>
        <%= Html.TextBox("RateLimit", Model.RateLimit) %>
        <%= Html.ValidationMessage("RateLimit", "*") %>
    </p>
    <p>
        <label for="Cost">
            <%= Html.Resource("PPP_Resources, PPP_View_Profile_Edit_Cost")%>:</label>
        <%= Html.TextBox("Cost", String.Format("{0:F}", Model.Cost)) %>
        <%= Html.ValidationMessage("Cost", "*") %>
    </p>
    <p>
        <label for="Comment">
            <%= Html.Resource("PPP_Resources, PPP_View_Profile_Edit_Comment")%>:</label>
        <%= Html.TextBox("Comment", Model.Comment)%>
        <%= Html.ValidationMessage("Comment", "*")%>
    </p>
    <p>
        <input type="submit" value='<%= Html.Resource("PPP_Resources, PPP_View_Profile_Edit_Save")%>' />
    </p>
</fieldset>
<% } %>
