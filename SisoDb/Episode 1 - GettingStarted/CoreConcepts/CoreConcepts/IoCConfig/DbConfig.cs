using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ninject.Modules;
using SisoDb;
using SisoDb.Sql2012;
using Ninject.Web.Common;

namespace CoreConcepts.IoCConfig
{
    public class DbConfig : NinjectModule
    {
        public override void Load()
        {
            var db = "CoreConcepts".CreateSql2012Db();
            Kernel.Bind<ISisoDatabase>()
                    .ToMethod(ctx => db)
                    .InSingletonScope();

            db.CreateIfNotExists();

            Kernel.Bind<ISession>()
                    .ToMethod(ctx => db.BeginSession())
                    .InRequestScope();
        }
    }
}