<%@ Page Title="" Language="C#"  Inherits="System.Web.Mvc.ViewPage<ContactManager.Models.ClientDetail>" %>


    <%= Html.ValidationSummary("Edit was unsuccessful. Please correct the errors and try again.") %>
    <% using (Html.BeginForm())
       {%>
    <%= Html.Hidden("DetailId", Model.DetailId)%>
    <%= Html.Hidden("UserId", Model.UserId)%>
    <fieldset class="fields">
        <legend><a>Edit Details for User:
            <%=Html.Encode(Model.UserName)%></a></legend>
            <p>
                <label for="FirstName">
                    FirstName:</label> 
                <%= Html.TextBox("FirstName", Model.FirstName)%>
                <%= Html.ValidationMessage("FirstName", "*") %>
            </p>
            <p>
                <label for="MiddleName">
                    MiddleName:</label>
                <%= Html.TextBox("MiddleName", Model.MiddleName)%>
                <%= Html.ValidationMessage("MiddleName", "*") %>
            </p>
            <p>
                <label for="LastName">
                    LastName:</label>
                <%= Html.TextBox("LastName", Model.LastName)%>
                <%= Html.ValidationMessage("LastName", "*") %>
            </p>
            <p>
                <label for="PassportID">
                    PassportID:</label>
                <%= Html.TextBox("PassportID", Model.PassportID)%>
                <%= Html.ValidationMessage("PassportID", "*") %>
            </p>
            <p>
                <label for="PassportDeliveryDate">
                    PassportDeliveryDate:</label>
                <%= Html.TextBox("PassportDeliveryDate", Model.PassportDeliveryDate.HasValue ? Model.PassportDeliveryDate.Value.ToShortDateString() : "")%>
                <%= Html.ValidationMessage("PassportDeliveryDate", "*") %>
            </p>
            <p>
                <label for="PassportComment">
                    PassportComment:</label>
                <%= Html.TextBox("PassportComment", Model.PassportComment)%>
                <%= Html.ValidationMessage("PassportComment", "*") %>
            </p>
    </fieldset>
    <p>
        <input type="submit" value="Save" />
    </p>
    <% } %>


