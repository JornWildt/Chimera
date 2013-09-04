using System.Runtime.Serialization;
using CuttingEdge.Conditions;
using Xyperico.Agres;
using Xyperico.Base.CommonDomainTypes;


namespace Chimera.Authentication.Core.UserAccounts.Events
{
  [DataContract]
  public class UserAccountCreatedEvent : IEvent
  {
    [DataMember(Order = 1)]
    public UserAccountId Id { get; private set; }

    [DataMember(Order = 2)]
    public string UserName { get; private set; }

    [DataMember(Order = 3)]
    public EMail EMail { get; private set; }


    public UserAccountCreatedEvent(UserAccountId id, string userName, EMail email)
    {
      Condition.Requires(id, "id").IsNotNull();
      Condition.Requires(userName, "userName").IsNotNull();
      Condition.Requires(email, "email").IsNotNull();

      Id = id;
      UserName = userName;
      EMail = email;
    }
  }
}
