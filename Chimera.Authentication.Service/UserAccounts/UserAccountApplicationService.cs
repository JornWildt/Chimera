using Chimera.Authentication.Contract.UserAccounts;
using Chimera.Authentication.Contract.UserAccounts.Commands;
using Chimera.Authentication.Core.UserAccounts;
using CuttingEdge.Conditions;
using Xyperico.Agres.EventStore;
using Xyperico.Agres.MessageBus;


namespace Chimera.Authentication.Service.UserAccounts
{
  public class UserAccountApplicationService : GenericApplicationService<UserAccount, UserAccountState, UserAccountId>,
    IHandleMessage<CreateUserAccountCommand>
  {
    #region Dependencies

    public IUserNameValidator UserNameValidator { get; set; }

    #endregion

    
    public UserAccountApplicationService(IEventStore eventStore, IUserNameValidator userNameValidator)
      : base(eventStore)
    {
      Condition.Requires(userNameValidator, "userNameValidator").IsNotNull();

      UserNameValidator = userNameValidator;
    }


    public void Handle(CreateUserAccountCommand cmd)
    {
      Update(cmd, user => user.Create(cmd, UserNameValidator, XmlConfiguration.Settings.GetPasswordPolicy()));
    }
  }
}
