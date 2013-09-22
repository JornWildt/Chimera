using System;
using System.Linq.Expressions;
using System.Web.Mvc;
using Chimera.Authentication.Shared.UserAccounts;



namespace Chimera.Authentication.Web
{
  // From http://www.c-sharpcorner.com/uploadfile/f9935e/password-policystrength-asp-net-mvc-validator
  public static class PasswordStrengthHtmlHelper
  {
    public static string JSBlock(this HtmlHelper html, string script)
    {
      return @"<script type=""text/javascript"">" + script + "</script>";
    }

    public static MvcHtmlString PasswordPolicyValidatorFor<TModel, TProperty>(this HtmlHelper<TModel> Html,
                        System.Linq.Expressions.Expression<Func<TModel, TProperty>> Expression,
                        PasswordPolicy Policy, string UserNameControlID, PasswordStrength Strength,
                        string ScriptPath, string ErrorMessage
                        )
    {
      MemberExpression memberExpression = (MemberExpression)Expression.Body;
      string PasswordControlID = memberExpression.Member.Name;

      string ValidationExpression = string.Empty;

      if (Policy != null)
      {
        ValidationExpression = Policy.GetExpression();
      }

      string StrengthScript = string.Empty;

      var labelBuilder = new TagBuilder("label");
      labelBuilder.GenerateId("LabelValidatorContainer" + PasswordControlID);
      if (Policy != null)
      {
        labelBuilder.Attributes.Add("validationexpression", ValidationExpression);
      }
      labelBuilder.Attributes.Add("name", "StrengthMessage");
      labelBuilder.Attributes.Add("class", "");

      string StrengthSpecs = string.Empty;

      if (Strength != null)
      {
        StrengthSpecs = Strength.GetStrengthSpecificationsArrays(PasswordControlID);
        StrengthScript = @"function PasswordPolicyValidatorValidate" + PasswordControlID + "(){ var val = document.getElementById('" + PasswordControlID + "'); var arr = GetStrengthArray" + PasswordControlID + "(); var arrCategories = GetCategoriesArray" + PasswordControlID + "(); var strength = GetStrength(arr, arrCategories, '" + PasswordControlID + "',GetUnicodeCharSetRangesArray" + PasswordControlID + "()); ShowStrengthColour(strength, arrCategories, GetColoursArray" + PasswordControlID + "(), 'LabelValidatorContainer" + PasswordControlID + "'); return PasswordPolicyValidatorValidate('" + PasswordControlID + "','LabelValidatorContainer" + PasswordControlID + "','" + (String.IsNullOrEmpty(UserNameControlID) ? "" : UserNameControlID) + "'); }";
      }
      else
        StrengthScript = @"function PasswordPolicyValidatorValidate" + PasswordControlID + "(){ return PasswordPolicyValidatorValidate('" + PasswordControlID + "','LabelValidatorContainer" + PasswordControlID + "','" + (String.IsNullOrEmpty(UserNameControlID) ? "" : UserNameControlID) + "'); }";

      string Include = (ScriptPath != null ? @"<script type=""text/javascript"" src=""" + ScriptPath + @"""></script>" : "");

      string OnInit = @"
                  function OnInit(PasswordControlID) {
                      $.ajaxSetup({ cache: false });
                        
                      $(document).ready(function() { 
                          jQuery.validator.addMethod(""passwordPolicyValidate" + PasswordControlID + @"""" + @", function(value, element) { 
                                      return PasswordPolicyValidatorValidate" + PasswordControlID + @"(); 
                                  }, '" + ErrorMessage + @"'); 
                      });                        
                  }
              ";

      return MvcHtmlString.Create(Include +
              Html.JSBlock(OnInit) +
              Html.JSBlock("$(OnInit('" + PasswordControlID + "'));") +
              Html.JSBlock(StrengthSpecs) +
              Html.JSBlock(StrengthScript) +
              labelBuilder.ToString(TagRenderMode.Normal));
    }
  }
}

/*

 *                       $(""#" + PasswordControlID + @""").ready(function() {
                          $(""#" + PasswordControlID + @""").change(function() {
                              $('#registerForm').validate({
                                  rules: {
                                      " + PasswordControlID + @": { passwordPolicyValidate" + PasswordControlID + @": true }
                                  }
                              });
                          });
                          $(""#" + PasswordControlID + @""").focus(function() {
                              $('#registerForm').validate({
                                  rules: {
                                      " + PasswordControlID + @": { passwordPolicyValidate" + PasswordControlID + @": true }
                                  }
                              });
                          });
                      });
                       
                      $(document).submit(function() {
                          $('#registerForm').validate({
                              rules: {
                                  " + PasswordControlID + @": { passwordPolicyValidate" + PasswordControlID + @": true }
                              }
                          });
                      });       


*/