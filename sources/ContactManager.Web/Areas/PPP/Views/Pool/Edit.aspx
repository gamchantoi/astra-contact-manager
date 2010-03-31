<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage<ContactManager.Models.Pool>" %>

<%@ Import Namespace="ContactManager.Web.Helpers" %>

    <script>
        $(document).ready(function() {
            $("#createForm").validate();
        });
    </script>

<%= Html.ValidationSummary(Html.Resource("PPP_Resources, PPP_View_Pool_Edit_ValidationSummary"), new { @class = "ui-state-error ui-corner-all" })%>
<% using (Html.BeginForm("Edit", "Pool", FormMethod.Post, new { id = "createForm" }))
   {%>
<%= Html.Hidden("PoolId", Model.PoolId)%>
<%--<%= Html.Hidden("Name", Model.Name)%>--%>
<fieldset class="fields">
    <legend>
        <%= Html.Resource("PPP_Resources, PPP_View_Pool_Edit_EditPool")%><%=Html.Encode(Model.Name)%></legend>
    <p>
        <label for="Name">
            <%= Html.Resource("PPP_Resources, PPP_View_Pool_Edit_Name")%></label>
        <%= Html.TextBox("Name", Model.Name, new { @class = "required" })%>
        <%= Html.ValidationMessage("Name", "*")%>
    </p>
    <p>
        <label for="Addresses">
            <%= Html.Resource("PPP_Resources, PPP_View_Pool_Edit_Addresses")%></label>
        <%= Html.TextBox("Addresses", Model.Addresses, new { @class = "required" }) %>
        <%= Html.ValidationMessage("Addresses", "*") %>
    </p>
    <p>
        <label for="NextPool">
            <%= Html.Resource("PPP_Resources, PPP_View_Pool_Edit_NextPool")%></label>
        <%= Html.DropDownList("NextPool", (SelectList)ViewData["Pools"])%>
        <%= Html.ValidationMessage("NextPool", "*")%>
    </p>
    <p>
        <input type="submit" value='<%= Html.Resource("PPP_Resources, PPP_View_Pool_Edit_Save")%>' />
    </p>
</fieldset>
<% } %>
