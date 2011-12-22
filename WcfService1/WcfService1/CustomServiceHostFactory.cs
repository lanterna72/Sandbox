using System;
using System.ServiceModel;
using System.ServiceModel.Activation;

namespace WcfService1
{
	public class CustomServiceHostFactory : ServiceHostFactory
	{
		public override ServiceHostBase CreateServiceHost(string constructorString, Uri[] baseAddresses)
		{
			return new CustomServiceHost(constructorString, baseAddresses);
			//return new ServiceHost(constructorString, baseAddresses);
		}

		protected override ServiceHost CreateServiceHost(Type serviceType, Uri[] baseAddresses)
		{
			return new CustomServiceHost(serviceType, baseAddresses);
			//return new ServiceHost(serviceType, baseAddresses);
		}
	}
}