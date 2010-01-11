<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<%= Html.ValidationSummary("Synchronization Status:") %>
    <fieldset>
        <legend>Synchronization with <%= Html.Encode(ViewData["HostName"])%></legend>
        <p>
        <%= Html.ActionLink("Synchronize From Host", "SyncFromHost") %>
        </p>
        <p>
        <%= Html.ActionLink("Synchronize To Host", "SyncToHost")%>
        </p>
    </fieldset>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
