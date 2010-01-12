<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<ContactManager.Models.Message>" %>
<%@ Import Namespace="ContactManager.Web.Helpers"%>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <fieldset>
        <legend><%= Html.Resource("Messages_Resources, Messages_View_Details_MessageDetails")%></legend>

            <%= Html.Hidden("MessageId",Model.MessageId) %>
        <p>
            <%= Html.Resource("Messages_Resources, Messages_View_Details_From")%>
            <%= Html.Encode(Model.Client.UserName) %>
            <%= Html.Resource("Messages_Resources, Messages_View_Details_To")%>
            <%= Html.Encode(Model.User.UserName) %>
        </p>
        <p>
            <%= Html.Resource("Messages_Resources, Messages_View_Details_MessageType")%>
            <%= Html.Hidden("MessageTypeId",Model.MessageTypeId) %>
            <%= Html.Encode(Model.MessageType.Name) %>
        </p>
        <p>
            <%= Html.Resource("Messages_Resources, Messages_View_Details_Title")%>
            <%= Html.Encode(Model.Title) %>
        </p>
        <p>
            <%= Html.Resource("Messages_Resources, Messages_View_Details_Text")%> 
            <%= Html.Encode(Model.Text) %>
        </p>
        <p>
            <%= Html.Resource("Messages_Resources, Messages_View_Details_StatusId")%>
            <%= Html.Encode(Model.StatusId) %>
        </p>
        <p>
            <%= Html.Resource("Messages_Resources, Messages_View_Details_Date")%>
            <%= Html.Encode(String.Format("{0:g}", Model.Date)) %>
        </p>
    </fieldset>
    <p>
        <%=Html.ActionLink(Html.Resource("Messages_Resources, Messages_View_Details_BackToList"), "Index")%>
    </p>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
