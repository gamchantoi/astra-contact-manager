<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<ContactManager.Models.PaymentMethod>" %>
<%@ Import Namespace="ContactManager.Web.Helpers"%>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <script>
        $(document).ready(function() {
            $("#createForm").validate();
        });
    </script>
    
    <% using (Html.BeginForm("Edit", "PaymentMethod", FormMethod.Post, new { id = "createForm" }))
       {%>
    <fieldset>
        <legend><%= Html.Resource("Accounts_Resources, Accounts_View_PaymentsMethods_Edit_EditPaymentMethod")%></legend>
        <%= Html.Hidden("MethodId",Model.MethodId) %>
        <p>
            <%= Html.Label(Html.Resource("Accounts_Resources, Accounts_View_PaymentsMethods_Create_Name"))%>
            <%= Html.TextBoxFor(model => model.Name, new { @class = "required" })%>
            <%= Html.ValidationMessageFor(model => model.Name) %>
        </p>
        <p>
            <%= Html.Label(Html.Resource("Accounts_Resources, Accounts_View_PaymentsMethods_Create_Comment"))%>
            <%= Html.TextBoxFor(model => model.Comment, new { @class = "required" })%>
            <%= Html.ValidationMessageFor(model => model.Comment) %>
        </p>
        <p>
            <label for="Visible">
                 <%= Html.Resource("Accounts_Resources, Accounts_View_PaymentsMethods_Create_Visible")%></label>
            <%= Html.CheckBox("Visible")%>
        </p>
        <p>
            <input type="submit" value=<%= Html.Resource("Accounts_Resources, Accounts_View_PaymentsMethods_Edit_Save")%> />
        </p>
    </fieldset>
    <% } %>
    <div>
        <%=Html.ActionLink(Html.Resource("Accounts_Resources, Accounts_View_PaymentsMethods_Create_BackToList"), "Index")%>
    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
