using System.Runtime.Serialization;
using CuttingEdge.Conditions;
using Xyperico.Agres;
using Xyperico.Base.CommonDomainTypes;
using System;


namespace Chimera.Authentication.Core.UserAccounts.Events
{
  [DataContract]
  public class PasswordChangedEvent : IEvent
  {
    [DataMember(Order = 1)]
    public UserAccountId Id { get; private set; }

    [DataMember(Order = 2)]
    public byte[] PasswordHash { get; private set; }

    [DataMember(Order = 3)]
    public byte[] PasswordSalt { get; private set; }

    [DataMember(Order = 4)]
    public string PasswordHashAlgorithm { get; private set; }


    public PasswordChangedEvent(UserAccountId id, byte[] passwordSalt, byte[] passwordHash, string passwordHashingAlgorithm)
    {
      Condition.Requires(id, "id").IsNotNull();

      if ((passwordSalt != null || passwordHash != null) && string.IsNullOrEmpty(passwordHashingAlgorithm))
        throw new InvalidOperationException("Missing passwordHashingAlgorithm");

      Id = id;
      PasswordHash = passwordHash;
      PasswordSalt = passwordSalt;
      PasswordHashAlgorithm = passwordHashingAlgorithm;
    }
  }
}
