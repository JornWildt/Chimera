using System.Configuration;
using Xyperico.Base;
using Xyperico.Web.Mvc;


namespace Chimera.Authentication.Web
{
  public class Configuration : ConfigurationSettingsBase<Configuration>
  {
    [ConfigurationProperty("LoginSuccessUrl")]
    public ActionUrlConfigurationElement LoginSuccessUrl
    {
      get { return (ActionUrlConfigurationElement)this["LoginSuccessUrl"]; }
    }


    [ConfigurationProperty("RegisterSuccessUrl")]
    public ActionUrlConfigurationElement RegisterSuccessUrl
    {
      get { return (ActionUrlConfigurationElement)this["RegisterSuccessUrl"]; }
    }
  }
}