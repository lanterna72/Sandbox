using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Microsoft.Data.Entity.Design.Extensibility;
using System.ComponentModel;

namespace VSIXProject1
{
    public class AggregateRootValue
    {
        internal static XName AggregateRootElementName = XName.Get("AggregateRoot", "http://efex");

        private readonly XElement _property;
        private readonly PropertyExtensionContext _context;

        public AggregateRootValue(XElement parent, PropertyExtensionContext context)
        {
            _property = parent;
            _context = context;
        }

        [DisplayName("Aggregate Root")]
        [Description("Determines if an entity is an Aggregate Root")]
        [Category("Extensions")]
        [DefaultValue(true)]
        public string AggregateRoot
        {
            get
            {
                XElement child = _property.Element(AggregateRootElementName);
                return (child == null) ? bool.TrueString : child.Value;
            }
            set
            {
                using (EntityDesignerChangeScope scope = _context.CreateChangeScope("Set AggregateRoot"))
                {
                    var element = _property.Element(AggregateRootElementName);
                    if (element == null)
                        _property.Add(new XElement(AggregateRootElementName, value));
                    else
                        element.SetValue(value);
                    scope.Complete();
                }
            }
        }
    }
}
