<%@ Page Title="" Language="C#" %>

<%= Html.ValidationSummary("Create was unsuccessful. Please correct the errors and try again.") %>
<% using (Html.BeginForm())
   {%>
<%= Html.Hidden("UserId", ViewData["UserId"]) %>
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
    <p>
        <input type="submit" value="Create" />
    </p>
    <% } %>
