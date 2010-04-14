<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage<ContactManager.Models.Street>" %>

<%@ Import Namespace="ContactManager.Web.Helpers" %>

<script language="javascript" type="text/javascript">
    $(document).ready(function() {
        $("#createForm").validate();
    });
</script>

<% using (Html.BeginForm("Edit", "Street", FormMethod.Post, new { id = "createForm" }))
   {%>
<fieldset>
    <legend>
        <%= Html.Resource("Addresses_Resources, Addresses_Street_Edit_EditStreet")%></legend>
    <%--            <p>
                <%= Html.LabelFor(model => model.StreetId) %>
                <%= Html.TextBoxFor(model => model.StreetId) %>
                <%= Html.ValidationMessageFor(model => model.StreetId) %>
            </p>--%>
    <%= Html.Hidden("StreetId", Model.StreetId) %>
    <p>
        <%= Html.Label(Html.Resource("Addresses_Resources, Addresses_Street_Create_Name"))%>
        <%= Html.TextBoxFor(model => model.Name, new { @class = "required" })%>
        <%= Html.ValidationMessageFor(model => model.Name) %>
    </p>
    <p>
        <%= Html.Label(Html.Resource("Addresses_Resources, Addresses_Street_Create_Tag")) %>
        <%= Html.TextBoxFor(model => model.Tag, new { @class = "required" })%>
        <%= Html.ValidationMessageFor(model => model.Tag) %>
    </p>
    <p>
        <input type="submit" value='<%= Html.Resource("Addresses_Resources, Addresses_Street_Edit_Save") %>' />
    </p>
</fieldset>
<% } %>
