<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/scheduler.Master" Inherits="System.Web.Mvc.ViewPage<SchedulerTest.Controllers.SkinLocaleController.LocaleData>" %>

<asp:Content ID="Content5" ContentPlaceHolderID="SampleTitle" runat="server">
    Skins and languages
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="ShortDescription" runat="server">
    Available skins and languages
</asp:Content>
<asp:Content ID="Content7" ContentPlaceHolderID="LongDescription" runat="server">
    There are four skins ('standart', 'glossy', 'terrace' and 'flat') and 28 languages available.<br />
    Select skin and language and press ‘Apply’
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="server">
   <form method="get" action="/SkinLocale" id="top_form" style="text-align:center;padding:5px;font-family: 'Helvetica';" class="contenttext">

Skin:<select id="skin_select" name="skin" >
    <% foreach (var skin in (string[])Enum.GetNames(typeof(DHTMLX.Scheduler.DHXScheduler.Skins)))
		  { %>
        <option value="<%= skin%>" id="<%= skin%>"><%= skin%></option>
    <% } %> 
     </select>&nbsp;&nbsp;
Language:<select id="language_select" name="language">
<% foreach(var locale in (string[]) Enum.GetNames(typeof(DHTMLX.Scheduler.SchedulerLocalization.Localizations))){ %>
        <option value="<%= locale%>" id="<%= locale%>"><%= locale%></option>
   <%  }%>
   
</select>
</form>
<script type="text/javascript">
	function $(id) {
		return document.getElementById(id);
	}
	$("skin_select").onchange = $("language_select").onchange = function () { $("top_form").submit(); };

	<% if(!string.IsNullOrEmpty(Model.skin)){ %>
		$("<%= Model.skin %>").selected = "selected";
	<%} %>
	<% if(!string.IsNullOrEmpty(Model.locale)){ %>
		$("<%= Model.locale %>").selected = "selected";
	<%} %>

    
</script>

    <%= Model.scheduler.Render() %> 

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="TitleContent" runat="server">
Scheduler | Available skins
</asp:Content>
