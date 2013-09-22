using System;
using System.Collections.Generic;
using WebMatrix.WebData;
using Xyperico.Base;
using Xyperico.Base.Exceptions;
using System.Web.Security;
using CuttingEdge.Conditions;
using log4net;


namespace Xyperico.Authentication.Web
{
  public class SimpleMembershipProvider : ExtendedMembershipProvider
  {
    static ILog Logger = LogManager.GetLogger(typeof(SimpleMembershipProvider));


    #region Dependencies

    public LazyObjectResolver<IUserRepository> UserRepository;

    public LazyObjectResolver<IUserNameValidator> UserNameValidator;

    #endregion


    #region Implemented

    public override bool ValidateUser(string username, string password)
    {
      try
      {
        User u = UserRepository.Value.GetByUserName(username);
        return u.PasswordMatches(password);
      }
      catch (MissingResourceException)
      {
        return false;
      }
    }


    public override string CreateUserAndAccount(string userName, string password, bool requireConfirmation, IDictionary<string, object> values)
    {
      try
      {
        Condition.Requires(values, "values").IsNotNull();
        string email = values["EMail"] as string;
        Condition.Requires(email, "values[EMail]").IsNotNullOrEmpty();
        PasswordPolicy passwordPolicy = Xyperico.Authentication.Configuration.Settings.GetPasswordPolicy();
        User user = new User(userName, password, email, UserNameValidator.Value, passwordPolicy);
        UserRepository.Value.Add(user);
      }
      catch (DuplicateKeyException ex)
      {
        if (ex.Key == "UserName")
          throw new MembershipCreateUserException(MembershipCreateStatus.DuplicateUserName);
        else
          throw new MembershipCreateUserException(MembershipCreateStatus.DuplicateEmail);
      }
      return null; // Return what? "A token that can be sent to the user to confirm the user account."
    }


    public override int GetUserIdFromOAuth(string provider, string providerUserId)
    {
      try
      {
        User user = UserRepository.Value.GetByExternalLogin(provider, providerUserId);
        return user.UserId;
      }
      catch (MissingResourceException)
      {
        return -1;
      }
    }


    public override string GetUserNameFromId(int userId)
    {
      try
      {
        User user = UserRepository.Value.GetByUserId(userId);
        return user.UserName;
      }
      catch (MissingResourceException)
      {
        return null;
      }
    }


    public override void CreateOrUpdateOAuthAccount(string provider, string providerUserId, string userName)
    {
      try
      {
        Logger.DebugFormat("CreateOrUpdateOAuthAccount provider = {0}, providerUserId = {1}, userName = {2}", provider, providerUserId, userName);
        try
        {
          User user = UserRepository.Value.GetByUserName(userName);
          user.AddExternalLogin(provider, providerUserId);
          UserRepository.Value.Update(user);
        }
        catch (MissingResourceException)
        {
          // User did not exists - create
          User user = new User(userName, null, null, UserNameValidator.Value, null);
          user.AddExternalLogin(provider, providerUserId);
          UserRepository.Value.Add(user);
        }
      }
      catch (DuplicateKeyException ex)
      {
        Logger.Debug(ex);
        if (ex.Key == "UserName")
          throw new MembershipCreateUserException(MembershipCreateStatus.DuplicateUserName);
        else if (ex.Key == "EMail")
          throw new MembershipCreateUserException(MembershipCreateStatus.DuplicateEmail);
        else if (ex.Key == "UserId")
          throw new MembershipCreateUserException(MembershipCreateStatus.DuplicateProviderUserKey);
      }
    }

    #endregion

    public override bool ConfirmAccount(string accountConfirmationToken)
    {
      throw new NotImplementedException();
    }

    public override bool ConfirmAccount(string userName, string accountConfirmationToken)
    {
      throw new NotImplementedException();
    }

    public override string CreateAccount(string userName, string password, bool requireConfirmationToken)
    {
      throw new NotImplementedException();
    }

