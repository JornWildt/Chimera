using System.Xml.Serialization;


namespace Chimera.Authentication.Core.UserAccounts.ConfigurationElements
{
  public class PasswordPolicyElement
  {
    [XmlAttribute]
    public int MinPasswordLength { get; set; }

    [XmlAttribute]
    public int MinNoOfUpperCaseChars { get; set; }

    [XmlAttribute]
    public int MinNoOfLowerCaseChars { get; set; }

    [XmlAttribute]
    public int MinNoOfNumbers { get; set; }

    [XmlAttribute]
    public int MaxNoOfAllowedCharacterRepetitions { get; set; }

    
    public PasswordPolicyElement()
    {
      MinPasswordLength = -1;
      MinNoOfUpperCaseChars = -1;
      MinNoOfLowerCaseChars = -1;
      MinNoOfNumbers = -1;
      MaxNoOfAllowedCharacterRepetitions = -1;
    }
  }
}
