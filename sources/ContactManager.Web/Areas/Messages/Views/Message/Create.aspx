<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<ContactManager.Models.Message>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <% using (Html.BeginForm())
       {%>
    <fieldset>
        <legend>Create new message</legend>
        <%--            <p>
                <%= Html.LabelFor(model => model.MessageId) %>
                <%= Html.TextBoxFor(model => model.MessageId) %>
                <%= Html.ValidationMessageFor(model => model.MessageId) %>
            </p>--%>
        <p>
            <label for="MessageTypeId">
                MessageTypeId:</label>
            <%= Html.DropDownList("MessageTypeId", (SelectList)ViewData["List"])%>
            <%= Html.ValidationMessage("MessageTypeId", "*") %>
        </p>
        <p>
            <%= Html.LabelFor(model => model.Title) %>
            <%= Html.TextBoxFor(model => model.Title) %>
            <%= Html.ValidationMessageFor(model => model.Title) %>
        </p>
        <p>
            <%= Html.LabelFor(model => model.Text) %>
            <%= Html.TextAreaFor(model => model.Text) %>
            <%= Html.ValidationMessageFor(model => model.Text) %>
        </p>
        <%--        <p>
            <%= Html.LabelFor(model => model.Date) %>
            <%= Html.TextBoxFor(model => model.Date) %>
            <%= Html.ValidationMessageFor(model => model.Date) %>
        </p>--%>
        <%--        <p>
            <%= Html.LabelFor(model => model.StatusId) %>
            <%= Html.TextBoxFor(model => model.StatusId) %>
            <%= Html.ValidationMessageFor(model => model.StatusId) %>
        </p>--%>
        <p>
            <input type="submit" value="Create" />
        </p>
    </fieldset>
    <% } %>
    <div>
        <%=Html.ActionLink("Back to List", "Index") %>
    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
