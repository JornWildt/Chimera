using Chimera.Authentication.Contract.UserAccounts;
using Chimera.Authentication.Contract.UserAccounts.Commands;
using Chimera.Authentication.Core.UserAccounts;
using Chimera.Authentication.Service.UserAccounts;
using NUnit.Framework;
using Xyperico.Agres.EventStore;
using Xyperico.Agres.ProtoBuf;
using Xyperico.Agres.Serialization;
using Xyperico.Agres.SQLite;
using Xyperico.Base.CommonDomainTypes;
using System;
using Xyperico.Agres.JsonNet;


namespace Chimera.Authentication.Core.Tests.UserAccounts
{
  [TestFixture]
  public class UserAccountTests : TestHelper
  {
    IAppendOnlyStore AppendOnlyStore;
    IEventStore EventStore;
    GenericRepository<UserAccount, UserAccountState, UserAccountId> Repository;
    UserAccountApplicationService UserAccountApplicationService;


    protected override void SetUp()
    {
      base.SetUp();
      AppendOnlyStore = new SQLiteAppendOnlyStore(SetupFixture.SqlConnectionString, false);
      //ISerializer serializer = new ProtoBufSerializer();
      ISerializer serializer = new JsonNetSerializer();
      EventStore = new EventStoreDB(AppendOnlyStore, serializer);
      Repository = new GenericRepository<UserAccount, UserAccountState, UserAccountId>(EventStore);
      UserAccountApplicationService = new UserAccountApplicationService(EventStore, UserNameValidator);
    }


    [Test]
    public void CanCreateAndReloadUserAccount()
    {
      // Arrange
      CreateUserAccountCommand cmd = new CreateUserAccountCommand(new UserAccountId(Guid.NewGuid()), "Johny", new EMail("johny@somewhere.org"), "123456");

      // Act
      UserAccountApplicationService.Handle(cmd);
      var user = Repository.Get(cmd.Id);

      // Assert
      Assert.IsNotNull(user);
      Assert.IsNotNull(user.Aggregate);
      Assert.AreEqual(cmd.Id, user.Aggregate.State.Id);
      Assert.AreEqual(cmd.UserName, user.Aggregate.State.UserName);
      Assert.AreEqual(cmd.EMail, user.Aggregate.State.EMail);
      Assert.IsNotNull(user.Aggregate.State.PasswordSalt);
      Assert.IsNotNull(user.Aggregate.State.PasswordHash);
      Assert.AreEqual(Configuration.Settings.PasswordHashAlgorithm, user.Aggregate.State.PasswordHashAlgorithm);
    }
  }
}
