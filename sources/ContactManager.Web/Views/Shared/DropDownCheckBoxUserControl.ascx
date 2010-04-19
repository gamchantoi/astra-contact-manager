<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Dictionary<string, SelectList>>" %>
<div>
    <select multiple="multiple" id="PaymentMethods" name="PaymentMethods">
        <%
            foreach (var keyValuePair in Model)
            {
        %>
        <optgroup label="<%=Html.Encode(keyValuePair.Key) %>">
            <%
                foreach (var item in keyValuePair.Value)
                {
            %>
            <option value="<%=Html.Encode(item.Value)%>" selected="<%=item.Selected ? "selected" : ""%>">
                <%=Html.Encode(item.Text)%>
            </option>
            <%
                } %>
        </optgroup>
        <%

            } %>
    </select>
</div>
