using System;
using System.Runtime.Serialization;
using Xyperico.Agres;


namespace Chimera.Authentication.Core.UserAccounts
{
  [DataContract]
  public class UserAccountId : Identity<Guid>
  {
    public UserAccountId()
      : base(Guid.NewGuid())
    {
    }


    protected override string Prefix
    {
      // Using GUIDs so leave this empty
      get { return ""; }
    }
  }
}
