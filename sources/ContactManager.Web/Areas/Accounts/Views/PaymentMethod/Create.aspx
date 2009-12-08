<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<ContactManager.Models.PaymentMethod>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Create</h2>


    <% using (Html.BeginForm()) {%>

        <fieldset>
            <legend>Fields</legend>
<%--            <p>
                <%= Html.LabelFor(model => model.MethodId) %>
                <%= Html.TextBoxFor(model => model.MethodId) %>
                <%= Html.ValidationMessageFor(model => model.MethodId) %>
            </p>--%>
            <p>
                <%= Html.LabelFor(model => model.Name) %>
                <%= Html.TextBoxFor(model => model.Name) %>
                <%= Html.ValidationMessageFor(model => model.Name) %>
            </p>
            <p>
                <%= Html.LabelFor(model => model.Comment) %>
                <%= Html.TextBoxFor(model => model.Comment) %>
                <%= Html.ValidationMessageFor(model => model.Comment) %>
            </p>
            <p>
                <%= Html.LabelFor(model => model.Visible) %>
                <%= Html.TextBoxFor(model => model.Visible) %>
                <%= Html.ValidationMessageFor(model => model.Visible) %>
            </p>
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

