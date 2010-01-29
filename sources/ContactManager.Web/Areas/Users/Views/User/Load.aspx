<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage<ContactManager.Models.ViewModels.LoadMoneyViewModel>" %>

<%@ Import Namespace="ContactManager.Web.Helpers" %>
<%= Html.ValidationSummary(Html.Resource("Users_Resources, Users_User_Load_ValidationSummary"))%>
<% using (Html.BeginForm())
   {%>
<%= Html.HiddenFor(model => model.ClientId) %>
<fieldset>
    <legend>
        <%= Html.Resource("Users_Resources, Users_User_Load_LoadMoneyFor")%>
        <%= Html.DisplayFor(model => model.FullName)%></legend>
    <p>
        <%= Html.Label(Html.Resource("Users_Resources, Users_User_Load_Balance")) %>
        <%= Html.DisplayFor(model => model.Balance, String.Format("{0:F}", Model.Balance)) %>
    </p>
    <p>
        <%= Html.Label(Html.Resource("Users_Resources, Users_User_Load_Sum")) %>
        <%= Html.TextBox("Sum", "")%>
        <%= Html.ValidationMessageFor(model => model.Sum)%>
    </p>
    <p>
        <%= Html.Label(Html.Resource("Users_Resources, Users_User_Load_Method")) %>
        <%= Html.DropDownListFor(model => model.MethodId, Model.LoadMethods) %>
        <%= Html.ValidationMessageFor(model => model.MethodId) %>
    </p>
    <p>
        <%= Html.Label( Html.Resource("Users_Resources, Users_User_Load_Comment")) %>
        <%= Html.TextBoxFor(model => model.Comment) %>
        <%= Html.ValidationMessageFor(model => model.Comment) %>
    </p>
    <p>
        <input type="submit" value='<%= Html.Resource("Users_Resources, Users_User_Load_Save")%>' />
    </p>
</fieldset>
<% } %>
