using System;
using System.ComponentModel.DataAnnotations;
using Chimera.Authentication.Shared.UserAccounts;
using Chimera.Authentication.Shared;


namespace Chimera.Authentication.Views
{
  [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
  public class PasswordValidationAttribute : ValidationAttribute
  {
    public override bool IsValid(object value)
    {
      string pwd = value as string;
      if (pwd == null)
        return false;

      PasswordPolicy policy = Configuration.Settings.GetPasswordPolicy();
      return policy.IsValid(pwd);
    }
  }
}
