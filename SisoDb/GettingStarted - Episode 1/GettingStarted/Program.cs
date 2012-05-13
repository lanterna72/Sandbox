using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SisoDb;
using SisoDb.Sql2012;

namespace GettingStarted
{
    class Program
    {
        static void Main(string[] args)
        {
            // 1.   Install SisoDb via Nuget: Install-Package SisoDb.Sql2012

            // 2.   Create ISisoDatabase via extension method.
            //      This is meant to be a long-lived object
            ISisoDatabase db = @"Data Source=.\SqlExpress;Initial Catalog=Foo;Integrated Security=true;".CreateSql2012Db();

            // 3.   Ensure that you __always__ have a new/clean db.
            //      This drops the existing db.
            db.EnsureNewDatabase();

            // 4.   See Customer.cs
            var customer = new Customer { CustomerNo = 1001, Name = "Julie" };

            // 5.   Create a Session
            //      This is meant to be a short-lived object.
            //      Create it, consume it, dispose of it.  
            //      Why? Because the session opens the connection and holds a transaction against the db.
            using (ISession session = db.BeginSession())
            {
                // 6. You have several options (see intellisense).
                //    If you use InsertMany(), SisoDb will use BulkInsert.
                session.Insert(customer);
            }

            // 7.   The above using block could be simplified for such a simple call.
            db.UseOnceTo().Insert(customer); 

            using (var session = db.BeginSession())
            {
                // 8.   Most performant way to retrieve an object is by Id (or multiple Id's)
                var refetch = session.GetById<Customer>(customer.Id);
                refetch.Name = "Julie Lerman";

                session.Update(refetch);
            }

            // 9.   Querying uses Lambda Expressions and can return different types of objects
            Customer match = db.UseOnceTo().Query<Customer>().Where(c => c.CustomerNo == 1001).Single();

            // 10.  Querying can return Json
            string jsonMatch = db.UseOnceTo().Query<Customer>().Where(c => c.CustomerNo == 1001).SingleAsJson();

            Console.WriteLine(jsonMatch);

            db.UseOnceTo().DeleteById<Customer>(customer.Id);
            var exists = db.UseOnceTo().Query<Customer>().Exists(match.Id);

            Console.WriteLine(exists);

        }
    }
}
