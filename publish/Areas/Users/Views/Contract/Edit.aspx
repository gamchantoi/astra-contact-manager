<%@ Page Title="" Language="C#"Inherits="System.Web.Mvc.ViewPage<ContactManager.Models.Contract>" %>
<%@ Import Namespace="ContactManager.Web.Helpers"%>

    <% using (Html.BeginForm()) {%>

        <fieldset>
         <%= Html.Hidden("UserId", ViewData["UserId"])%>
         <%= Html.Hidden("ContractId", Model.ContractId)%>
            <legend><a><%= Html.Resource("Users_Resources, Users_Shared_ContractUserControl_Edit")%></a></legend>
            <p>
                <%= Html.Label(Html.Resource("Users_Resources, Users_Shared_ContractUserControl_ContractNumber"))%>
                <%= Html.TextBoxFor(model => model.ContractNumber) %>
                <%= Html.ValidationMessageFor(model => model.ContractNumber) %>
            </p>
            <p>
                <%= Html.Label(Html.Resource("Users_Resources, Users_Shared_ContractUserControl_Comment"))%>
                <%= Html.TextBoxFor(model => model.Comment) %>
                <%= Html.ValidationMessageFor(model => model.Comment) %>
            </p>
            <p>
                <%= Html.Label(Html.Resource("Users_Resources, Users_Shared_ContractUserControl_CreateDate"))%>
                <%= Html.TextBoxFor(model => model.CreateDate, String.Format("{0:g}", Model.CreateDate)) %>
                <%= Html.ValidationMessageFor(model => model.CreateDate) %>
            </p>
            <p>
                <%= Html.Label(Html.Resource("Users_Resources, Users_Shared_ContractUserControl_ExpiredDate"))%>
                <%= Html.TextBoxFor(model => model.ExpiredDate, String.Format("{0:g}", Model.ExpiredDate)) %>
                <%= Html.ValidationMessageFor(model => model.ExpiredDate) %>
            </p>
            <p>
                <input type="submit" value="<%= Html.Resource("Users_Resources, Users_Shared_ContractUserControl_Save")%>" />
            </p>
        </fieldset>

    <% } %>

