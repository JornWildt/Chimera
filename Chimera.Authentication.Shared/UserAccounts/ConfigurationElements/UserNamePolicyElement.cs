using System.Configuration;


namespace Chimera.Authentication.Shared.UserAccounts.ConfigurationElements
{
  public class UserNamePolicyElement : ConfigurationElement
  {
    public override bool IsReadOnly() { return false; }


    [ConfigurationProperty("ValidUserNamePattern")]
    public string ValidUserNamePattern
    {
      get { return (string)this["ValidUserNamePattern"]; }
      set { this["ValidUserNamePattern"] = value; }
    }


    [ConfigurationProperty("InvalidUserNameFile")]
    public string InvalidUserNameFile
    {
      get { return (string)this["InvalidUserNameFile"]; }
      set { this["InvalidUserNameFile"] = value; }
    }


    [ConfigurationProperty("MinLength")]
    public int MinLength
    {
      get { return (int)this["MinLength"]; }
      set { this["MinLength"] = value; }
    }


    [ConfigurationProperty("MaxLength")]
    public int MaxLength
    {
      get { return (int)this["MaxLength"]; }
      set { this["MaxLength"] = value; }
    }
  }
}
