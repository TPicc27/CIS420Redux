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
        public IQueryable<User> Users { get { return Db.Users; }}
    }
}
