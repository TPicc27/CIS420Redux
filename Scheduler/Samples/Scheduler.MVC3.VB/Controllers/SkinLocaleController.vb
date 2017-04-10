Imports System.Collections.Generic
Imports System.Linq
Imports System.Web
Imports System.Web.Mvc
Imports DHTMLX.Scheduler
Imports DHTMLX.Scheduler.Data
Imports DHTMLX.Scheduler.Controls
Imports DHTMLX.Common


Namespace SchedulerTest.Controllers

	''' <summary>
	''' check available skins and localizations
	''' </summary>
	Public Class SkinLocaleController
		Inherits Controller
		'
		' GET: /SkinLocale/
		Public Class LocaleData
			Public scheduler As DHXScheduler
			Public locale As String
			Public skin As String
			Public Sub New(sched As DHXScheduler, loc As String, sk As String)
				scheduler = sched
				locale = loc
				skin = sk
			End Sub
		End Class
		Public Function Index() As ActionResult
            Dim locale = SchedulerLocalization.Localizations.English
            Dim skin = DHXScheduler.Skins.Standart
            Dim request_lang = Me.Request.QueryString("language")
            Dim request_skin = Me.Request.QueryString("skin")

            If (Not String.IsNullOrEmpty(request_lang)) Then
                locale = [Enum].Parse(GetType(SchedulerLocalization.Localizations), request_lang)
            Else
                request_lang = SchedulerLocalization.Localizations.English.ToString()
            End If

            If (Not String.IsNullOrEmpty(request_skin)) Then

                skin = [Enum].Parse(GetType(DHXScheduler.Skins), request_skin)
            Else
                request_skin = DHXScheduler.Skins.Standart.ToString()
            End If
           


            Dim scheduler = New DHXScheduler(Me)
            scheduler.Skin = skin
            scheduler.Localization.Set(locale)


            scheduler.InitialDate = New DateTime(2011, 11, 24)

            scheduler.XY.scroll_width = 0
            scheduler.Config.first_hour = 8
            scheduler.Config.last_hour = 19
            scheduler.Config.time_step = 30
            scheduler.Config.multi_day = True
            scheduler.Config.limit_time_select = True

            scheduler.Localization.Directory = "locale"

            Dim rooms = New DHXSchedulerDataContext().Rooms.ToList()

            Dim unit = New UnitsView("unit1", "room_id")
            unit.AddOptions(rooms)
            'parse model objects
            scheduler.Views.Add(unit)

            Dim timeline = New TimelineView("timeline", "room_id")
            timeline.RenderMode = TimelineView.RenderModes.Bar
            timeline.FitEvents = False
            timeline.AddOptions(rooms)
            scheduler.Views.Add(timeline)


            scheduler.EnableDataprocessor = True
            scheduler.LoadData = True
            scheduler.InitialDate = New DateTime(2011, 9, 19)
            Return View(New LocaleData(scheduler, request_lang, request_skin))
        End Function
		Public Function Data() As ContentResult

			Dim data__1 = New SchedulerAjaxData((New DHXSchedulerDataContext()).Events)


			Return data__1
		End Function

		Public Function Save(id As System.Nullable(Of Integer), actionValues As FormCollection) As ContentResult

			Dim action = New DataAction(actionValues)

			Dim data As New DHXSchedulerDataContext()
			Dim changedEvent = DirectCast(DHXEventsHelper.Bind(GetType([Event]), actionValues), [Event])
			Try
				Select Case action.Type
					Case DataActionTypes.Insert
						data.Events.InsertOnSubmit(changedEvent)
						Exit Select
					Case DataActionTypes.Delete
						changedEvent = data.Events.SingleOrDefault(Function(ev) ev.id = action.SourceId)
						data.Events.DeleteOnSubmit(changedEvent)
						Exit Select
					Case Else
						' "update"                          
						Dim eventToUpdate = data.Events.SingleOrDefault(Function(ev) ev.id = action.SourceId)
						DHXEventsHelper.Update(eventToUpdate, changedEvent, New List(Of String)() From { _
							"id" _
						})
						Exit Select
				End Select
				data.SubmitChanges()
				action.TargetId = changedEvent.id
			Catch a As Exception
				action.Type = DataActionTypes.[Error]
			End Try

			Return (New AjaxSaveResponse(action))
		End Function
	End Class
End Namespace
