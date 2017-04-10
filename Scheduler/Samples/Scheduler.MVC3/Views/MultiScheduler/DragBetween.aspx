<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/scheduler.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Scheduler | Drag and drop between schedulers
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="server">
<style>
    .column-left {
        float:left;
        border-right:1px solid #cecece;
    }
    .column-right {
        float:right;
        border-left:1px solid #cecece;
    }
    .column {
        height:100%;
        width:48%;
    }
</style>

<div class="column column-left">
<%= this.Model.sh1.Render() %>
</div>
<div class="column column-right">
<%= this.Model.sh2.Render() %>
</div>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="SampleTitle" runat="server">
Drag and drop between schedulers
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ShortDescription" runat="server">
Events can be dragged from one calendar to another
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="LongDescription" runat="server">
Try to create event in one of calendars and then drop it to another one. <br />
</asp:Content>
