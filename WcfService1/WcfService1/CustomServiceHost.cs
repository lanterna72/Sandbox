using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ServiceModel;
using System.ServiceModel.Description;

namespace WcfService1
{
	public class CustomServiceHost : ServiceHost
	{
		public CustomServiceHost(Type serviceType, Uri[] baseAddresses)
			: base(serviceType, baseAddresses)
		{

		}

		public CustomServiceHost(string constructorString, Uri[] baseAddresses)
			: base(constructorString, baseAddresses)
		{

		}

		public override ReadOnlyCollection<ServiceEndpoint> AddDefaultEndpoints()
		{
			var endpoints =  base.AddDefaultEndpoints();
			return endpoints;
		}

		public override void AddServiceEndpoint(ServiceEndpoint endpoint)
		{
			base.AddServiceEndpoint(endpoint);
		}

		protected override void ApplyConfiguration()
		{
			base.ApplyConfiguration();

			var endpoings = Description.Endpoints;
			var e = AddDefaultEndpoints();
		}

		protected override ServiceDescription CreateDescription(out IDictionary<string, ContractDescription> implementedContracts)
		{
			return base.CreateDescription(out implementedContracts);
		}

		protected override void InitializeRuntime()
		{
			var endpoings = Description.Endpoints;
			var e = AddDefaultEndpoints();
			base.InitializeRuntime();
		}
	}
}