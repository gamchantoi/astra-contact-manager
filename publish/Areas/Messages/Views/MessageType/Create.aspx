<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage<ContactManager.Models.MessageType>" %>

<%@ Import Namespace="ContactManager.Web.Helpers" %>

<script>
    $(document).ready(function() {
        $("#createForm").validate();
    });
</script>

<% using (Html.BeginForm("Create", "MessageType", FormMethod.Post, new { id = "createForm" }))
   {%>
<fieldset>
    <legend>
        <%= Html.Resource("Messages_Resources, Messages_View_MessageType_Create_CreateMessageType")%></legend>
    <p>
        <%= Html.Label(Html.Resource("Messages_Resources, Messages_View_MessageType_Create_Name"))%>
        <%= Html.TextBoxFor(model => model.Name, new { @class = "required" })%>
        <%= Html.ValidationMessageFor(model => model.Name) %>
    </p>
    <p>
        <input type="submit" value='<%= Html.Resource("Messages_Resources, Messages_View_MessageType_Create_Create")%>' />
    </p>
</fieldset>
<% } %>
