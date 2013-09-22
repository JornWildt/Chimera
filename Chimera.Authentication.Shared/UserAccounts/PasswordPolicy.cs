using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Xml.Serialization;


namespace Chimera.Authentication.Shared.UserAccounts
{
  // From http://www.c-sharpcorner.com/uploadfile/f9935e/password-policystrength-asp-net-mvc-validator
  public class PasswordPolicy
  {
    public string UnicodeCharSetRanges { get; set; }
    public int MinPasswordLength { get; set; }
    public int MinNoOfUpperCaseChars { get; set; }
    public int MinNoOfLowerCaseChars { get; set; }
    public int MinNoOfUniCaseChars { get; set; }
    public int MinNoOfNumbers { get; set; }
    public int MinNoOfSymbols { get; set; }
    public int MaxNoOfAllowedCharacterRepetitions { get; set; }
    public string DisallowedChars { get; set; }


    public PasswordPolicy()
    {
      UnicodeCharSetRanges = string.Empty;
      DisallowedChars = string.Empty;
      MinPasswordLength = -1;
      MinNoOfUpperCaseChars = -1;
      MinNoOfLowerCaseChars = -1;
      MinNoOfUniCaseChars = -1;
      MinNoOfNumbers = -1;
      MinNoOfSymbols = -1;
      MaxNoOfAllowedCharacterRepetitions = -1;
    }


    public string GetExpression()
    {
      if (DisallowedChars.Length > 0)
      {
        DisallowedChars = DisallowedChars.Replace(@"\", @"\\");
      }

      string Unicase = String.IsNullOrEmpty(UnicodeCharSetRanges) ? "A-Z" : UnicodeCharSetRanges.Split(',')[0].Trim();
      string Lowercase = String.IsNullOrEmpty(UnicodeCharSetRanges) ? "a-z" : (UnicodeCharSetRanges.Split(',').Length >= 2 ? UnicodeCharSetRanges.Split(',')[1] : "a-z");

      return @"^"
          + (MaxNoOfAllowedCharacterRepetitions > -1 ? @"(?=^((.)(?!(.*?\2){" + (MaxNoOfAllowedCharacterRepetitions).ToString() + @",}))+$)" : "")
          + (MinPasswordLength > -1 ? "(?=.{" + MinPasswordLength.ToString() + @",})" : "")
          + (MinNoOfNumbers > -1 ? @"(?=([^0-9]*?\d){" + MinNoOfNumbers.ToString() + ",})" : "")
          + (MinNoOfUniCaseChars > -1 ? "(?=([^" + Unicase + @"]*?[" + Unicase + @"]){" + MinNoOfUniCaseChars.ToString() + @",})" : "")
          + (MinNoOfLowerCaseChars > -1 ? "(?=([^" + Lowercase + @"]*?[" + Lowercase + @"]){" + MinNoOfLowerCaseChars.ToString() + ",})" : "")
          + (MinNoOfUpperCaseChars > -1 ? "(?=([^" + Unicase + @"]*?[" + Unicase + @"]){" + MinNoOfUpperCaseChars.ToString() + @",})" : "")
          + (MinNoOfSymbols > -1 ? "(?=([" + Unicase + Lowercase + @"0-9]*?[^" + Unicase + Lowercase + @"]){" + MinNoOfSymbols.ToString() + ",})" : "")
          + (DisallowedChars.Length > 0 ? @"(?=[^" + DisallowedChars + @"]+$)" : "")
          + @".*$";
    }


    public bool IsValid(string Password)
    {
      Console.WriteLine(GetExpression());
      return Regex.Match(Password, GetExpression()).Success;
    }


    public string GetDescription(string fieldName)
    {
      string lengthMsg = (MinPasswordLength > -1 ? string.Format(_.Auth.PwdLengthMsg_p1, MinPasswordLength) : null);
      string lowerCaseMsg = (MinNoOfLowerCaseChars > -1 ? string.Format(_.Auth.PwdLowerCaseMsg_p1, MinNoOfLowerCaseChars) : null);
      string upperCaseMsg = (MinNoOfUpperCaseChars > -1 ? string.Format(_.Auth.PwdUpperCaseMsg_p1, MinNoOfUpperCaseChars) : null);
      string numberMsg = (MinNoOfNumbers > -1 ? string.Format(_.Auth.PwdNumberMsg_p1, MinNoOfNumbers) : null);
      string repeatMsg = (MaxNoOfAllowedCharacterRepetitions > -1 ? string.Format(_.Auth.PwdRepetitionsMsg_p1, MaxNoOfAllowedCharacterRepetitions) : null);

      string[] messages = new string[] { lengthMsg, lowerCaseMsg, upperCaseMsg, numberMsg, repeatMsg };
      List<string> nonEmptyMessages = messages.Where(m => m != null).ToList();
      if (nonEmptyMessages.Count == 0)
        return null;

      string message = nonEmptyMessages.Aggregate((a, b) => a + ", " + b);
      return string.Format(_.Auth.PwdMust_p2, fieldName, message);
    }
  }


