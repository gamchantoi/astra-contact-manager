<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<ContactManager.Models.PaymentMethod>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <% using (Html.BeginForm())
       {%>
    <fieldset>
        <legend>Edit payment method</legend>
        <%= Html.Hidden("MethodId",Model.MethodId) %>
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
            <label for="Visible">
                Visible:</label>
            <%= Html.CheckBox("Visible")%>
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
