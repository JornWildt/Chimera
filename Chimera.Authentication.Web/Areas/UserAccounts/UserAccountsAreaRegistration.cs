using System;
using System.Collections.Generic;
using System.Web.Mvc;
using log4net;
using Xyperico.Base;
using Chimera.Authentication.Core.UserAccounts;
//using Microsoft.Web.WebPages.OAuth;
//using BaseConfiguration = Xyperico.Authentication.Configuration;


namespace Chimera.Authentication.Web.Areas.UserAccounts
{
  public class UserAccountsAreaRegistration : AreaRegistration
  {
    ILog Logger = LogManager.GetLogger(typeof(UserAccountsAreaRegistration));

    public override string AreaName
    {
      get
      {
        return "UserAccounts";
      }
    }

    public override void RegisterArea(AreaRegistrationContext context)
    {
      Logger.Debug("Register UserAccounts area");

      context.MapRoute(
          "UserAccounts_default",
          "app/useraccounts/{controller}/{action}",
          new { controller = "login", action = "show" }
      );

      ConfigureDependencies(ObjectContainer.Container);
      //ConfigureAuthentication();
    }


    private void ConfigureDependencies(IObjectContainer container)
    {
      Chimera.Authentication.Views.MongoDB.Utility.Initialize(container);
      container.AddComponent<IUserNameValidator, FilebasedUserNameValidator>();
    }


#if false
    private void ConfigureAuthentication()
    {
      if (BaseConfiguration.Settings == null || BaseConfiguration.Settings.ExternalProviders == null)
        return;

      foreach (AuthenticationProviderSection providerCfg in BaseConfiguration.Settings.ExternalProviders)
      {
        if (providerCfg.Active)
        {
          Type providerType = Type.GetType(providerCfg.Type);
          object[] args = new object[]
          {
            providerCfg.ClientId,
            providerCfg.ClientSecret
          };
          IExternalAuthenticationProvider provider = (IExternalAuthenticationProvider)Activator.CreateInstance(providerType, args);

          OAuthWebSecurity.RegisterClient(provider.AuthenticationClient, providerCfg.DisplayName, new Dictionary<string, object>());
        }
      }
    }
#endif
  }
}
