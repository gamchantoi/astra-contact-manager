<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage<ContactManager.Models.PPPSecret>" %>
<%@ Import Namespace="ContactManager.Web.Helpers"%>

    <% using (Html.BeginForm()) {%>
    <%= Html.HiddenFor(s => s.UserId) %>

        <fieldset class="fields">
            <legend><a><%= Html.DisplayFor(s => s.Name) %></a></legend>
 <%--           <p>
                <%= Html.LabelFor(model => model.Name) %>
                <%= Html.TextBoxFor(model => model.Name) %>
                <%= Html.ValidationMessageFor(model => model.Name) %>
            </p>
            <p>
                <%= Html.LabelFor(model => model.Password) %>
                <%= Html.TextBoxFor(model => model.Password) %>
                <%= Html.ValidationMessageFor(model => model.Password) %>
            </p>
            <p>
                <%= Html.LabelFor(model => model.Comment) %>
                <%= Html.TextBoxFor(model => model.Comment) %>
                <%= Html.ValidationMessageFor(model => model.Comment) %>
            </p>--%>
            <p>
                <%= Html.Label( Html.Resource("PPP_Resources, PPP_View_Secret_Edit_Service")) %>
                <%= Html.TextBoxFor(model => model.Service) %>
                <%= Html.ValidationMessageFor(model => model.Service) %>
            </p>
            <p>
                <%= Html.Label( Html.Resource("PPP_Resources, PPP_View_Secret_Edit_CallerId")) %>
                <%= Html.TextBoxFor(model => model.CallerId) %>
                <%= Html.ValidationMessageFor(model => model.CallerId) %>
            </p>
            <p>
                <%= Html.Label( Html.Resource("PPP_Resources, PPP_View_Secret_Edit_LocalAddress")) %>
                <%= Html.TextBoxFor(model => model.LocalAddress) %>
                <%= Html.ValidationMessageFor(model => model.LocalAddress) %>
            </p>
            <p>
                <%= Html.Label( Html.Resource("PPP_Resources, PPP_View_Secret_Edit_RemoteAddress")) %>
                <%= Html.TextBoxFor(model => model.RemoteAddress) %>
                <%= Html.ValidationMessageFor(model => model.RemoteAddress) %>
            </p>
<%--            <p>
                <%= Html.LabelFor(model => model.DHCPAddress) %>
                <%= Html.TextBoxFor(model => model.DHCPAddress) %>
                <%= Html.ValidationMessageFor(model => model.DHCPAddress) %>
            </p>
            <p>
                <%= Html.LabelFor(model => model.MACAddress) %>
                <%= Html.TextBoxFor(model => model.MACAddress) %>
                <%= Html.ValidationMessageFor(model => model.MACAddress) %>
            </p>--%>
            <p>
                <%= Html.Label( Html.Resource("PPP_Resources, PPP_View_Secret_Edit_Routes")) %>
                <%= Html.TextBoxFor(model => model.Routes) %>
                <%= Html.ValidationMessageFor(model => model.Routes) %>
            </p>
            <p>
                <%= Html.Label( Html.Resource("PPP_Resources, PPP_View_Secret_Edit_LimitBytesIn")) %>
                <%= Html.TextBoxFor(model => model.LimitBytesIn) %>
                <%= Html.ValidationMessageFor(model => model.LimitBytesIn) %>
            </p>
            <p>
                <%= Html.Label( Html.Resource("PPP_Resources, PPP_View_Secret_Edit_LimitBytesOut")) %>
                <%= Html.TextBoxFor(model => model.LimitBytesOut) %>
                <%= Html.ValidationMessageFor(model => model.LimitBytesOut) %>
            </p>
<%--            <p>
                <%= Html.LabelFor(model => model.Disabled) %>
                <%= Html.CheckBox("Disabled", Model.Disabled.HasValue ? Model.Disabled : false) %>
                <%= Html.ValidationMessageFor(model => model.Disabled) %>
            </p>--%>
            <p>
                <input type="submit" value=<%= Html.Resource("PPP_Resources, PPP_View_Profile_Edit_Save") %> />
            </p>
        </fieldset>

    <% } %>
