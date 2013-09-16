using System.Collections.Generic;
using System.Linq;
using Chimera.Authentication.Contract.UserAccounts;
using Chimera.Authentication.Contract.UserAccounts.Commands;
using Chimera.Authentication.Contract.UserAccounts.Events;
using CuttingEdge.Conditions;
using Xyperico.Agres;
using Xyperico.Agres.EventStore;
using Xyperico.Base.Crypto;


namespace Chimera.Authentication.Core.UserAccounts
{
  public class UserAccount : AbstractAggregate<UserAccountId, UserAccountState>
  {
    public UserAccount(IEnumerable<IEvent> events)
      : base(events)
    {
    }

    
    #region Public command handlers

    public void Create(CreateUserAccountCommand cmd, IUserNameValidator userNameValidator, PasswordPolicy passwordPolicy)
    {
      Condition.Requires(cmd, "cmd").IsNotNull();
      Condition.Requires(userNameValidator, "userNameValidator").IsNotNull();
      Condition.Requires(passwordPolicy, "passwordPolicy").IsNotNull();

      if (!userNameValidator.IsValidUserName(cmd.UserName))
        throw new InvalidUserNameException(cmd.UserName);

      Publish(new UserAccountCreatedEvent(cmd.Id, cmd.UserName, cmd.EMail));

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
      if (State.PasswordSalt == null || State.PasswordHash == null)
        return false;
      byte[] hash = PasswordHasher.GeneratePasswordHash(password, State.PasswordSalt, State.PasswordHashAlgorithm);
      return hash.SequenceEqual(State.PasswordHash);
    }


    private void SetPassword(string newPassword, PasswordPolicy passwordPolicy)
    {
      if (newPassword != null)
      {
        if (!passwordPolicy.IsValid(newPassword))
          throw new InvalidPasswordException(passwordPolicy.GetDescription(_.Auth.Password));

        string algorithm = XmlConfiguration.Settings.PasswordHashAlgorithm;
        byte[] salt, hash;
        GeneratePasswordHash(newPassword, algorithm, out salt, out hash);

        Publish(new PasswordChangedEvent(Id, salt, hash, algorithm));
      }
      else
      {
        Publish(new PasswordChangedEvent(Id, null, null, null));
      }
    }

    #endregion
  }
}
