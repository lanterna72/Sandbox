using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GettingStarted
{
    // 1. By default, SisoDb will make every public property indexed, thus queryable.
    public class Customer
    {
        // 2.   This member is mandatory.
        //      It can be called Id, CustomerId, or StructureId.
        //      It can be of type Guid, int, long, or Guid?, int?, or long?.
        //      It can also be a string.
        //      If you stick to Guid, int, or long, SisoDb will generate the values for you.
        public Guid Id { get; set; }

        public int CustomerNo { get; set; }
        public string Name { get; set; }
    }
}
