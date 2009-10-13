<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<ContactManager.Models.Client>" %>

<%@ Import Namespace="ContactManager.Models" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script language="javascript" type="text/javascript" src="<%= Url.Content("~/Scripts/PasswordGenerator.js")%>"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <%= Html.ValidationSummary("Edit was unsuccessful. Please correct the errors and try again.") %>
    <fieldset class="fields">
        <legend>Edit User:
            <%=Html.Encode(Model.UserName)%></legend>
        <fieldset class="fields">
            <legend>General</legend>
            <% using (Html.BeginForm())
               {%>
            <%= Html.Hidden("UserId", Model.UserId) %>
            <%= Html.Hidden("UserName", Model.UserName)%>
            <%= Html.Hidden("Balance", Model.Balance)%>
            <%= Html.Hidden("Status", Model.Status)%>
            <p>
                <label for="Balance">
                    Balance:</label>
                <%= Html.Encode(String.Format("{0:F}", Model.Balance)) %>
            </p>
            <p>
                <label for="Password">
                    Password:</label>
                <%= Html.TextBox("Password", Model.Password) %>
                <%= Html.ValidationMessage("Password", "*") %>
                <input type="button" value="Generate" onclick="javascript: generatePassword('8', '#Password');" />
            </p>
            <p>
                <label for="Role">
                    Role:</label>
                <%= Html.DropDownList("Role", (SelectList)ViewData["Roles"])%>
                <%= Html.ValidationMessage("Role", "*") %>
            </p>
            <%if (!Model.Role.Equals(Role.admin.ToString()))
              {%>
            <p>
                <label for="ProfileId">
                    Profile:</label>
                <%= Html.DropDownList("ProfileId", (SelectList)ViewData["Profiles"])%>
                <%= Html.ValidationMessage("ProfileId", "*")%>
            </p>
            <%} %>
            <p>
                <label for="Credit">
                    Credit:</label>
                <%= Html.TextBox("Credit", String.Format("{0:F}", Model.Credit))%>
                <%= Html.ValidationMessage("Credit", "*")%>
            </p>
            <p>
                <label for="Email">
                    Email:</label>
                <%= Html.TextBox("Email", Model.Email) %>
                <%= Html.ValidationMessage("Email", "*") %>
            </p>
            <p>
                <label for="Comment">
                    Comment:</label>
                <%= Html.TextBox("Comment", Model.Comment)%>
                <%= Html.ValidationMessage("Comment", "*")%>
            </p>
            <p>
                <label for="Status">
                    Status:</label>
                <%= Html.DropDownList("SecretStatus", (SelectList)ViewData["Statuses"])%>
                <%= Html.ValidationMessage("SecretStatus", "*")%>
            </p>
            <p>
                <input name="button" type="submit" value="Save" />
                <input name="button" type="submit" value="Details" />
            </p>
            <% } %>
        </fieldset>
        <%if (!Model.Role.Equals(Role.admin.ToString()))
          {%>
        <fieldset class="fields">
            <legend>Identification</legend>
            <% using (Html.BeginForm("UpdateSecret", "User"))
               {%>
            <%= Html.Hidden("UserId", Model.UserId)%>
            <p>
                <label for="LocalAddress">
                    Local Address:</label>
                <%= Html.TextBox("LocalAddress", Model.mkt_PPPSecretsReference.Value != null ? Model.mkt_PPPSecretsReference.Value.LocalAddress : "")%>
                <%= Html.ValidationMessage("LocalAddress", "*")%>
            </p>
            <p>
                <label for="RemoteAddress">
                    Remote Address:</label>
                <%= Html.TextBox("RemoteAddress", Model.mkt_PPPSecretsReference.Value != null ? Model.mkt_PPPSecretsReference.Value.RemoteAddress : "")%>
                <%= Html.ValidationMessage("RemoteAddress", "*")%>
            </p>
            <p>
                <label for="MACAddress">
                    MAC Address:</label>
                <%= Html.TextBox("MACAddress", Model.mkt_PPPSecretsReference.Value != null ? Model.mkt_PPPSecretsReference.Value.MACAddress : "")%>
                <%= Html.ValidationMessage("MACAddress", "*")%>
            </p>
            <p>
                <label for="DHCPAddress">
                    DHCP Address:</label>
                <%= Html.TextBox("DHCPAddress", Model.mkt_PPPSecretsReference.Value != null ? Model.mkt_PPPSecretsReference.Value.DHCPAddress : "")%>
                <%= Html.ValidationMessage("DHCPAddress", "*")%>
            </p>
            <legend>System Services</legend>
            <p>
                <label for="SystemRealIP">
                    Real IP Address:</label>
                <%= Html.CheckBox("SystemRealIP", Model.mkt_PPPSecretsReference.Value != null ? Model.mkt_PPPSecretsReference.Value.SystemRealIP : false)%>
            </p>
            <p style="display: none;">
                <label for="SystemStayOnline">
                    Stay Online:</label>
                <%= Html.CheckBox("SystemStayOnline", Model.mkt_PPPSecretsReference.Value != null ? Model.mkt_PPPSecretsReference.Value.SystemStayOnline : false)%>
            </p>
            <p>
                <input name="button" type="submit" value="Update" />
            </p>
            <%} %>
        </fieldset>
        <%} %>
    </fieldset>
    <div>
        <%=Html.ActionLink("Back to List", "Index") %>
    </div>
</asp:Content>
