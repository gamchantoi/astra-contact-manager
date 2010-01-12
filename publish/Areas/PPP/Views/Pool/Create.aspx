<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<ContactManager.Models.Pool>" %>

<%@ Import Namespace="ContactManager.Web.Helpers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <%= Html.ValidationSummary(Html.Resource("PPP_Resources, PPP_View_Pool_Create_ValidationSummary"))%>
    <% using (Html.BeginForm())
       {%>
    <fieldset class="fields">
        <legend>
            <%= Html.Resource("PPP_Resources, PPP_View_Pool_Create_CreateNewPool")%></legend>
        <p>
            <label for="Name">
                <%= Html.Resource("PPP_Resources, PPP_View_Pool_Create_Name")%></label>
            <%= Html.TextBox("Name") %>
            <%= Html.ValidationMessage("Name", "*") %>
        </p>
        <p>
            <label for="Addresses">
                <%= Html.Resource("PPP_Resources, PPP_View_Pool_Create_Addresses")%></label>
            <%= Html.TextBox("Addresses") %>
            <%= Html.ValidationMessage("Addresses", "*") %>
        </p>
        <p>
            <label for="NextPool">
                <%= Html.Resource("PPP_Resources, PPP_View_Pool_Create_NextPool")%></label>
            <%= Html.DropDownList("NextPool", (SelectList)ViewData["Pools"])%>
            <%= Html.ValidationMessage("NextPool", "*")%>
        </p>
        <p>
            <input type="submit" value='<%= Html.Resource("PPP_Resources, PPP_View_Pool_Create_Create")%>' />
        </p>
    </fieldset>
    <% } %>
    <div>
        <%=Html.ActionLink(Html.Resource("PPP_Resources, PPP_View_Pool_Create_BackToList"), "Index")%>
    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
