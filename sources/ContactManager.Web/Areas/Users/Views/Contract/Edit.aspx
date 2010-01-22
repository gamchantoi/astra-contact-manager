<%@ Page Title="" Language="C#"Inherits="System.Web.Mvc.ViewPage<ContactManager.Models.Contract>" %>

    <% using (Html.BeginForm()) {%>

        <fieldset>
         <%= Html.Hidden("UserId", ViewData["UserId"])%>
            <legend>Edit</legend>
            <p>
                <%= Html.LabelFor(model => model.ContractId) %>
                <%= Html.TextBoxFor(model => model.ContractId) %>
                <%= Html.ValidationMessageFor(model => model.ContractId) %>
            </p>
            <p>
                <%= Html.LabelFor(model => model.ContractNumber) %>
                <%= Html.TextBoxFor(model => model.ContractNumber) %>
                <%= Html.ValidationMessageFor(model => model.ContractNumber) %>
            </p>
            <p>
                <%= Html.LabelFor(model => model.Comment) %>
                <%= Html.TextBoxFor(model => model.Comment) %>
                <%= Html.ValidationMessageFor(model => model.Comment) %>
            </p>
            <p>
                <%= Html.LabelFor(model => model.CreateDate) %>
                <%= Html.TextBoxFor(model => model.CreateDate, String.Format("{0:g}", Model.CreateDate)) %>
                <%= Html.ValidationMessageFor(model => model.CreateDate) %>
            </p>
            <p>
                <%= Html.LabelFor(model => model.ExpiredDate) %>
                <%= Html.TextBoxFor(model => model.ExpiredDate, String.Format("{0:g}", Model.ExpiredDate)) %>
                <%= Html.ValidationMessageFor(model => model.ExpiredDate) %>
            </p>
            <p>
                <input type="submit" value="Save" />
            </p>
        </fieldset>

    <% } %>

