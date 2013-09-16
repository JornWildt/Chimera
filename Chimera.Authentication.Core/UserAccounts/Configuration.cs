using System.Configuration;
using Chimera.Authentication.Core.UserAccounts.ConfigurationElements;
using Xyperico.Base;
using System.Xml.Serialization;


namespace Chimera.Authentication.Core.UserAccounts
{
  [Module("UserAccounts")]
  [XmlRoot("Configuration")]
  public class XmlConfiguration : XmlFileConfiguration<XmlConfiguration>
  {
    [XmlAttribute]
    public string PasswordHashAlgorithm { get; set; }

    public PasswordPolicyElement PasswordPolicy { get; set; }

    public UserNamePolicyElement UserNamePolicy { get; set; }

    public XmlConfiguration()
    {
      PasswordPolicy = new PasswordPolicyElement();
      UserNamePolicy = new UserNamePolicyElement();
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
