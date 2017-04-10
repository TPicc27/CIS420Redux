﻿<%@ Page Title="" Language="VB" MasterPageFile="~/Views/Shared/scheduler.Master" Inherits="System.Web.Mvc.ViewPage(Of DHTMLX.Scheduler.DHXScheduler)" %>

<asp:Content ID="Content2" ContentPlaceHolderID="TitleContent" runat="server">
Scheduler | Custom details form
</asp:Content>



<asp:Content ID="Content5" ContentPlaceHolderID="SampleTitle" runat="server">
    Custom details form
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="ShortDescription" runat="server">
    You can customize details form.
</asp:Content>
<asp:Content ID="Content7" ContentPlaceHolderID="LongDescription" runat="server">
    Drag or double-click to create new event<br />
    Double-click on existing events to edit them.
</asp:Content>


<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="server">
   
    <% Html.RenderPartial("Scheduler") %> 
</asp:Content>