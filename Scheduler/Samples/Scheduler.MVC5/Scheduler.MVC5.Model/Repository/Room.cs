using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Scheduler.MVC5.Model.Models;

namespace Scheduler.MVC5.Model
{
    public partial class Repository
    {
       
        public IQueryable<Room> Rooms {
            get { return Db.Rooms; }
        }
        public bool CreateRoom(Room instance)
        {
            if (instance.key == 0)
            {
                Db.Rooms.Add(instance);
                Db.SaveChanges();
                return true;
            }
            return false;
        }

        public bool UpdateRoom(Room instance)
        {
            var cache = Db.Rooms.FirstOrDefault(o => o.key == instance.key);
            if (cache != null)
            {
                Db.Entry(cache).CurrentValues.SetValues(instance);
                Db.SaveChanges();
                return true;
            }
            return false;
        }

        public bool RemoveRoom(int id)
        {
            var instance = Db.Rooms.FirstOrDefault(o => o.key == id);
            if (instance != null)
            {
                Db.Rooms.Remove(instance);
                Db.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
