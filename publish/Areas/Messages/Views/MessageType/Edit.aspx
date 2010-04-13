<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<ContactManager.Models.MessageType>" %>

<%@ Import Namespace="ContactManager.Web.Helpers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <script>
        $(document).ready(function() {
            $("#createForm").validate();
        });
    </script>

    <% using (Html.BeginForm("Edit", "MessageType", FormMethod.Post, new { id = "createForm" }))
       {%>
    <fieldset>
        <legend>
            <%= Html.Resource("Messages_Resources, Messages_View_MessageType_Edit_EditMessageType")%></legend>
        <%= Html.Hidden("TypeId", Model.TypeId)%>
        <p>
            <%= Html.Label(Html.Resource("Messages_Resources, Messages_View_MessageType_Edit_Name")) %>
            <%= Html.TextBoxFor(model => model.Name, new { @class = "required" })%>
            <%= Html.ValidationMessageFor(model => model.Name) %>
        </p>
        <p>
            <input type="submit" value='<%= Html.Resource("Messages_Resources, Messages_View_MessageType_Edit_Save")%>' />
        </p>
    </fieldset>
    <% } %>
    <div>
        <%=Html.ActionLink(Html.Resource("Messages_Resources, Messages_View_MessageType_Edit_BackToList"), "Index") %>
    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
