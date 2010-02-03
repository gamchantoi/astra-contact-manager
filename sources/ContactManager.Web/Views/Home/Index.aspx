<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<ContactManager.Users.ViewModels.ClientViewModel>" %>

<%@ Import Namespace="ContactManager.Web.Helpers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <table class="container">
        <tr>
            <td>
                <fieldset class="fields">
                    <legend>
                        <%= Html.Resource("Web_Resources, Site_View_Account_Index_UserData")%></legend>
                    <p>
                        <%= Html.Resource("Web_Resources, Site_View_Account_Index_UserName")%>
                        <%= Html.Encode(Model.UserName) %>
                    </p>
                    <p>
                        <%= Html.Resource("Web_Resources, Site_View_Account_Index_Email")%>
                        <%= Html.Encode(Model.Email) %>
                    </p>
                    <p>
                        <%= Html.Resource("Web_Resources, Site_View_Account_Index_Status")%>
                        <%= Html.Encode(Model.StatusName) %>
                    </p>
                    <p>
                        <%= Html.Resource("Web_Resources, Site_View_Account_Index_Balance")%>
                        <%= Html.Encode(String.Format("{0:F}", Model.Balance)) %>
                    </p>
                    <p>
                        <%= Html.Resource("Web_Resources, Site_View_Account_Index_FullName")%>
                        <%--<%= Html.Encode(Model.GetFullName()) %>--%>
                    </p>
                    <p>
                        <%= Html.Resource("Web_Resources, Site_View_Account_Index_TariffName")%>
                        <%--<%= Html.Encode(Model.GetProfileName()) %>--%>
                    </p>
                </fieldset>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td>
                <div>
                    <h3>
                        <a href="#">
                            <%=Html.Resource("Users_Resources, Users_Shared_DetailsUserControl_Details")%></a></h3>
                    <div>
                        <%Html.RenderPartial("DetailsUserControl", Model); %></div>
                </div>
            </td>
            <td>
                <div>
                    <h3>
                        <a href="#">
                            <%=Html.Resource("Users_Resources, Users_Shared_ServicesUserControl_Services")%></a></h3>
                    <div>
                        <%Html.RenderPartial("ServicesUserControl", Model); %></div>
                </div>
            </td>
        </tr>
        <tr>
            <td>
                <div>
                    <h3>
                        <a href="#">
                            <%=Html.Resource("Users_Resources, Users_Shared_AddressUserControl_Address")%></a></h3>
                    <div>
                        <%Html.RenderPartial("AddressUserControl", Model); %></div>
                </div>
            </td>
            <td>
                <div>
                    <h3>
                        <a href="#">
                            <%=Html.Resource("Users_Resources, Users_Shared_ContractUserControl_Contract")%></a></h3>
                    <div>
                        <%Html.RenderPartial("ContractUserControl", Model); %></div>
                </div>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
