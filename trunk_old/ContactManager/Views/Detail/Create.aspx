<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <%= Html.ValidationSummary("Create was unsuccessful. Please correct the errors and try again.") %>
    <% using (Html.BeginForm())
       {%>
    <%= Html.Hidden("UserId", ViewData["UserId"]) %>
    <fieldset class="fields">
        <legend>Create Details for User:
            <%=Html.Encode(ViewData["ClientName"])%></legend>
        <fieldset class="fields">
            <legend>Details</legend>
            <p>
                <label for="FirstName">
                    FirstName:</label>
                <%= Html.TextBox("FirstName")%>
                <%= Html.ValidationMessage("FirstName", "*")%>
            </p>
            <p>
                <label for="MiddleName">
                    MiddleName:</label>
                <%= Html.TextBox("MiddleName")%>
                <%= Html.ValidationMessage("MiddleName", "*")%>
            </p>
            <p>
                <label for="LastName">
                    LastName:</label>
                <%= Html.TextBox("LastName")%>
                <%= Html.ValidationMessage("LastName", "*")%>
            </p>
            <p>
                <label for="PassportID">
                    PassportID:</label>
                <%= Html.TextBox("PassportID")%>
                <%= Html.ValidationMessage("PassportID", "*")%>
            </p>
            <p>
                <label for="PassportDeliveryDate">
                    PassportDeliveryDate:</label>
                <%= Html.TextBox("PassportDeliveryDate")%>
                <%= Html.ValidationMessage("PassportDeliveryDate", "*")%>
            </p>
            <p>
                <label for="PassportComment">
                    PassportComment:</label>
                <%= Html.TextBox("PassportComment")%>
                <%= Html.ValidationMessage("PassportComment", "*")%>
            </p>
        </fieldset>
        <fieldset class="fields">
            <legend>Address</legend>
            <p>
                <label for="City">
                    City:</label>
                <%= Html.TextBox("City")%>
                <%= Html.ValidationMessage("City", "*")%>
            </p>
            <p>
                <label for="StreetId">
                    Street:</label>
                <%= Html.TextBox("StreetId")%>
                <%= Html.ValidationMessage("StreetId", "*")%>
            </p>
            <p>
                <label for="Building">
                    Building:</label>
                <%= Html.TextBox("Building")%>
                <%= Html.ValidationMessage("Building", "*")%>
            </p>
            <p>
                <label for="Details">
                    Details:</label>
                <%= Html.TextBox("Details")%>
                <%= Html.ValidationMessage("Details", "*")%>
            </p>
        </fieldset>
        <fieldset class="fields">
            <legend>Contract</legend>
            <p>
                <label for="ContractNumber">
                    ContractNumber:</label>
                <%= Html.TextBox("ContractNumber")%>
                <%= Html.ValidationMessage("ContractNumber", "*")%>
            </p>
            <p>
                <label for="Comment">
                    Comment:</label>
                <%= Html.TextBox("Comment")%>
                <%= Html.ValidationMessage("Comment", "*")%>
            </p>
            <p>
                <label for="CreateDate">
                    CreateDate:</label>
                <%= Html.TextBox("CreateDate")%>
                <%= Html.ValidationMessage("CreateDate", "*")%>
            </p>
            <p>
                <label for="ExpiredDate">
                    ExpiredDate:</label>
                <%= Html.TextBox("ExpiredDate")%>
                <%= Html.ValidationMessage("ExpiredDate", "*")%>
            </p>
        </fieldset>
    </fieldset>
    <p>
        <input type="submit" value="Create" />
    </p>
    <% } %>
    <div>
        <a href='<%= Url.Action("Edit", "User", new {id=ViewData["UserId"]}) %>'>Back to User</a>
    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
