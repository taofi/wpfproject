using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Utilities;

namespace WpfApp1
{
    public partial class SUBSCRIPTION_BDEntities1 : DbContext
    {
        public SUBSCRIPTION_BDEntities1()
            : base(DatabaseConfig.SubscriptionDbConnectionString)
        {
            Database.CreateIfNotExists();
        }

    }
}
