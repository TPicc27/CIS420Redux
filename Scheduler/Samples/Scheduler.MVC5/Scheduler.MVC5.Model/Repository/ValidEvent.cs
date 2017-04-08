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
        public IQueryable<ValidEvent> ValidEvents { get {return Db.ValidEvents; } }
        public bool CreateValidEvent(ValidEvent instance)
        {
            
                Db.ValidEvents.Add(instance);
                Db.SaveChanges();
                return true;
            
        }

        public bool UpdateValidEvent(ValidEvent instance)
        {
            var cache = Db.ValidEvents.FirstOrDefault(o => o.id == instance.id);
            if (cache != null)
            {
                Db.Entry(cache).CurrentValues.SetValues(instance);
                Db.SaveChanges();
                return true;
            }
            return false;
        }

        public bool RemoveValidEvent(int id)
        {
            var instance = Db.ValidEvents.FirstOrDefault(o => o.id == id);
            if (instance != null)
            {
                Db.ValidEvents.Remove(instance);
                Db.SaveChanges();
                return true;

            }
            return false;
        }
    }
}
