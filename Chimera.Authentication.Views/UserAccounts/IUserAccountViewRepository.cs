namespace Chimera.Authentication.Views.UserAccounts
{
  public interface IUserAccountViewRepository
  {
    void Add(UserAccountView user);
    UserAccountView Get(string id);
    UserAccountView GetByUserName(string userName);
    UserAccountView GetByEMail(string userName);
    void Remove(string id);
  }
}
