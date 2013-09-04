using System;


namespace Chimera.Authentication.Core.UserAccounts
{
  public class InvalidPasswordException : Exception
  {
    public InvalidPasswordException(string msg)
      : base(msg)
    {
    }
  }
}
