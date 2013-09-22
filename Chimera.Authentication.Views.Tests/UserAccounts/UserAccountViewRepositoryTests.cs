using System;
using Chimera.Authentication.Views.MongoDB.UserAccounts;
using Chimera.Authentication.Views.UserAccounts;
using NUnit.Framework;
using Xyperico.Base.Exceptions;


namespace Chimera.Authentication.Views.Tests.UserAccounts
{
  [TestFixture]
  public class UserAccountViewRepositoryTests : TestHelper
  {
    IUserAccountViewRepository UserAccountViewRepository = new UserAccountViewRepository();


    [Test]
    public void CanAddAndGetUser()
    {
      // Arrange
      UserAccountView u1 = UserAccountViewBuilder.BuildUserAccountView("Klaus", "klaus@klaus.kl");

      // Act
      UserAccountView u2 = UserAccountViewRepository.Get(u1.Id);

      // Assert
      Assert.IsNotNull(u2);
      Assert.AreEqual(u1.Id, u2.Id);
      Assert.AreNotEqual(u1.Id, Guid.Empty, "Persistence layer must assign IDs");
      Assert.AreEqual(u1.UserName, u2.UserName);
    }


    [Test]
    public void CanGetUserByUserName()
    {
      // Arrange
      UserAccountView u1 = UserAccountViewBuilder.BuildUserAccountView();

      // Act
      UserAccountView u2 = UserAccountViewRepository.GetByUserName(u1.UserName);

      // Assert
      Assert.IsNotNull(u2);
      Assert.AreEqual(u1.Id, u2.Id);
      Assert.AreEqual(u1.UserName, u2.UserName);
    }


    [Test]
    public void WhenGettingUserByUserNameItIgnoresCasing()
    {
      // Arrange
      UserAccountView u1 = UserAccountViewBuilder.BuildUserAccountView();

      // Act
      UserAccountView u2 = UserAccountViewRepository.GetByUserName(u1.UserName.ToLower());
      UserAccountView u3 = UserAccountViewRepository.GetByUserName(u1.UserName.ToUpper());

      // Assert
      Assert.IsNotNull(u2);
      Assert.AreEqual(u1.Id, u2.Id);
      Assert.AreEqual(u1.UserName, u2.UserName);
      Assert.IsNotNull(u3);
      Assert.AreEqual(u1.Id, u3.Id);
      Assert.AreEqual(u1.UserName, u3.UserName);
    }


    [Test]
    public void WhenGettingUnknownUserByUserNameItThrowsMissingResource()
    {
      AssertThrows<MissingResourceException>(() => UserAccountViewRepository.GetByUserName("unknownuser"));
    }


    [Test]
    public void CanGetUserByEMail()
    {
      // Arrange
      UserAccountView u1 = UserAccountViewBuilder.BuildUserAccountView();

      // Act
      UserAccountView u2 = UserAccountViewRepository.GetByEMail(u1.EMail);

      // Assert
      Assert.IsNotNull(u2);
      Assert.AreEqual(u1.Id, u2.Id);
      Assert.AreEqual(u1.EMail, u2.EMail);
    }


    [Test]
    public void WhenGettingUserByEMailItIgnoresCasing()
    {
      // Arrange
      UserAccountView u1 = UserAccountViewBuilder.BuildUserAccountView();

      // Act
      UserAccountView u2 = UserAccountViewRepository.GetByEMail(u1.EMail.ToLower());
      UserAccountView u3 = UserAccountViewRepository.GetByEMail(u1.EMail.ToUpper());

      // Assert
      Assert.IsNotNull(u2);
      Assert.AreEqual(u1.Id, u2.Id);
      Assert.AreEqual(u1.EMail, u2.EMail);
      Assert.IsNotNull(u3);
      Assert.AreEqual(u1.Id, u3.Id);
      Assert.AreEqual(u1.EMail, u3.EMail);
    }


    [Test]
    public void WhenGettingUnknownUserByEMailItThrowsMissingResource()
    {
      AssertThrows<MissingResourceException>(() => UserAccountViewRepository.GetByEMail("unknownemail"));
    }
  }
}
