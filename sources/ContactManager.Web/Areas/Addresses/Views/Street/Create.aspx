<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage<ContactManager.Models.Street>" %>

<%@ Import Namespace="ContactManager.Web.Helpers" %>

<script>
    $(document).ready(function() {
        $("#createForm").validate();
    });
</script>

<% using (Html.BeginForm("Create", "Street", FormMethod.Post, new { id = "createForm" }))
   {%>
<fieldset>
    <legend>
        <%= Html.Resource("Addresses_Resources, Addresses_Street_Create_CreateStreet")%></legend>
    <p>
        <%= Html.Label(Html.Resource("Addresses_Resources, Addresses_Street_Create_Name")) %>
        <%= Html.TextBoxFor(model => model.Name, new { @class = "required" })%>
        <%= Html.ValidationMessageFor(model => model.Name) %>
    </p>
    <p>
        <%= Html.Label(Html.Resource("Addresses_Resources, Addresses_Street_Create_Tag"))%>
        <%= Html.TextBoxFor(model => model.Tag, new { @class = "required" })%>
        <%= Html.ValidationMessageFor(model => model.Tag) %>
    </p>
    <p>
        <input onclick="SubmitStreet('<%= Url.Content("~/Addresses/Street/Create")%>');"
            type="button" value="<%= Html.Resource("Addresses_Resources, Addresses_Street_Create_Create")%>" />
    </p>
</fieldset>
<% } %>
