using Microsoft.Owin;
using Owin;
using System;
using System.Data.Entity;
using System.Data.Entity.SqlServer;

[assembly: OwinStartupAttribute(typeof(LearningPlattform.Startup))]
namespace LearningPlattform
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
    //This class is used to configure connection resiliency for azure database servers
    //Only supports in Entity Framework 6 and above
    //public class MyConfiguration : DbConfiguration
    //{
    //    public MyConfiguration()
    //    {
    //        SetExecutionStrategy(
    //            "System.Data.SqlClient",
    //            () => new SqlAzureExecutionStrategy(1, TimeSpan.FromSeconds(30)));
    //        //First parameter = number of retries
    //        //Second parameter = number of seconds to start retry
    //    }
    //}
}
