using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    public partial class SUBSCRIPTION_BDEntities1 : DbContext
    {
        public SUBSCRIPTION_BDEntities1()
            : base("name=SUBSCRIPTION_BDEntities1")
        {
            Database.CreateIfNotExists();
        }

    }
}
