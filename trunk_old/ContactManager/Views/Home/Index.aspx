<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<ContactManager.Models.Client>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <fieldset class="fields">
        <legend>User data</legend>
        <p>
            UserName:
            <%= Html.Encode(Model.UserName) %>
        </p>

        <p>
            Email:
            <%= Html.Encode(Model.Email) %>
        </p>
        <p>
            Status:
            <%= Html.Encode(Model.Status == 1 ? "Active" : "Disabled") %>
        </p>
        <p>
            Balance:
            <%= Html.Encode(String.Format("{0:F}", Model.Balance)) %>
        </p>
        <p>
            FullName:
            <%= Html.Encode(Model.FullName) %>
        </p>
        <p>
            TariffName:
            <%= Html.Encode(Model.ProfileName) %>
        </p>
    </fieldset>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>

