using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Data.Entity.Design.Extensibility;
using System.ComponentModel.Composition;
using System.Xml.Linq;

namespace VSIXProject1
{
    [Export(typeof(IEntityDesignerExtendedProperty))]
    [EntityDesignerExtendedProperty(EntityDesignerSelection.ConceptualModelEntityType)]
    public class AggregateRootFactory : IEntityDesignerExtendedProperty
    {
        public object CreateProperty(XElement element, PropertyExtensionContext context)
        {
            var edmXName = XName.Get("Key", "http://schemas.microsoft.com/ado/2008/09/edm");
            var keys = element.Parent.Element(edmXName).Elements().Select(e => e.Attribute("Name").Value);

            if (keys.Contains(element.Attribute("Name").Value))
                return new AggregateRootValue(element, context);

            return null;
        }
    }
}
