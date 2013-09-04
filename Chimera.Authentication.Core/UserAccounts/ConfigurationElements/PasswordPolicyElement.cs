using System.Configuration;


namespace Chimera.Authentication.Core.UserAccounts.ConfigurationElements
{
  public class PasswordPolicyElement : ConfigurationElement
  {
    public override bool IsReadOnly() { return false; }


    [ConfigurationProperty("MinPasswordLength", DefaultValue = -1)]
    public int MinPasswordLength
    {
      get { return (int)this["MinPasswordLength"]; }
      set { this["MinPasswordLength"] = value; }
    }


    [ConfigurationProperty("MinNoOfUpperCaseChars", DefaultValue = -1)]
    public int MinNoOfUpperCaseChars
    {
      get { return (int)this["MinNoOfUpperCaseChars"]; }
      set { this["MinNoOfUpperCaseChars"] = value; }
    }


    [ConfigurationProperty("MinNoOfLowerCaseChars", DefaultValue = -1)]
    public int MinNoOfLowerCaseChars
    {
      get { return (int)this["MinNoOfLowerCaseChars"]; }
      set { this["MinNoOfLowerCaseChars"] = value; }
    }


    [ConfigurationProperty("MinNoOfNumbers", DefaultValue = -1)]
    public int MinNoOfNumbers
    {
      get { return (int)this["MinNoOfNumbers"]; }
      set { this["MinNoOfNumbers"] = value; }
    }


    [ConfigurationProperty("MaxNoOfAllowedCharacterRepetitions", DefaultValue = -1)]
    public int MaxNoOfAllowedCharacterRepetitions
    {
      get { return (int)this["MaxNoOfAllowedCharacterRepetitions"]; }
      set { this["MaxNoOfAllowedCharacterRepetitions"] = value; }
    }
  }
}
