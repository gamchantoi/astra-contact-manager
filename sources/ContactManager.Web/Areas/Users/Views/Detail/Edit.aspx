<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<ContactManager.Models.Client>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <%= Html.ValidationSummary("Edit was unsuccessful. Please correct the errors and try again.") %>
    <% using (Html.BeginForm())
       {%>
    <% var details = Model.ClientDetailsReference.Value == null ? new ContactManager.Models.ClientDetail() : Model.ClientDetailsReference.Value; %>
    <% var address = Model.astra_AddressesReference.Value == null ? new ContactManager.Models.Address() : Model.astra_AddressesReference.Value; %>
    <% var contract = Model.astra_ContractsReference.Value == null ? new ContactManager.Models.Contract() : Model.astra_ContractsReference.Value; %>
    <%= Html.Hidden("DetailId", details.DetailId)%>
    <%= Html.Hidden("AddressId", address.AddressId)%>
    <%= Html.Hidden("ContractId", contract.ContractId)%>
    <%= Html.Hidden("UserId", Model.UserId)%>
    <fieldset class="fields">
        <legend>Edit Details for User:
            <%=Html.Encode(Model.UserName)%></legend>
        <fieldset class="fields">
            <legend>Details</legend>
            <p>
                <label for="FirstName">
                    FirstName:</label> 
                <%= Html.TextBox("FirstName", details.FirstName)%>
                <%= Html.ValidationMessage("FirstName", "*") %>
            </p>
            <p>
                <label for="MiddleName">
                    MiddleName:</label>
                <%= Html.TextBox("MiddleName", details.MiddleName)%>
                <%= Html.ValidationMessage("MiddleName", "*") %>
            </p>
            <p>
                <label for="LastName">
                    LastName:</label>
                <%= Html.TextBox("LastName", details.LastName)%>
                <%= Html.ValidationMessage("LastName", "*") %>
            </p>
            <p>
                <label for="PassportID">
                    PassportID:</label>
                <%= Html.TextBox("PassportID", details.PassportID)%>
                <%= Html.ValidationMessage("PassportID", "*") %>
            </p>
            <p>
                <label for="PassportDeliveryDate">
                    PassportDeliveryDate:</label>
                <%= Html.TextBox("PassportDeliveryDate", details.PassportDeliveryDate.HasValue ? details.PassportDeliveryDate.Value.ToShortDateString() : "")%>
                <%= Html.ValidationMessage("PassportDeliveryDate", "*") %>
            </p>
            <p>
                <label for="PassportComment">
                    PassportComment:</label>
                <%= Html.TextBox("PassportComment", details.PassportComment)%>
                <%= Html.ValidationMessage("PassportComment", "*") %>
            </p>
        </fieldset>
        <fieldset class="fields">
            <legend>Address</legend>
            <p>
                <label for="City">
                    City:</label>
                <%= Html.TextBox("City", address.City)%>
                <%= Html.ValidationMessage("City", "*")%>
            </p>
            <p>
                <label for="StreetId">
                    Street:</label>
                <%= Html.TextBox("StreetId", address.Street)%>
                <%= Html.ValidationMessage("StreetId", "*")%>
            </p>
            <p>
                <label for="Building">
                    Building:</label>
                <%= Html.TextBox("Building", address.Building)%>
                <%= Html.ValidationMessage("Building", "*") %>
            </p>
            <p>
                <label for="Details">
                    Details:</label>
                <%= Html.TextBox("Details", address.Details)%>
                <%= Html.ValidationMessage("Details", "*")%>
            </p>
        </fieldset>
        <fieldset class="fields">
            <legend>Contract</legend>
            <p>
                <label for="ContractNumber">
                    ContractNumber:</label>
                <%= Html.TextBox("ContractNumber", contract.ContractNumber)%>
                <%= Html.ValidationMessage("ContractNumber", "*")%>
            </p>
            <p>
                <label for="Comment">
                    Comment:</label>
                <%= Html.TextBox("Comment", contract.Comment)%>
                <%= Html.ValidationMessage("Comment", "*") %>
            </p>
            <p>
                <label for="CreateDate">
                    CreateDate:</label>
                <%= Html.TextBox("CreateDate", contract.CreateDate.HasValue ? contract.CreateDate.Value.ToShortDateString() : "")%>
                <%= Html.ValidationMessage("CreateDate", "*")%>
            </p>
            <p>
                <label for="ExpiredDate">
                    ExpiredDate:</label>
                <%= Html.TextBox("ExpiredDate", contract.ExpiredDate.HasValue ? contract.ExpiredDate.Value.ToShortDateString() : "")%>
                <%= Html.ValidationMessage("ExpiredDate", "*") %>
            </p>
        </fieldset>
    </fieldset>
    <p>
        <input type="submit" value="Save" />
    </p>
    <% } %>
    <div>
        <a href='<%= Url.Action("Edit", "User", new {id=Model.UserId}) %>'>Back to User</a>
    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
