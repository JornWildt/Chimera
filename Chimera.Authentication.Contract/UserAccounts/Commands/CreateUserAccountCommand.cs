using System.Runtime.Serialization;
using CuttingEdge.Conditions;
using Xyperico.Agres;
using Xyperico.Base.CommonDomainTypes;


namespace Chimera.Authentication.Contract.UserAccounts.Commands
{
  [DataContract]
  public class CreateUserAccountCommand : ICommand<UserAccountId>
  {
    [DataMember(Order = 1)]
    public UserAccountId Id { get; private set; }

    [DataMember(Order = 2)]
    public string UserName { get; private set; }

    [DataMember(Order = 3)]
    public EMail EMail { get; private set; }

    [DataMember(Order = 4)]
    public string Password { get; private set; }


    public CreateUserAccountCommand()
    {
    }


    public CreateUserAccountCommand(UserAccountId id, string userName, EMail email, string password)
    {
      Condition.Requires(id, "id").IsNotNull();
      Condition.Requires(userName, "userName").IsNotNullOrEmpty();
      Condition.Requires(email, "email").IsNotNull();

      Id = id;
      UserName = userName;
      EMail = email;
      Password = password;
    }
  }
}
