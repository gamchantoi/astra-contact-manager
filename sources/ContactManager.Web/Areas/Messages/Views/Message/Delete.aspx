<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<ContactManager.Models.Message>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <% using (Html.BeginForm())
       {%>
    <fieldset>
        <legend>Delete this Message ? </legend>
        <p>
            MessageId:
            <%= Html.Encode(Model.MessageId) %>
            <%= Html.Hidden("MessageId",Model.MessageId) %>
        </p>
        <p>
            From:
            <%= Html.Encode(Model.Client.UserName) %>
            
            To:
            <%= Html.Encode(Model.User.UserName) %>
        </p>
        <p>
            MessageType:
            <%= Html.Hidden("MessageTypeId",Model.MessageTypeId) %>
            <%= Html.Encode(Model.MessageType.Name) %>
        </p>
        <p>
            Title:
            <%= Html.Encode(Model.Title) %>
        </p>
        <p>
            Text:
            <%= Html.Encode(Model.Text) %>
        </p>
        <p>
            StatusId:
            <%= Html.Encode(Model.StatusId) %>
        </p>
        <p>
            Date:
            <%= Html.Encode(String.Format("{0:g}", Model.Date)) %>
        </p>
        <p>
            <input type="submit" value="Delete" />
        </p>
    </fieldset>
    <% } %>
    <p>
        <%=Html.ActionLink("Back to List", "Index") %>
    </p>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
