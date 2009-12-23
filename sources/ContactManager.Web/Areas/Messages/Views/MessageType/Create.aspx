<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<ContactManager.Models.MessageType>" %>
<%@ Import Namespace="ContactManager.Web.Helpers"%>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <% using (Html.BeginForm()) {%>

        <fieldset>
            <legend><%= Html.Resource("Messages_Resources, Messages_View_MessageType_Create_CreateMessageType")%></legend>
            <p>
                <%= Html.Label(Html.Resource("Messages_Resources, Messages_View_MessageType_Create_Name"))%>
                <%= Html.TextBoxFor(model => model.Name) %>
                <%= Html.ValidationMessageFor(model => model.Name) %>
            </p>
            <p>
                <input type="submit" value=<%= Html.Resource("Messages_Resources, Messages_View_MessageType_Create_Create")%> />
            </p>
        </fieldset>

    <% } %>

    <div>
        <%=Html.ActionLink(Html.Resource("Messages_Resources, Messages_View_MessageType_Create_BackToList"), "Index") %>
    </div>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>

