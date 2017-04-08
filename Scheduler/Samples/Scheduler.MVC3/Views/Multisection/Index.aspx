<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/scheduler.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Scheduler | Multisection events
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="server">
<% Html.RenderPartial("Scheduler"); %> 
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="SampleTitle" runat="server">Multisection events
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ShortDescription" runat="server">
Event can be assigned to several sections of Timeline or Units at the same time.
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="LongDescription" runat="server">
Try to drag multisection event. Double click on the event to see the details.
</asp:Content>
