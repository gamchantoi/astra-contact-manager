<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<ContactManager.Models.Message>" %>
<%@ Import Namespace="ContactManager.Web.Helpers"%>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <% using (Html.BeginForm())
       {%>
    <fieldset>
        <legend><%= Html.Resource("Messages_Resources, Messages_View_Create_CreateNewMessage") %></legend>
        <p>
            <label for="MessageTypeId">
                <%= Html.Resource("Messages_Resources, Messages_View_Create_MessageTypeId")%></label>
            <%= Html.DropDownList("MessageTypeId", (SelectList)ViewData["List"])%>
            <%= Html.ValidationMessage("MessageTypeId", "*") %>
        </p>
        <p>
            <%= Html.Label(Html.Resource("Messages_Resources, Messages_View_Create_Title")) %>
            <%= Html.TextBoxFor(model => model.Title) %>
            <%= Html.ValidationMessageFor(model => model.Title) %>
        </p>
        <p>
            <%= Html.Label(Html.Resource("Messages_Resources, Messages_View_Create_Text"))%>
            <%= Html.TextAreaFor(model => model.Text) %>
            <%= Html.ValidationMessageFor(model => model.Text) %>
        </p>
        <p>
            <input type="submit" value=<%= Html.Resource("Messages_Resources, Messages_View_Create_Create")%> />
        </p>
    </fieldset>
    <% } %>
    <div>
        <% = Html.ActionLink(Html.Resource("Messages_Resources, Messages_View_Create_BackToList"), "Index") %>
    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
