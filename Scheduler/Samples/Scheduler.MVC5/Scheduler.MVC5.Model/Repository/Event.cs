using System.Linq;
using Scheduler.MVC5.Model.Models;

namespace Scheduler.MVC5.Model
{
    public partial class Repository
    {
        public IQueryable<Event> Events
        {
            get { return Db.Events; }
        }

        public bool CreateEvents(Event instance)
        {
            if (instance.id == 0)
            {
                Db.Events.Add(instance);
                Db.SaveChanges();
                return true;
            }
            return false;
        }

        public bool UpdateEvents(Event instance)
        {
            var cache = Db.Events.FirstOrDefault(o => o.id == instance.id);
            if (cache != null)
            {
                Db.Entry(cache).CurrentValues.SetValues(instance);
                Db.SaveChanges();
                return true;
            }
            return false;
        }

        public bool RemoveEvents(int id)
        {
            var instance = Db.Events.FirstOrDefault(o => o.id == id);
            if (instance != null)
            {
                Db.Events.Remove(instance);
                Db.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
