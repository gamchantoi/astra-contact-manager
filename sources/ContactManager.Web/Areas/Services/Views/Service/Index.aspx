<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<ContactManager.Models.Service>>" %>
<%@ Import Namespace="ContactManager.Web.Helpers"%>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <table class="data-table" cellpadding="0" cellspacing="0">
        <thead>
            <tr>
                <th>
                    <%= Html.Resource("Services_Resources, Services_View_Index_Type")%>
                </th>
                <th>
                    <%= Html.Resource("Services_Resources, Services_View_Create_Name")%>
                </th>
                <th>
                    <%= Html.Resource("Services_Resources, Services_View_Create_Comment")%>
                </th>
                <th>
                    <%= Html.Resource("Services_Resources, Services_View_Create_Cost")%>
                </th>
                <th>
                    <%= Html.Resource("Services_Resources, Services_View_Index_Status")%>
                </th>
            </tr>
        </thead>
        <tbody>
            <% foreach (var item in Model)
               { %>
            <tr>
                <td>
                    <%= Html.Encode(item.IsRegular ? "Regular" : "Once") %>
                </td>
                <td>
                    <a href='<%= Url.Action("Edit", new {id=item.ServiceId}) %>'><%= Html.Encode(item.Name)%></a>
                    <%if(!string.IsNullOrEmpty(item.SystemData)){ %>
                    (System)
                    <%} %>
                </td>
                 <td>
                    <%= Html.Encode(item.Comment) %>
                </td>
                <td>
                    <%= Html.Encode(String.Format("{0:F}", item.Cost)) %>
                </td>
                <td>
                    <%= Html.Encode(item.Active ? "Active" : "Disable")%>
                </td>
            </tr>
            <% } %>
        </tbody>
    </table>
    <p>
        <%= Html.ActionLink(Html.Resource("Services_Resources, Services_View_Index_CreateNew"), "Create") %>&nbsp;|&nbsp;
        <%= Html.ActionLink(Html.Resource("Services_Resources, Services_View_Index_ServiceActivity"), "Activities")%>
    </p>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
