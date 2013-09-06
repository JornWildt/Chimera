using Chimera.Authentication.Contract.UserAccounts;
using Chimera.Authentication.Contract.UserAccounts.Events;
using Xyperico.Agres;
using Xyperico.Base.CommonDomainTypes;


namespace Chimera.Authentication.Core.UserAccounts
{
  public class UserAccountState : IHaveIdentity<UserAccountId>
  {
    public UserAccountId Id { get; protected set; }

    public string UserName { get; protected set; }

    public EMail EMail { get; protected set; }

    public byte[] PasswordHash { get; protected set; }

    public byte[] PasswordSalt { get; protected set; }

    public string PasswordHashAlgorithm { get; protected set; }


    #region Restore state

    public void RestoreFrom(UserAccountCreatedEvent e)
    {
      Id = e.Id;
      UserName = e.UserName;
      EMail = e.EMail;
    }


    public void RestoreFrom(PasswordChangedEvent e)
    {
      PasswordSalt = e.PasswordSalt;
      PasswordHash = e.PasswordHash;
      PasswordHashAlgorithm = e.PasswordHashAlgorithm;
    }

    #endregion
  }
}
