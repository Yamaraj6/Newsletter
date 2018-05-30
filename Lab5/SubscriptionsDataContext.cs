using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;
using System.Web;
using System.Data.Entity;

namespace Lab5
{
    public class SubscriptionsDataContext : DbContext
    {
        public DbSet<Subscription> Subscriptions { get; set; }
        public SubscriptionsDataContext() : base("admindatabaseConnectionString")
        {
        }
    }
}