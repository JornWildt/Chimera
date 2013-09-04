namespace Chimera.Authentication.Core.UserAccounts
{
  public interface IUserNameValidator
  {
    bool IsValidUserName(string userName);
  }
}
