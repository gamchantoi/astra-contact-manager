﻿<%@ Import Namespace="ContactManager.Models"%>
<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<%
    if (Request.IsAuthenticated) {
%>
Active Server:
<% var ctx = new CurrentContext();%>
<% var host = ctx.GetCurrentHost();%>
<%= host != null ? host.Address : "No server selected." %>
        Welcome <b><%= Html.Encode(Page.User.Identity.Name) %></b>!
        [ <%= Html.ActionLink("Log Off", "LogOff", "Account") %> ]
<%
    }
    else {
%> 
        [ <%= Html.ActionLink("Log On", "LogOn", "Account") %> ]
<%
    }
%>
