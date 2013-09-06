using Chimera.Authentication.Core.UserAccounts;
using NUnit.Framework;
using Xyperico.Base;
using Xyperico.Agres.SQLite;


namespace Chimera.Authentication.Core.Tests
{
  [SetUpFixture]
  public class SetupFixture
  {
    public const string SqlConnectionString = "Data Source=ChimeraAuthenticationEventStore.db";


    public static void Setup(IObjectContainer container)
    {
      SQLiteAppendOnlyStore.CreateTableIfNotExists(SqlConnectionString);

      container.AddComponent<IUserNameValidator, FilebasedUserNameValidator>();

      Chimera.Authentication.Core.ApplicationStarter.Initialize();
    }


    [SetUp]
    public void TestSetup()
    {
      Xyperico.Base.Testing.TestHelper.ClearObjectContainer();
      Setup(Xyperico.Base.Testing.TestHelper.ObjectContainer);
    }
  }
}
