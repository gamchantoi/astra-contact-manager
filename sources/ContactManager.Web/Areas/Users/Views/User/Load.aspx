<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" 
Inherits="System.Web.Mvc.ViewPage<ContactManager.Models.ViewModels.LoadMoneyViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <% using (Html.BeginForm())
       {%>
    <%= Html.HiddenFor(model => model.UserId) %>
    <fieldset>
        <legend>Load Modey for:
            <%= Html.DisplayFor(model => model.FullName)%></legend>
        <p>
            <%= Html.LabelFor(model => model.Balance) %>
            <%= Html.DisplayFor(model => model.Balance, String.Format("{0:F}", Model.Balance)) %>
        </p>
        <p>
            <%= Html.LabelFor(model => model.Sum) %>
            <%= Html.TextBoxFor(model => model.Sum)%>
            <%= Html.ValidationMessageFor(model => model.Sum)%>
        </p>
        <p>
            <%= Html.Label("Method") %>
            <%= Html.DropDownListFor(model => model.MethodId, Model.LoadMethods) %>
            <%= Html.ValidationMessageFor(model => model.MethodId) %>
        </p>
        <p>
            <%= Html.LabelFor(model => model.Comment) %>
            <%= Html.TextBoxFor(model => model.Comment) %>
            <%= Html.ValidationMessageFor(model => model.Comment) %>
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
