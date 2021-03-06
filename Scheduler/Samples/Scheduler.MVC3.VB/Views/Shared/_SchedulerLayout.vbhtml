﻿<!DOCTYPE html>
<html>
<head runat="server">
    <title>@RenderSection("TitleContent")</title>
    <style type="text/css">
	    html, body
	    {
	        height:100%;
	        padding:0px;
	        margin:0px;
	        overflow:hidden;
	    }    
	</style>
    <link href="@Url.Content("~/Content/samples.css")" rel="stylesheet" type="text/css" />   
</head>
<body> 
    <div id="header" class="header_class">  
        <div class="samp_header" >
            <div class="sample_title">@RenderSection("SampleTitle")</div>
            <div class="vertical_line"></div>
            <div class="desk_holder">
                <div class="short_description">@RenderSection("ShortDescription")</div>
                <div class="long_description">@RenderSection("LongDescription")</div>
            </div>     
        </div>   
    </div> 
    <div id="main_content">
        <script type="text/javascript">
            (function () {
                var hold = false;
                function initSize() {
                    if (hold) return;
                    setTimeout(function () { hold = false; }, 100);
                    hold = true;
                    var head = document.getElementById("header");
                    var headHeight = 1 * (head.offsetHeight || head.clientHeight);
                    document.getElementById("main_content").style.height = (window.innerHeight || window.document.body.clientHeight) - headHeight + "px";
                }
                if (window.attachEvent) {
                    window.attachEvent("onresize", initSize);
                } else {
                    window.addEventListener("resize", initSize, false);
                }
                initSize();
            })()
        </script>
        
       
        @RenderBody()   
    </div>
    
</body>
</html>
