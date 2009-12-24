<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<ContactManager.Models.Profile>" %>
<%@ Import Namespace="ContactManager.Web.Helpers"%>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <%= Html.ValidationSummary(Html.Resource("PPP_Resources, PPP_View_Profile_Create_ValidationSummary"))%>

    <% using (Html.BeginForm()) {%>
        <fieldset class="fields">
            <legend><%= Html.Resource("PPP_Resources, PPP_View_Profile_Create_CreateNewProfile")%></legend>
            <p>
                <label for="Name"><%= Html.Resource("PPP_Resources, PPP_View_Profile_Create_Name")%>:</label>
                <%= Html.TextBox("Name") %>
                <%= Html.ValidationMessage("Name", "*") %>
            </p>
            <p>
                <label for="LocalAddress"><%= Html.Resource("PPP_Resources, PPP_View_Profile_Create_LocalAddress")%>:</label>
                <%= Html.TextBox("LocalAddress") %>
                <%= Html.ValidationMessage("LocalAddress", "*") %>
            </p>
            <p>
                <label for="PoolId"><%= Html.Resource("PPP_Resources, PPP_View_Profile_Create_Pool")%>:</label>
                <%= Html.DropDownList("PoolId", (SelectList)ViewData["Pools"])%>
                <%= Html.ValidationMessage("PoolId", "*")%>
            </p>
            <p>
                <label for="RateLimit"><%= Html.Resource("PPP_Resources, PPP_View_Profile_Create_RateLimit")%>:</label>
                <%= Html.TextBox("RateLimit") %>
                <%= Html.ValidationMessage("RateLimit", "*") %>
            </p>
            <p>
                <label for="Cost"><%= Html.Resource("PPP_Resources, PPP_View_Profile_Create_Cost")%>:</label>
                <%= Html.TextBox("Cost") %>
                <%= Html.ValidationMessage("Cost", "*") %>
            </p>
            <p>
                <label for="Comment"><%= Html.Resource("PPP_Resources, PPP_View_Profile_Create_Comment")%>:</label>
                <%= Html.TextBox("Comment")%>
                <%= Html.ValidationMessage("Comment", "*")%>
            </p>
            <p>
                <input type="submit" value=<%= Html.Resource("PPP_Resources, PPP_View_Profile_Create_Create")%> />
            </p>
        </fieldset>

    <% } %>

    <div>
        <%=Html.ActionLink( Html.Resource("PPP_Resources, PPP_View_Profile_Create_BackToList"), "Index") %>
    </div>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>

