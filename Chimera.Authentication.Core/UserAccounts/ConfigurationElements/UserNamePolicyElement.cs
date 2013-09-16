using System.Xml.Serialization;


namespace Chimera.Authentication.Core.UserAccounts.ConfigurationElements
{
  public class UserNamePolicyElement
  {
    [XmlAttribute]
    public string ValidUserNamePattern { get; set; }

    [XmlAttribute]
    public string InvalidUserNameFile { get; set; }

    [XmlAttribute]
    public int MinLength { get; set; }

    [XmlAttribute]
    public int MaxLength { get; set; }
  }
}
