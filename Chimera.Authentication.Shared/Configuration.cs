using System.Configuration;
using Chimera.Authentication.Shared.UserAccounts;
using Chimera.Authentication.Shared.UserAccounts.ConfigurationElements;
using Xyperico.Base;


namespace Chimera.Authentication.Shared
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


    [ConfigurationProperty("ExternalProviders")]
    public ConfigurationElementCollection<AuthenticationProviderSection> ExternalProviders
    {
      get { return (ConfigurationElementCollection<AuthenticationProviderSection>)this["ExternalProviders"]; }
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
