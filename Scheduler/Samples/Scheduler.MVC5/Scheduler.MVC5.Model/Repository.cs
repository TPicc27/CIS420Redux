using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Core.Objects.DataClasses;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;
using Ninject;

namespace Scheduler.MVC5.Model
{
    public partial class Repository:IRepository
    {
        [Inject]
        public SchedulerContext Db { get; set; }
        
    }
}
