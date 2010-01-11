<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<ContactManager.Models.Message>" %>
<%@ Import Namespace="ContactManager.Web.Helpers"%>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <% using (Html.BeginForm())
       {%>
    <fieldset>
        <legend><%= Html.Resource("Messages_Resources, Messages_View_Delete_DeleteThisMessage")%></legend>
            <%= Html.Hidden("MessageId",Model.MessageId) %>
        <p>
            <%= Html.Resource("Messages_Resources, Messages_View_Delete_From")%>
            <%= Html.Encode(Model.Client.UserName) %>
            
            <%= Html.Resource("Messages_Resources, Messages_View_Delete_To")%>
            <%= Html.Encode(Model.User.UserName) %>
        </p>
        <p>
            <%= Html.Resource("Messages_Resources, Messages_View_Delete_MessageType")%>
            <%= Html.Hidden("MessageTypeId",Model.MessageTypeId) %>
            <%= Html.Encode(Model.MessageType.Name) %>
        </p>
        <p>
            <%= Html.Resource("Messages_Resources, Messages_View_Delete_Title")%>
            <%= Html.Encode(Model.Title) %>
        </p>
        <p>
            <%= Html.Resource("Messages_Resources, Messages_View_Delete_Text")%>
            <%= Html.Encode(Model.Text) %>
        </p>
        <p>
            <%= Html.Resource("Messages_Resources, Messages_View_Delete_StatusId")%>
            <%= Html.Encode(Model.StatusId) %>
        </p>
        <p>
            <%= Html.Resource("Messages_Resources, Messages_View_Delete_Date")%>
            <%= Html.Encode(String.Format("{0:g}", Model.Date)) %>
        </p>
        <p>
            <input type="submit" value=<%= Html.Resource("Messages_Resources, Messages_View_Delete_Delete") %> />
        </p>
    </fieldset>
    <% } %>
    <p>
        <%=Html.ActionLink(Html.Resource("Messages_Resources, Messages_View_Delete_BackToList"), "Index")%>
    </p>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
