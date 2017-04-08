Imports System.Collections.Generic
Imports System.Linq
Imports System.Web
Imports System.Web.Mvc
Imports DHTMLX.Scheduler
Imports DHTMLX.Scheduler.Controls
Namespace SchedulerTest.Controllers
    ''' <summary>
    ''' you can create multiple scheduler on page
    ''' </summary>
    Public Class MultiSchedulerController
        Inherits Controller
        '
        ' GET: /MultiScheduler/
        Public Class [mod]

            Public sh1 As DHXScheduler
            Public sh2 As DHXScheduler
        End Class
        Public Function Index() As ActionResult
            'each scheduler must have unique name
            Dim scheduler = New DHXScheduler("sched1")

            Dim scheduler2 = New DHXScheduler("sched2")

            Return View(New [mod]() With { _
             .sh1 = scheduler, _
             .sh2 = scheduler2 _
            })
        End Function

        Public Function DragBetween() As ActionResult
            'each scheduler must have unique name
            Dim scheduler = New DHXScheduler("sched1")
            scheduler.Extensions.Add(SchedulerExtensions.Extension.DragBetween)
            scheduler.InitialView = scheduler.Views(scheduler.Views.Count - 1).Name

            Dim scheduler2 = New DHXScheduler("sched2")
            Dim timeline = New TimelineView("timeline", "room_id")
            timeline.RenderMode = TimelineView.RenderModes.Bar

            Dim rooms = New List(Of Object)
            For i As Integer = 1 To 10
                rooms.Add(New With {.key = i, .label = String.Format("Room #{0}", i)})
            Next


            timeline.FitEvents = False
            scheduler2.Views.Add(timeline)
            timeline.AddOptions(rooms)
            timeline.X_Unit = TimelineView.XScaleUnits.Hour
            timeline.X_Size = 18
            timeline.AddSecondScale(TimelineView.XScaleUnits.Day, "%j, %M")
            timeline.X_Step = 4
            scheduler2.InitialView = timeline.Name

            Return View(New [mod]() With { _
             .sh1 = scheduler, _
             .sh2 = scheduler2 _
            })
        End Function
    End Class
End Namespace
