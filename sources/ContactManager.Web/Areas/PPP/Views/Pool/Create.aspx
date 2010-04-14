<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage<ContactManager.Models.Pool>" %>

<%@ Import Namespace="ContactManager.Web.Helpers" %>

    <script language="javascript" type="text/javascript">
        $(document).ready(function() {
            $("#createForm").validate();
        });
    </script>

    <%= Html.ValidationSummary(Html.Resource("PPP_Resources, PPP_View_Pool_Create_ValidationSummary"))%>
    <% using (Html.BeginForm("Create", "Pool", FormMethod.Post, new { id = "createForm" }))
       {%>
    <fieldset class="fields">
        <legend>
            <%= Html.Resource("PPP_Resources, PPP_View_Pool_Create_CreateNewPool")%></legend>
        <p>
            <label for="Name">
                <%= Html.Resource("PPP_Resources, PPP_View_Pool_Create_Name")%></label>
            <%= Html.TextBox("Name", "", new { @class = "required" })%>
            <%= Html.ValidationMessage("Name", "*") %>
        </p>
        <p>
            <label for="Addresses">
                <%= Html.Resource("PPP_Resources, PPP_View_Pool_Create_Addresses")%></label>
            <%= Html.TextBox("Addresses", "", new { @class = "required" })%>
            <%= Html.ValidationMessage("Addresses", "*") %>
        </p>
        <p>
            <label for="NextPool">
                <%= Html.Resource("PPP_Resources, PPP_View_Pool_Create_NextPool")%></label>
            <%= Html.DropDownList("NextPool", (SelectList)ViewData["Pools"])%>
            <%= Html.ValidationMessage("NextPool", "*")%>
        </p>
        <p>
            <input type="submit" value='<%= Html.Resource("PPP_Resources, PPP_View_Pool_Create_Create")%>' />
        </p>
    </fieldset>
    <% } %>