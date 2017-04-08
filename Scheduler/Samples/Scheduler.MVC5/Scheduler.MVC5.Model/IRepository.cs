using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Scheduler.MVC5.Model.Models;

namespace Scheduler.MVC5.Model
{
    public interface IRepository
    {
        IQueryable<ColoredEvent> ColoredEvents { get; }
        bool CreateColoredEvent(ColoredEvent instance);
        bool UpdateColoredEvent(ColoredEvent instance);
        bool RemoveColoredEvent(int id);

        IQueryable<Event> Events { get; }
        bool CreateEvents(Event instance);
        bool UpdateEvents(Event instance);
        bool RemoveEvents(int id);

        IQueryable<Room> Rooms { get; }
        bool CreateRoom(Room instance);
        bool UpdateRoom(Room instance);
        bool RemoveRoom(int id);

        IQueryable<Grid> Grids { get; }
        bool CreateGrid(Grid instance);
        bool UpdateGrid(Grid instance);
        bool RemoveGrid(int id);

        IQueryable<Recurring>Recurrings { get; }
        bool CreateRecurring(Recurring instance);
        bool UpdateRecurring(Recurring instance);
        bool RemoveRecurring(int id);
        bool RemoveRecurringCondition(IQueryable<Recurring> instances);

        IQueryable<ValidEvent>ValidEvents { get; }
        bool CreateValidEvent(ValidEvent instance);
        bool UpdateValidEvent(ValidEvent instance );
        bool RemoveValidEvent(int id);

        IQueryable<User> Users { get; } 
    }
}
