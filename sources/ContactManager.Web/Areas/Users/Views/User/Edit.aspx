<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<ContactManager.Users.ViewModels.ClientViewModel>" %>

<%@ Import Namespace="ContactManager.Web.Helpers" %>
<%@ Import Namespace="ContactManager.Models.Enums" %>
<%@ Import Namespace="ContactManager.Users.Services" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script language="javascript" type="text/javascript" src="<%= Url.Content("~/Scripts/astra-password-generator.js")%>"></script>
    <script language="javascript" type="text/javascript" src="<%= Url.Content("~/Scripts/astra-dialog.js")%>"></script>

    <script type="text/javascript">
        $(function() {
            $("#details-accordion").accordion({ header: "h3" });
        });
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <%= Html.ValidationSummary(Html.Resource("Users_Resources, Users_User_Edit_ValidationSummary"))%>
    <fieldset class="fields">
        <legend>
            <%= Html.Resource("Users_Resources, Users_User_Edit_EditUser")%>
            <%=Html.Encode(Model.UserName)%></legend>
        <table>
            <tr>
                <td>
                    <fieldset class="fields">
                        <legend>
                            <%= Html.Resource("Users_Resources, Users_User_Edit_General")%></legend>
                        <% using (Html.BeginForm())
                           {%>
                        <%= Html.Hidden("UserId", Model.UserId) %>
                        <%= Html.Hidden("Balance", Model.Balance)%>
                        <p>
                            <label for="Balance">
                                <%= Html.Resource("Users_Resources, Users_User_Edit_Balance")%>:</label>
                            <%= Html.Encode(String.Format("{0:F}", Model.Balance)) %>
                        </p>
                        <p>
                            <label for="UserName">
                                <%= Html.Resource("Users_Resources, Users_User_Edit_UserName")%>:</label>
                            <%= Html.TextBox("UserName", Model.UserName)%>
                            <%= Html.ValidationMessage("UserName", "*")%>
                        </p>
                        <p>
                            <label for="Password">
                                <%= Html.Resource("Users_Resources, Users_User_Edit_Password")%>:</label>
                            <%= Html.TextBox("Password", Model.Password) %>
                            <%= Html.ValidationMessage("Password", "*") %>
                            <input type="button" value="Generate" onclick="javascript: generatePassword('8', '#Password');" />
                        </p>
                        <p>
                            <label for="Role">
                                <%= Html.Resource("Users_Resources, Users_User_Edit_Role")%>:</label>
                            <%= Html.DropDownListFor(r => r.Role, Model.Roles)%>
                            <%= Html.ValidationMessage("Role", "*") %>
                        </p>
                        <%if (!Model.Role.Equals(ROLES.admin.ToString()))
                          {%>
                        <p>
                            <label for="ProfileId">
                                <%= Html.Resource("Users_Resources, Users_User_Edit_Profile")%>:</label>
                            <%= Html.DropDownListFor(p => p.ProfileId, Model.Profiles)%>
                            <%= Html.ValidationMessage("ProfileId", "*")%>
                        </p>
                        <%} %>
                        <p>
                            <label for="Credit">
                                <%= Html.Resource("Users_Resources, Users_User_Edit_Credit")%>:</label>
                            <%= Html.TextBox("Credit", String.Format("{0:F}", Model.Credit))%>
                            <%= Html.ValidationMessage("Credit", "*")%>
                        </p>
                        <p>
                            <label for="Email">
                                <%= Html.Resource("Users_Resources, Users_User_Edit_Email")%>:</label>
                            <%= Html.TextBox("Email", Model.Email) %>
                            <%= Html.ValidationMessage("Email", "*") %>
                        </p>
                        <p>
                            <label for="Comment">
                                <%= Html.Resource("Users_Resources, Users_User_Edit_Comment")%>:</label>
                            <%= Html.TextBox("Comment", Model.Comment)%>
                            <%= Html.ValidationMessage("Comment", "*")%>
                        </p>
                        <p>
                            <label for="StatusId">
                                <%= Html.Resource("Users_Resources, Users_User_Edit_Status")%>:</label>
                            <%= Html.DropDownListFor(s => s.StatusId, Model.Statuses)%>
                            <%= Html.ValidationMessage("StatusId", "*")%>
                        </p>
                        <p>
                            <input name="button" type="submit" value="<%= Html.Resource("Users_Resources, Users_User_Edit_Save")%>" />
                        </p>
                        <% } %>
                    </fieldset>
                </td>
                <td>
                    <div id="details-accordion">
                        <div>
                            <h3>
                                <a href="#"><%=Html.Resource("Users_Resources, Users_Shared_PPPSecretUserControl_PPPSecret")%></a></h3>
                            <div>
                                <%Html.RenderPartial("PPPSecretUserControl", Model); %></div>
                        </div>
                        <div>
                            <h3>
                                <a href="#"><%=Html.Resource("Users_Resources, Users_Shared_DetailsUserControl_Details")%></a></h3>
                            <div>
                                <%Html.RenderPartial("DetailsUserControl", Model); %></div>
                        </div>
                        <div>
                            <h3>
                                <a href="#"><%=Html.Resource("Users_Resources, Users_Shared_ServicesUserControl_Services")%></a></h3>
                            <div>
                                <%Html.RenderPartial("ServicesUserControl", Model); %></div>
                        </div>
                        <div>
                            <h3>
                                <a href="#"><%=Html.Resource("Users_Resources, Users_Shared_AddressUserControl_Address")%></a></h3>
                            <div>
                                <%Html.RenderPartial("AddressUserControl", Model); %></div>
                        </div>
                        <div>
                            <h3>
                                <a href="#"><%=Html.Resource("Users_Resources, Users_Shared_ContractUserControl_Contract")%></a></h3>
                            <div>
                                <%Html.RenderPartial("ContractUserControl", Model); %></div>
                        </div>
                    </div>
                </td>
            </tr>
        </table>
    </fieldset>
    <div>
        <%=Html.ActionLink(Html.Resource("Users_Resources, Users_User_Edit_BackToList"), "Index") %>
    </div>
</asp:Content>
