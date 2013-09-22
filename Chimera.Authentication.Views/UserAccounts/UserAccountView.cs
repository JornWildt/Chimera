using Chimera.Authentication.Contract.UserAccounts.Events;
using Xyperico.Base;
using Xyperico.Base.CommonDomainTypes;


namespace Chimera.Authentication.Views.UserAccounts
{
  public class UserAccountView : IHaveId<string>
  {
    public string Id { get; private set; }

    public int MembershipProviderId { get; private set; }

    public string UserName { get; private set; }

    public string UserNameLowercase { get; private set; }

    public string EMail { get; private set; }

    public string EMailLowercase { get; private set; }

    
    public UserAccountView(UserAccountCreatedEvent e, int membershipProviderId)
    {
      Id = e.Id.Literal;
      MembershipProviderId = membershipProviderId;
      UserName = e.UserName;
      UserNameLowercase = UserName.ToLower();
      EMail = e.EMail.ToString();
      EMailLowercase = EMail.ToLower();
    }
  }
}
