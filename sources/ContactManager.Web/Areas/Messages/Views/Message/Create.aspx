<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage<ContactManager.Models.Message>" %>

<%@ Import Namespace="ContactManager.Web.Helpers" %>

<script>
    $(document).ready(function() {
        $("#createForm").validate();
    });
</script>

<% using (Html.BeginForm("Create", "Message", FormMethod.Post, new { id = "createForm" }))
   {%>
<fieldset>
    <legend>
        <%= Html.Resource("Messages_Resources, Messages_View_Create_CreateNewMessage")%></legend>
    <p>
        <label for="MessageTypeId">
            <%= Html.Resource("Messages_Resources, Messages_View_Create_MessageTypeId")%></label>
        <%= Html.DropDownList("MessageTypeId", (SelectList)ViewData["List"])%>
        <%= Html.ValidationMessage("MessageTypeId", "*")%>
    </p>
    <p>
        <%= Html.Label(Html.Resource("Messages_Resources, Messages_View_Create_Title"))%>
        <%= Html.TextBoxFor(model => model.Title, new { @class = "required" })%>
        <%= Html.ValidationMessageFor(model => model.Title)%>
    </p>
    <p>
        <%= Html.Label(Html.Resource("Messages_Resources, Messages_View_Create_Text"))%>
        <%= Html.TextAreaFor(model => model.Text, new { @class = "required" })%>
        <%= Html.ValidationMessageFor(model => model.Text)%>
    </p>
    <p>
        <input type="submit" value='<%= Html.Resource("Messages_Resources, Messages_View_Create_Create")%>' />
    </p>
</fieldset>
<% } %>
