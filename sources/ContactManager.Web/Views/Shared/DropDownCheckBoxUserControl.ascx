<%@ Import Namespace="ContactManager.Accounts.ViewModels"%>

<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<DropDownViewModel>" %>
<div>
    <select multiple="multiple" id="<%=Html.Encode(Model.Name) %>" name="<%=Html.Encode(Model.Name) %>">
        <%
            foreach (var keyValuePair in Model.ItemsList)
            {
        %>
        <optgroup label="<%=Html.Encode(keyValuePair.Key) %>">
            <%
                foreach (var item in keyValuePair.Value)
                {
            %>
            <option value="<%=Html.Encode(item.Value)%>" <%=item.Selected ? "selected=\"selected\"" : ""%>>
                <%=Html.Encode(item.Text)%>
            </option>
            <%
                } %>
        </optgroup>
        <%

            } %>
    </select>
</div>
