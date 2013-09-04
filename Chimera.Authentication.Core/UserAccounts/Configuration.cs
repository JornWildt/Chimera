using System.Configuration;
using Chimera.Authentication.Core.UserAccounts.ConfigurationElements;
using Xyperico.Base;


namespace Chimera.Authentication.Core.UserAccounts
{
  public class Configuration : ConfigurationSettingsBase<Configuration>
  {
    public override bool IsReadOnly() { return false; }


    [ConfigurationProperty("PasswordHashAlgorithm")]
    public string PasswordHashAlgorithm
    {
      get { return (string)this["PasswordHashAlgorithm"]; }
      set { this["PasswordHashAlgorithm"] = value; }
    }


    [ConfigurationProperty("UserNamePolicy")]
    public UserNamePolicyElement UserNamePolicy
    {
      get { return (UserNamePolicyElement)this["UserNamePolicy"]; }
      set { this["UserNamePolicy"] = value; }
    }


    [ConfigurationProperty("PasswordPolicy")]
    public PasswordPolicyElement PasswordPolicy
    {
      get { return (PasswordPolicyElement)this["PasswordPolicy"]; }
      set { this["PasswordPolicy"] = value; }
    }


    public PasswordPolicy GetPasswordPolicy()
    {
      return new PasswordPolicy
      {
        MinPasswordLength = PasswordPolicy.MinPasswordLength,
        MinNoOfLowerCaseChars = PasswordPolicy.MinNoOfLowerCaseChars,
        MinNoOfUpperCaseChars = PasswordPolicy.MinNoOfUpperCaseChars,
        MinNoOfNumbers = PasswordPolicy.MinNoOfNumbers,
        MaxNoOfAllowedCharacterRepetitions = PasswordPolicy.MaxNoOfAllowedCharacterRepetitions
      };
    }
  }
}
