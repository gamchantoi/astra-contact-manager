<%@ Page Title="" Language="C#"  Inherits="System.Web.Mvc.ViewPage<ContactManager.Models.ClientDetail>" %>
<%@ Import Namespace="ContactManager.Web.Helpers"%>


    <%= Html.ValidationSummary("Edit was unsuccessful. Please correct the errors and try again.") %>
    <% using (Html.BeginForm())
       {%>
    <%= Html.Hidden("DetailId", Model.DetailId)%>
    <%= Html.Hidden("UserId", Model.UserId)%>
    <fieldset class="fields">
        <legend><a><%=Html.Resource("Users_Resources, Users_Shared_DetailsUserControl_Details")%>
            <%=Html.Encode(Model.UserName)%></a></legend>
            <p>
                <label for="FirstName">
                    <%=Html.Resource("Users_Resources, Users_Detail_Edit_FirstName")%></label> 
                <%= Html.TextBox("FirstName", Model.FirstName)%>
                <%= Html.ValidationMessage("FirstName", "*") %>
            </p>
            <p>
                <label for="MiddleName">
                    <%=Html.Resource("Users_Resources, Users_Detail_Edit_MiddleName")%></label>
                <%= Html.TextBox("MiddleName", Model.MiddleName)%>
                <%= Html.ValidationMessage("MiddleName", "*") %>
            </p>
            <p>
                <label for="LastName">
                    <%=Html.Resource("Users_Resources, Users_Detail_Edit_LastName")%></label>
                <%= Html.TextBox("LastName", Model.LastName)%>
                <%= Html.ValidationMessage("LastName", "*") %>
            </p>
            <p>
                <label for="PassportID">
                    <%=Html.Resource("Users_Resources, Users_Shared_DetailsUserControl_Passport")%></label>
                <%= Html.TextBox("PassportID", Model.PassportID)%>
                <%= Html.ValidationMessage("PassportID", "*") %>
            </p>
            <p>
                <label for="PassportDeliveryDate">
                    <%=Html.Resource("Users_Resources, Users_Shared_DetailsUserControl_PassportDeliveryDate")%></label>
                <%= Html.TextBox("PassportDeliveryDate", Model.PassportDeliveryDate.HasValue ? Model.PassportDeliveryDate.Value.ToShortDateString() : "")%>
                <%= Html.ValidationMessage("PassportDeliveryDate", "*") %>
            </p>
            <p>
                <label for="PassportComment">
                    <%=Html.Resource("Users_Resources, Users_Shared_DetailsUserControl_PassportComment")%></label>
                <%= Html.TextBox("PassportComment", Model.PassportComment)%>
                <%= Html.ValidationMessage("PassportComment", "*") %>
            </p>
    </fieldset>
    <p>
        <input type="submit" value="Save" />
    </p>
    <% } %>


