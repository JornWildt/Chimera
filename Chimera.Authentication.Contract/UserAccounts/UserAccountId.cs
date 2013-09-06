using System;
using System.Runtime.Serialization;
using Xyperico.Agres;


namespace Chimera.Authentication.Contract.UserAccounts
{
  [DataContract]
  public class UserAccountId : Identity<Guid>
  {
    public UserAccountId()
    {
    }


    public UserAccountId(Guid id)
      : base(id)
    {
    }


    protected override string Prefix
    {
      // Using GUIDs so leave this empty
      get { return ""; }
    }
  }
}
