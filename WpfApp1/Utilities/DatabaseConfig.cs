using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Utilities
{
    public static class DatabaseConfig
    {
        public static string SubscriptionDbConnectionString { get; } =
            "metadata=res://*/BaseModel.csdl|res://*/BaseModel.ssdl|res://*/BaseModel.msl;" +
            "provider=System.Data.SqlClient;" +
            "provider connection string=\"data source=localhost;initial catalog=SUBSCRIPTION_BD;User Id=Admin;Password=Pa$sw0rd;encrypt=False;TrustServerCertificate=True;MultipleActiveResultSets=True;App=EntityFramework\"";
    }
}
