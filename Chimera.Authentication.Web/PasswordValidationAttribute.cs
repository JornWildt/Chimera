using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Chimera.Authentication.Shared.UserAccounts;


namespace Chimera.Authentication.Web
{
  [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
  public class PasswordValidationAttribute : Chimera.Authentication.Views.PasswordValidationAttribute, IClientValidatable
  {
    public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
    {
      PasswordPolicy policy = Chimera.Authentication.Shared.Configuration.Settings.GetPasswordPolicy();

      var rule = new ModelClientValidationRule();
      rule.ValidationType = "passwordpolicy";
      rule.ValidationParameters["policyexpr"] = policy.GetExpression();
      rule.ErrorMessage = policy.GetDescription(metadata.DisplayName);
      yield return rule;
    }
  }
}