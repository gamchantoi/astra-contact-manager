<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<ContactManager.Models.MessageType>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <% using (Html.BeginForm()) {%>

        <fieldset>
            <legend>Edit Message Type</legend>
<%--            <p>
                <%= Html.LabelFor(model => model.TypeId) %>
                <%= Html.TextBoxFor(model => model.TypeId) %>
                <%= Html.ValidationMessageFor(model => model.TypeId) %>
            </p>--%>
             <%= Html.Hidden("TypeId", Model.TypeId)%>
            <p>
                <%= Html.LabelFor(model => model.Name) %>
                <%= Html.TextBoxFor(model => model.Name) %>
                <%= Html.ValidationMessageFor(model => model.Name) %>
            </p>
            <p>
                <input type="submit" value="Save" />
            </p>
        </fieldset>

    <% } %>

    <div>
        <%=Html.ActionLink("Back to List", "Index") %>
    </div>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>

