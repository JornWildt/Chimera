using Chimera.Authentication.Views.MongoDB.UserAccounts;
using Chimera.Authentication.Views.UserAccounts;
using MongoDB.Bson.Serialization;
using Xyperico.Base;


namespace Chimera.Authentication.Views.MongoDB
{
  public static class Utility
  {
    public static void Initialize(IObjectContainer container)
    {
      Xyperico.Base.MongoDB.Utility.Initialize();

      BsonClassMap.RegisterClassMap<UserAccountView>(cm =>
      {
        cm.AutoMap();
        cm.GetMemberMap("EMailLowercase").SetIgnoreIfNull(true);
        //cm.GetMemberMap("ExternalLogins").SetIgnoreIfNull(true);
      });

      ConfigureDependencies(container);
    }


    private static void ConfigureDependencies(IObjectContainer container)
    {
      container.AddComponent<IUserAccountViewRepository, UserAccountViewRepository>();
    }
  }
}
