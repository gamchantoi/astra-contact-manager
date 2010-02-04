<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<SettingsViewModel>" %>

<%@ Import Namespace="ContactManager.Web.ViewModels" %>
<%@ Import Namespace="ContactManager.Web.Helpers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <%= Html.ValidationSummary("Error")%>
    <table class="container">
        <tr>
            <td>
                <fieldset class="fields">
                    <legend>
                        <%= Html.Resource("Web_Resources, Site_View_Settings_SystemSettings")%></legend>
                    <p>
                        <%= Html.ActionLink(Html.Resource("Web_Resources, Site_View_Settings_ClearDB"), "ClearDB")%>
                    </p>
                    <p>
                        <%= Html.ActionLink(Html.Resource("Web_Resources, Site_View_Settings_Index_EditStatuses"), "Index", "Status", new {@area="Users"}, null)%>
                    </p>
                    <p>
                        <%= Html.ActionLink(Html.Resource("Web_Resources, Site_View_Settings_Index_Addreses"), "Index", "Address", new { area = "Addresses" }, null)%>
                    </p>
                </fieldset>
            </td>
            <td>
                <fieldset class="fields">
                    <legend>
                        <%= Html.Resource("Web_Resources, Site_View_Settings_SynchronizationWith")%>
                        <%= Html.Encode(Model.ServerName)%></legend>
                    <p>
                        <%= Html.ActionLink(Html.Resource("Web_Resources, Site_View_Settings_SynchronizationTo"), "SyncToHost", "Sync", new { area = "Synchronizations" }, null)%>
                    </p>
                    <p>
                        <%= Html.ActionLink(Html.Resource("Web_Resources, Site_View_Settings_SynchronizationFrom"), "SyncFromHost", "Sync", new { area = "Synchronizations" }, null)%>
                    </p>
                </fieldset>
            </td>
        </tr>
        <tr>
            <td colspan="2" style="text-align:right">
                <%=Html.Encode("Build v.") %>
                <%=Model.BuildVersion %>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">

    <script language="javascript" type="text/javascript" src="<%= Url.Content("~/Scripts/astra-dialog.js")%>"></script>

</asp:Content>
