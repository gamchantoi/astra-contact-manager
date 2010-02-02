<%@ Page Title="" Language="C#" %>
<%@ Import Namespace="ContactManager.Web.Helpers"%>

<%= Html.ValidationSummary("Create was unsuccessful. Please correct the errors and try again.") %>
<% using (Html.BeginForm())
   {%>
<%= Html.Hidden("UserId", ViewData["UserId"]) %>
    <fieldset class="fields">
        <legend>Details</legend>
        <p>
            <label for="FirstName">
                <%=Html.Resource("Users_Resources, Users_Detail_Edit_FirstName")%></label>
            <%= Html.TextBox("FirstName")%>
            <%= Html.ValidationMessage("FirstName", "*")%>
        </p>
        <p>
            <label for="MiddleName">
                <%=Html.Resource("Users_Resources, Users_Detail_Edit_MiddleName")%></label>
            <%= Html.TextBox("MiddleName")%>
            <%= Html.ValidationMessage("MiddleName", "*")%>
        </p>
        <p>
            <label for="LastName">
                <%=Html.Resource("Users_Resources, Users_Detail_Edit_LastName")%></label>
            <%= Html.TextBox("LastName")%>
            <%= Html.ValidationMessage("LastName", "*")%>
        </p>
        <p>
            <label for="PassportID">
                <%=Html.Resource("Users_Resources, Users_Shared_DetailsUserControl_Passport")%></label>
            <%= Html.TextBox("PassportID")%>
            <%= Html.ValidationMessage("PassportID", "*")%>
        </p>
        <p>
            <label for="PassportDeliveryDate">
                <%=Html.Resource("Users_Resources, Users_Shared_DetailsUserControl_PassportDeliveryDate")%></label>
            <%= Html.TextBox("PassportDeliveryDate")%>
            <%= Html.ValidationMessage("PassportDeliveryDate", "*")%>
        </p>
        <p>
            <label for="PassportComment">
                <%=Html.Resource("Users_Resources, Users_Shared_DetailsUserControl_PassportComment")%></label>
            <%= Html.TextBox("PassportComment")%>
            <%= Html.ValidationMessage("PassportComment", "*")%>
        </p>
    </fieldset>
    <p>
        <input type="submit" value=<%=Html.Resource("Users_Resources, Users_User_Create_Create")%> />
    </p>
    <% } %>
