using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JSSerializerSample.Models
{
    public class IndexViewModel : BaseViewModel
    {
        public string Message { get; set; }
        public string UserName { get; set; }

        public ComplexViewModel ComplexProperty { get; set; }
    }
}