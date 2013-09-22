using System;
using Chimera.Authentication.Contract.UserAccounts;
using Chimera.Authentication.Contract.UserAccounts.Commands;
using Chimera.Authentication.Core.Service.UserAccounts;
using Chimera.Authentication.Core.UserAccounts;
using Chimera.Authentication.Shared;
using NUnit.Framework;
using Xyperico.Agres.EventStore;
using Xyperico.Agres.JsonNet;
using Xyperico.Agres.ProtoBuf;
using Xyperico.Agres.Serialization;
using Xyperico.Agres.SQLite;
using Xyperico.Base.CommonDomainTypes;


namespace Chimera.Authentication.Core.Tests.UserAccounts
{
  [TestFixture]
  public class UserAccountTests_Json : UserAccountTests<JsonNetSerializer>
  {
  }


  [TestFixture]
  public class UserAccountTests_ProtoBuf : UserAccountTests<ProtoBufSerializer>
  {
  }


  [TestFixture]
  public class UserAccountTests_DataContract : UserAccountTests<DataContractSerializer>
  {
  }


  public abstract class UserAccountTests<TSerializer> : TestHelper
    where TSerializer : ISerializer, new()
  {
    IAppendOnlyStore AppendOnlyStore;
    ISerializer Serializer;
    IEventStore EventStore;
    GenericRepository<UserAccount, UserAccountState, UserAccountId> Repository;
    UserAccountApplicationService UserAccountApplicationService;


    protected override void SetUp()
    {
      base.SetUp();
      AppendOnlyStore = new SQLiteAppendOnlyStore(SetupFixture.SqlConnectionString, false);
      Serializer = new TSerializer();
      EventStore = new EventStoreDB(AppendOnlyStore, Serializer);
      Repository = new GenericRepository<UserAccount, UserAccountState, UserAccountId>(EventStore);
      UserAccountApplicationService = new UserAccountApplicationService(EventStore, UserNameValidator);
    }


    [Test]
    public void CanSerializeUserAccountId()
    {
      // Arrange
      UserAccountId id1 = new UserAccountId(Guid.NewGuid());

      // Act
      byte[] data = Serializer.Serialize(id1);
      UserAccountId id2 = (UserAccountId)Serializer.Deserialize(data);

      // Assert
      Assert.IsNotNull(id2);
      Assert.AreEqual(id1, id2);
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
