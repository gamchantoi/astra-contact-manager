<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<ContactManager.Models.PaymentMethod>" %>
<%@ Import Namespace="ContactManager.Web.Helpers"%>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">


    <% using (Html.BeginForm()) {%>

        <fieldset>
            <legend><%= Html.Resource("Accounts_Resources, Accounts_View_PaymentsMethods_Create_CreatePaymentMethod")%></legend>
            <p>
                <%= Html.Label(Html.Resource("Accounts_Resources, Accounts_View_PaymentsMethods_Create_Name")) %>
                <%= Html.TextBoxFor(model => model.Name) %>
                <%= Html.ValidationMessageFor(model => model.Name) %>
            </p>
            <p>
                <%= Html.Label(Html.Resource("Accounts_Resources, Accounts_View_PaymentsMethods_Create_Comment"))%>
                <%= Html.TextBoxFor(model => model.Comment) %>
                <%= Html.ValidationMessageFor(model => model.Comment) %>
            </p>
        <p>
            <label for="Visible">
                <%= Html.Resource("Accounts_Resources, Accounts_View_PaymentsMethods_Create_Visible")%></label>
            <%= Html.CheckBox("Visible")%>
        </p>
            <p>
                <input type="submit" value=<%= Html.Resource("Accounts_Resources, Accounts_View_PaymentsMethods_Create_Create")%> />
            </p>
        </fieldset>

    <% } %>

    <div>
        <%=Html.ActionLink(Html.Resource("Accounts_Resources, Accounts_View_PaymentsMethods_Create_BackToList"), "Index") %>
    </div>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>

