using Chimera.Authentication.Contract.UserAccounts;
using Chimera.Authentication.Contract.UserAccounts.Events;
using Chimera.Authentication.Views.UserAccounts;
using Xyperico.Base.CommonDomainTypes;
using Xyperico.Base.Testing;


namespace Chimera.Authentication.Views.Tests.UserAccounts
{
  public class UserAccountViewBuilder : DisposingBuilder<UserAccountView>, IUserAccountViewBuilder
  {
    #region Dependencies

    public IUserAccountViewRepository UserAccountViewProvider { get; set; }

    #endregion


    #region IUserBuilder Members

    public UserAccountView BuildUserAccountView(string userName = null, string email = null)
    {
      UserAccountCreatedEvent e = new UserAccountCreatedEvent(new UserAccountId(), "John", new EMail("john@john.john"));
      UserAccountView u = new UserAccountView(e, 1);
      UserAccountViewProvider.Add(u);
      RegisterInstance(u);
      return u;
    }

    #endregion


    protected override void DisposeInstance(UserAccountView user)
    {
      UserAccountViewProvider.Remove(user.Id);
    }
  }
}
