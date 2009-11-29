<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<ContactManager.Models.ClientDetail>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <%= Html.ValidationSummary("Edit was unsuccessful. Please correct the errors and try again.") %>
    <% using (Html.BeginForm())
       {%>
    <%= Html.Hidden("UserId", ViewData["UserId"])%>
    <fieldset>
        <legend>Load money for
            <%= ViewData["UserName"]%></legend>
        <p>
            <label for="Details">
                Details:</label>
            <span id="Details">
                <%= Html.Encode(ViewData["UserDetail"] ?? "No details")%>
            </span>
        </p>
        <p>
            <label for="Balance">
                Balance:</label>
            <%= Html.Encode(String.Format("{0:F}", ViewData["Balance"]) + " + ")%>
            <%= Html.TextBox("Load", 0) %>
        </p>
        <p>
            <label for="MethodId">
                Payment Method:</label>
            <%= Html.DropDownList("MethodId", (SelectList)ViewData["Methods"])%>
            <%= Html.ValidationMessage("MethodId", "*")%>
        </p>
        <p>
            <label for="Comment">
                Comment:</label>
            <%= Html.TextBox("Comment")%>
            <%= Html.ValidationMessage("Comment", "*")%>
        </p>
        <p>
            <input type="submit" value="Load" />
        </p>
    </fieldset>
    <% } %>
    <div>
        <%=Html.ActionLink("Back to List", "Index") %>
    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
