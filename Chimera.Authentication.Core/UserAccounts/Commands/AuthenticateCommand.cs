using System.Runtime.Serialization;
using CuttingEdge.Conditions;
using Xyperico.Agres;


namespace Chimera.Authentication.Core.UserAccounts.Commands
{
  [DataContract]
  public class AuthenticateCommand : ICommand<UserAccountId>
  {
    [DataMember(Order = 1)]
    public UserAccountId Id { get; private set; }
    
    [DataMember(Order = 2)]
    public string UserName { get; private set; }

    [DataMember(Order = 3)]
    public string Password { get; private set; }

    
    public AuthenticateCommand() { }


    public AuthenticateCommand(UserAccountId id, string userName, string password)
    {
      Condition.Requires(id, "id").IsNotNull();
      Condition.Requires(userName, "userName").IsNotNullOrEmpty();
      Condition.Requires(password, "password").IsNotNull();

      Id = id;
      UserName = userName;
      Password = password;
    }
  }
}