  public class PasswordStrength
  {
    public string UnicodeCharSetRanges { get; set; }
    public string StrengthCategories { get; set; }
    public string StrengthColours { get; set; }
    public string MinPasswordLengthStrength { get; set; }
    public string MinNoOfUniCaseCharsStrength { get; set; }
    public string MinNoOfUpperCaseCharsStrength { get; set; }
    public string MinNoOfLowerCaseCharsStrength { get; set; }
    public string MinNoOfNumbersStrength { get; set; }
    public string MinNoOfSymbolsStrength { get; set; }
    public string MaxNoOfAllowedCharacterRepetitionsStrength { get; set; }

    public string GetStrengthSpecificationsArrays(string ControlID)
    {
      PropertyInfo[] propertyInfos;
      propertyInfos = typeof(PasswordStrength).GetProperties();

      List<PropertyInfo> lstPropertyInfos = propertyInfos.ToList().Where(pi => pi.Name.EndsWith("Strength") && pi.GetValue(this, null).ToString().Trim().Length > 0).ToList();

      int propertyCount = lstPropertyInfos.Count;

      string js = "function GetStrengthArray" + ControlID + "(){var strengthArray = new Array(" + propertyCount + ");" +
                     "for (i=0; i <" + propertyCount + "; i++){strengthArray[i]=new Array(" + StrengthCategories.Split(',').Count() + ");}";

      int i = 0;

      for (i = 0; i < propertyCount; i++)
      {
        PropertyInfo pi = lstPropertyInfos[i];

        js = js + @"strengthArray[" + i + "][1]='" + pi.Name.Substring(0, pi.Name.IndexOf("Strength")) + "';";

        for (int j = 2; j < StrengthCategories.Split(',').Count() + 2; j++)
        {
          js = js + @"strengthArray[" + i.ToString() + "][" + j.ToString() + "]='" + pi.GetValue(this, null).ToString().Split(',')[j - 2].Trim() + "';";
        }
      }

      js = js + " return strengthArray;}";

      int NoOfCategories = StrengthCategories.Split(',').Count();

      js = js + "function GetCategoriesArray" + ControlID + "() { var arr = new Array(" + NoOfCategories + ");";
      for (i = 0; i < NoOfCategories; i++)
      {
        js = js + @"arr[" + i.ToString() + "]='" + StrengthCategories.Split(',')[i].Trim() + "';";
      }

      js = js + @" return arr; }";

      int NoOfColours = StrengthColours.Split(',').Count();

      js = js + "function GetColoursArray" + ControlID + "() { var arr = new Array(" + NoOfColours + ");";
      for (i = 0; i < NoOfColours; i++)
      {
        js = js + @"arr[" + i.ToString() + "]='" + StrengthColours.Split(',')[i].Trim() + "';";
      }

      js = js + @" return arr; }";

      int NoOfCases = UnicodeCharSetRanges.Split(',').Count();

      js = js + "function GetUnicodeCharSetRangesArray" + ControlID + "() { var arr = new Array(" + NoOfCases + ");";

      for (i = 0; i < NoOfCases; i++)
      {
        js = js + @"arr[" + i.ToString() + "]='" + UnicodeCharSetRanges.Split(',')[i].Trim() + "';";
      }

      js = js + @" return arr; }";

      return js;
    }
  }
}
