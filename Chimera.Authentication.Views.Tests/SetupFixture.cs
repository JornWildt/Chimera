using Chimera.Authentication.Views.Tests.UserAccounts;
using NUnit.Framework;
using Xyperico.Base;
using Xyperico.Base.Collections;


namespace Chimera.Authentication.Views.Tests
{
  [SetUpFixture]
  public class SetupFixture
  {
    public static void Setup(IObjectContainer container)
    {
      Chimera.Authentication.Views.MongoDB.Utility.Initialize(container);

      container.AddComponent<INameValueContextCollection, CallContextNamedValueCollection>();
      container.AddComponent<IUserAccountViewBuilder, UserAccountViewBuilder>();
    }


    [SetUp]
    public void TestSetup()
    {
      Xyperico.Base.Testing.TestHelper.ClearObjectContainer();
      Setup(Xyperico.Base.Testing.TestHelper.ObjectContainer);
    }
  }
}
