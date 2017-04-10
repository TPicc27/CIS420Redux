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
        public IQueryable<Grid> Grids { get { return Db.Grids; } }
        public bool CreateGrid(Grid instance)
        {
            if (instance.id == 0)
            {
                Db.Grids.Add(instance);
                Db.SaveChanges();
                return true;
            }
            return false;
        }

        public bool UpdateGrid(Grid instance)
        {
            var cache = Db.Grids.FirstOrDefault(o => o.id == instance.id);
            if (cache != null)
            {
                Db.Entry(cache).CurrentValues.SetValues(instance);
                Db.SaveChanges();
                return true;
            }
            return false;
        }

        public bool RemoveGrid(int id)
        {
            var instance = Db.Grids.FirstOrDefault(o => o.id == id);
            if (instance != null)
            {
                Db.Grids.Remove(instance);
                Db.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
