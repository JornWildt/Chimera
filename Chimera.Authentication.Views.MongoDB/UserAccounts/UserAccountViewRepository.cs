using Chimera.Authentication.Views.UserAccounts;
using CuttingEdge.Conditions;
using MongoDB.Driver.Builders;


namespace Chimera.Authentication.Views.MongoDB.UserAccounts
{
  public class UserAccountViewRepository : GenericRepository<UserAccountView, string>, IUserAccountViewRepository
  {
    public override void Setup()
    {
      base.Setup();
      Collection.EnsureIndex(
        new IndexKeysBuilder().Ascending("EMailLowercase"),
        IndexOptions.SetSparse(true).SetUnique(true));

      Collection.EnsureIndex(
        new IndexKeysBuilder().Ascending("UserNameLowercase"),
        IndexOptions.SetUnique(true));

      Collection.EnsureIndex(
        new IndexKeysBuilder().Ascending("ExternalLogins.Provider").Ascending("ExternalLogins.ProviderUserId"),
        IndexOptions.SetSparse(true).SetUnique(true));
    }


    protected override string MapDuplicateKeyErrorToKeyName(string error)
    {
      if (error.Contains("$EMailLowercase"))
        return "EMail";
      else if (error.Contains("$UserNameLowercase"))
        return "UserName";
      else if (error.Contains("$ExternalLogins.Provider"))
        return "ExternalLogin";
      else
        return null;
    }


    #region IUserAccountViewRepository Members

    public UserAccountView GetByUserName(string userName)
    {
      Condition.Requires(userName, "userName").IsNotNull();

      return FindSingle(new { UserNameLowercase = userName.ToLower() });
    }


    public UserAccountView GetByEMail(string email)
    {
      Condition.Requires(email, "email").IsNotNull();

      return FindSingle(new { EMailLowercase = email.ToLower() });
    }

    #endregion
  }
}
