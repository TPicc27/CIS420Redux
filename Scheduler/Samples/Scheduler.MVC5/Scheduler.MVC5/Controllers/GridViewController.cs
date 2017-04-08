using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DHTMLX.Common;
using DHTMLX.Scheduler;
using DHTMLX.Scheduler.Controls;
using DHTMLX.Scheduler.Data;
using Scheduler.MVC5.Model.Models;

namespace Scheduler.MVC5.Controllers
{
    public class GridViewController : BaseController
    {
        // GET: GridView
        public ActionResult Index()
        {
            var scheduler = new DHXScheduler(this);

            var grid = new GridView("grid");

            //adding the columns
            grid.Columns.Add(new GridViewColumn("text", "Event")//data property, label
            {
                Width = 300
            });

            grid.Columns.Add(new GridViewColumn("start_date", "Date")
            {   //can assign template for column contents
                // for more info about templates syntax see http://scheduler-net.com/docs/dhxscheduler.templates.html
                Template = @"<% if((ev.end_date - ev.start_date)/1440000 > 1){%>
                                {start_date:date(%d %M %Y)} - {end_date:date(%d %M %Y)} 
                            <% }else{ %> 
                                {start_date:date(%d %M %Y %H:%i)} 
                            <% } %>",
                Align = GridViewColumn.Aligns.Left,
                Width = 200
            });



            grid.Columns.Add(new GridViewColumn("details", "Details")
            {
                Align = GridViewColumn.Aligns.Left
            });

            scheduler.Views.Add(grid);

            scheduler.Lightbox.Add(new LightboxText("text", "Text"));
            scheduler.Lightbox.Add(new LightboxText("details", "Details"));
            scheduler.Lightbox.Add(new LightboxTime("time"));

            scheduler.InitialView = grid.Name;
            scheduler.InitialDate = new DateTime(2011, 9, 19);
            scheduler.LoadData = true;
            scheduler.EnableDataprocessor = true;
            return View(scheduler);
        }

        public ContentResult Data()
        {
            var data = new SchedulerAjaxData(Repository.Grids);

            return data;
        }

        public ContentResult Save(int? id, FormCollection actionValues)
        {
            var action = new DataAction(actionValues);

            try
            {
                var changedEvent = (Grid)DHXEventsHelper.Bind(typeof(Grid), actionValues);
                switch (action.Type)
                {
                    case DataActionTypes.Insert:
                        if (!Repository.CreateGrid(changedEvent))
                        {
                            action.Type = DataActionTypes.Error;
                        }
                        break;
                    case DataActionTypes.Delete:
                        changedEvent = Repository.Grids.SingleOrDefault(ev => ev.id == action.SourceId);
                        if (!Repository.RemoveGrid((int)action.SourceId))
                        {
                            action.Type = DataActionTypes.Error;
                        }
                        break;
                    default:// "update"                          
                        var eventToUpdate = Repository.Grids.SingleOrDefault(ev => ev.id == action.SourceId);
                        if (!Repository.UpdateGrid(changedEvent))
                        {
                            action.Type = DataActionTypes.Error;
                        }
                        DHXEventsHelper.Update(eventToUpdate, changedEvent, new List<string>() { "id" });
                        break;
                }
                //data.SubmitChanges();
                action.TargetId = changedEvent.id;
            }
            catch
            {
                action.Type = DataActionTypes.Error;
            }
            return (new AjaxSaveResponse(action));
        }

    }
}