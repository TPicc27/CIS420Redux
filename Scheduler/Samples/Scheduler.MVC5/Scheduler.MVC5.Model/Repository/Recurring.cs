using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject.Activation;
using Scheduler.MVC5.Model.Models;

namespace Scheduler.MVC5.Model
{
    public partial class Repository
    {
        public IQueryable<Recurring> Recurrings { get { return Db.Recurrings; } }
        public bool CreateRecurring(Recurring instance)
        {
            if (instance.id == 0)
            {
                Db.Recurrings.Add(instance);
                Db.SaveChanges();
                return true;
            }
            return false;
        }

        public bool UpdateRecurring(Recurring instance)
        {
            var cache = Db.Recurrings.FirstOrDefault(o => o.id == instance.id);
            if (cache != null)
            {
                Db.Entry(cache).CurrentValues.SetValues(instance);
                Db.SaveChanges();
                return true;
            }
            return false;

        }

        public bool RemoveRecurring(int id)
        {
            var instance = Db.Recurrings.FirstOrDefault(o => o.id == id);
            if (instance != null)
            {
                Db.Recurrings.Remove(instance);
                Db.SaveChanges();
                return true;
            }
            return false;
        }

        public bool RemoveRecurringCondition(IQueryable<Recurring> instances)
        {
            if (instances.Any())
            {
                Db.Recurrings.RemoveRange(instances);
                Db.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
