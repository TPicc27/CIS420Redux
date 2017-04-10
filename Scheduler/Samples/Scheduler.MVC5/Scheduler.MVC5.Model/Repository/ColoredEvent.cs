using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Scheduler.MVC5.Model.Models;

namespace Scheduler.MVC5.Model
{
    public partial class Repository
    {
        public IQueryable<ColoredEvent> ColoredEvents
        {
            get { return Db.ColoredEvents; }
        }

        public bool CreateColoredEvent(ColoredEvent instance)
        {
            if (instance.id == 0)
            {
                Db.ColoredEvents.Add(instance);
                Db.SaveChanges();
                return true;
            }
            return false;
        }

        public bool UpdateColoredEvent(ColoredEvent instance)
        {
            var cache = Db.ColoredEvents.FirstOrDefault(o => o.id == instance.id);
            if (cache != null)
            {
                Db.Entry(cache).CurrentValues.SetValues(instance);
                Db.SaveChanges();
                return true;
            }
            return false;
        }

        public bool RemoveColoredEvent(int id)
        {
            var instance = Db.ColoredEvents.FirstOrDefault(o => o.id == id);
            if (instance != null)
            {
                Db.ColoredEvents.Remove(instance);
                Db.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
