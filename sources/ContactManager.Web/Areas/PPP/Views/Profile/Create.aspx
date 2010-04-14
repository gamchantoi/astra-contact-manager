<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage<ContactManager.Models.Profile>" %>

<%@ Import Namespace="ContactManager.Web.Helpers" %>


    <script language="javascript" type="text/javascript">
        $(document).ready(function() {
            $("#createForm").validate();
        });
    </script>

    <%= Html.ValidationSummary(Html.Resource("PPP_Resources, PPP_View_Profile_Create_ValidationSummary"))%>
    <% using (Html.BeginForm("Create", "Profile", FormMethod.Post, new { id = "createForm" }))
       {%>
    <fieldset class="fields">
        <legend>
            <%= Html.Resource("PPP_Resources, PPP_View_Profile_Create_CreateNewProfile")%></legend>
        <p>
            <label for="Name">
                <%= Html.Resource("PPP_Resources, PPP_View_Profile_Create_Name")%>:</label>
            <%= Html.TextBox("Name", "", new { @class = "required" })%>
            <%= Html.ValidationMessage("Name", "*") %>
        </p>
        <p>
            <label for="DisplayName">
                <%= Html.Resource("PPP_Resources, PPP_View_Profile_Create_DisplayName")%>:</label>
            <%= Html.TextBox("DisplayName", "", new { @class = "required" })%>
            <%= Html.ValidationMessage("DisplayName", "*")%>
        </p>
        <p>
            <label for="LocalAddress">
                <%= Html.Resource("PPP_Resources, PPP_View_Profile_Create_LocalAddress")%>:</label>
            <%= Html.TextBox("LocalAddress", "", new { @class = "required" })%>
            <%= Html.ValidationMessage("LocalAddress", "*") %>
        </p>
        <p>
            <label for="PoolId">
                <%= Html.Resource("PPP_Resources, PPP_View_Profile_Create_Pool")%>:</label>
            <%= Html.DropDownList("PoolId", (SelectList)ViewData["Pools"])%>
            <%= Html.ValidationMessage("PoolId", "*")%>
        </p>
        <p>
            <label for="RateLimit">
                <%= Html.Resource("PPP_Resources, PPP_View_Profile_Create_RateLimit")%>:</label>
            <%= Html.TextBox("RateLimit", "", new { @class = "required" })%>
            <%= Html.ValidationMessage("RateLimit", "*") %>
        </p>
        <p>
            <label for="Cost">
                <%= Html.Resource("PPP_Resources, PPP_View_Profile_Create_Cost")%>:</label>
            <%= Html.TextBox("Cost", "", new { @class = "required" })%>
            <%= Html.ValidationMessage("Cost", "*") %>
        </p>
        <p>
            <label for="Comment">
                <%= Html.Resource("PPP_Resources, PPP_View_Profile_Create_Comment")%>:</label>
            <%= Html.TextBox("Comment", "", new { @class = "required" })%>
            <%= Html.ValidationMessage("Comment", "*")%>
        </p>
        <p>
            <input type="submit" value='<%= Html.Resource("PPP_Resources, PPP_View_Profile_Create_Create")%>' />
        </p>
    </fieldset>
    <% } %>