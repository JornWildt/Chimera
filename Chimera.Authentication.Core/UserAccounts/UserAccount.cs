using System.Collections.Generic;
using System.Linq;
using Chimera.Authentication.Core.UserAccounts.Commands;
using CuttingEdge.Conditions;
using Xyperico.Agres;
using Xyperico.Agres.EventStore;
using Xyperico.Base.CommonDomainTypes;
using Xyperico.Base.Crypto;
using Chimera.Authentication.Core.UserAccounts.Events;


namespace Chimera.Authentication.Core.UserAccounts
{
  public class UserAccount : AbstractAggregate<UserAccountId>
  {
    public class UserAccountData
    {
      public string UserName { get; protected set; }

      public EMail EMail { get; protected set; }

      public byte[] PasswordHash { get; protected set; }

      public byte[] PasswordSalt { get; protected set; }

      public string PasswordHashAlgorithm { get; protected set; }
    }


    public UserAccountData Data { get; protected set; }


    public UserAccount(IEnumerable<IEvent> events)
      : base(events)
    {
      Data = new UserAccountData();
    }

    
    #region Public command handlers

    public void Create(CreateUserAccountCommand cmd, IUserNameValidator userNameValidator, PasswordPolicy passwordPolicy)
    {
      Condition.Requires(cmd, "cmd").IsNotNull();
      Condition.Requires(userNameValidator, "userNameValidator").IsNotNull();

      if (!userNameValidator.IsValidUserName(cmd.UserName))
        throw new InvalidUserNameException(cmd.UserName);

      if (cmd.Password != null)
        SetPassword(cmd.Password, passwordPolicy);
    }


    public void Authenticate(AuthenticateCommand cmd)
    {
      Condition.Requires(cmd, "cmd").IsNotNull();
    }

    #endregion


    #region Internal stuff

    private void GeneratePasswordHash(string password, string passwordHashAlgorithm, out byte[] salt, out byte[] hash)
    {
      salt = RandomStringGenerator.GenerateRandomBytes(20);
      hash = PasswordHasher.GeneratePasswordHash(password, salt, passwordHashAlgorithm);
    }


    private bool PasswordMatches(string password)
    {
      if (Data.PasswordSalt == null || Data.PasswordHash == null)
        return false;
      byte[] hash = PasswordHasher.GeneratePasswordHash(password, Data.PasswordSalt, Data.PasswordHashAlgorithm);
      return hash.SequenceEqual(Data.PasswordHash);
    }


    private void SetPassword(string newPassword, PasswordPolicy passwordPolicy)
    {
      if (newPassword != null)
      {
        if (!passwordPolicy.IsValid(newPassword))
          throw new InvalidPasswordException(passwordPolicy.GetDescription(_.Auth.Password));

        string passwordHashAlgorithm = Configuration.Settings.PasswordHashAlgorithm;
        byte[] salt, hash;
        GeneratePasswordHash(newPassword, passwordHashAlgorithm, out salt, out hash);
        byte[] passwordSalt = salt;
        byte[] passwordHash = hash;

        Publish(new PasswordChangedEvent(Id, passwordSalt, passwordHash, passwordHashAlgorithm));
      }
      else
      {
        Publish(new PasswordChangedEvent(Id, null, null, null));
      }
    }

    #endregion


    #region Restore state
    #endregion
  }
}