    public override bool DeleteAccount(string userName)
    {
      throw new NotImplementedException();
    }

    public override string GeneratePasswordResetToken(string userName, int tokenExpirationInMinutesFromNow)
    {
      throw new NotImplementedException();
    }

    public override ICollection<WebMatrix.WebData.OAuthAccountData> GetAccountsForUser(string userName)
    {
      throw new NotImplementedException();
    }

    public override DateTime GetCreateDate(string userName)
    {
      throw new NotImplementedException();
    }

    public override DateTime GetLastPasswordFailureDate(string userName)
    {
      throw new NotImplementedException();
    }

    public override DateTime GetPasswordChangedDate(string userName)
    {
      throw new NotImplementedException();
    }

    public override int GetPasswordFailuresSinceLastSuccess(string userName)
    {
      throw new NotImplementedException();
    }

    public override int GetUserIdFromPasswordResetToken(string token)
    {
      throw new NotImplementedException();
    }

    public override bool IsConfirmed(string userName)
    {
      throw new NotImplementedException();
    }

    public override bool ResetPasswordWithToken(string token, string newPassword)
    {
      throw new NotImplementedException();
    }

    public override string ApplicationName
    {
      get
      {
        throw new NotImplementedException();
      }
      set
      {
        throw new NotImplementedException();
      }
    }

    public override bool ChangePassword(string username, string oldPassword, string newPassword)
    {
      throw new NotImplementedException();
    }

    public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion, string newPasswordAnswer)
    {
      throw new NotImplementedException();
    }

    public override System.Web.Security.MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, out System.Web.Security.MembershipCreateStatus status)
    {
      throw new NotImplementedException();
    }

    public override bool DeleteUser(string username, bool deleteAllRelatedData)
    {
      throw new NotImplementedException();
    }

    public override bool EnablePasswordReset
    {
      get { throw new NotImplementedException(); }
    }

    public override bool EnablePasswordRetrieval
    {
      get { throw new NotImplementedException(); }
    }

    public override System.Web.Security.MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
    {
      throw new NotImplementedException();
    }

    public override System.Web.Security.MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
    {
      throw new NotImplementedException();
    }

    public override System.Web.Security.MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
    {
      throw new NotImplementedException();
    }

    public override int GetNumberOfUsersOnline()
    {
      throw new NotImplementedException();
    }

    public override string GetPassword(string username, string answer)
    {
      throw new NotImplementedException();
    }

    public override System.Web.Security.MembershipUser GetUser(string username, bool userIsOnline)
    {
      throw new NotImplementedException();
    }

    public override System.Web.Security.MembershipUser GetUser(object providerUserKey, bool userIsOnline)
    {
      throw new NotImplementedException();
    }

    public override string GetUserNameByEmail(string email)
    {
      throw new NotImplementedException();
    }

    public override int MaxInvalidPasswordAttempts
    {
      get { throw new NotImplementedException(); }
    }

    public override int MinRequiredNonAlphanumericCharacters
    {
      get { throw new NotImplementedException(); }
    }

    public override int MinRequiredPasswordLength
    {
      get { throw new NotImplementedException(); }
    }

    public override int PasswordAttemptWindow
    {
      get { throw new NotImplementedException(); }
    }

    public override System.Web.Security.MembershipPasswordFormat PasswordFormat
    {
      get { throw new NotImplementedException(); }
    }

    public override string PasswordStrengthRegularExpression
    {
      get { throw new NotImplementedException(); }
    }

    public override bool RequiresQuestionAndAnswer
    {
      get { throw new NotImplementedException(); }
    }

    public override bool RequiresUniqueEmail
    {
      get { throw new NotImplementedException(); }
    }

    public override string ResetPassword(string username, string answer)
    {
      throw new NotImplementedException();
    }

    public override bool UnlockUser(string userName)
    {
      throw new NotImplementedException();
    }

    public override void UpdateUser(System.Web.Security.MembershipUser user)
    {
      throw new NotImplementedException();
    }

  }
}
