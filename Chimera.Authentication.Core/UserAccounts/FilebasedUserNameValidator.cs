using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using Xyperico.Base.IO;


namespace Chimera.Authentication.Core.UserAccounts
{
  public class FilebasedUserNameValidator : IUserNameValidator
  {
    private static readonly Regex ValidUserNamePattern;

    private HashSet<string> InvalidUserNames = new HashSet<string>();
    private DateTime? InvalidUserNamesLastRead;

    // Primarily used for internal testing
    public static int FileReloadTimes { get; set; }


    static FilebasedUserNameValidator()
    {
      if (!string.IsNullOrEmpty(Configuration.Settings.UserNamePolicy.ValidUserNamePattern))
        ValidUserNamePattern = new Regex(Configuration.Settings.UserNamePolicy.ValidUserNamePattern);
      else
        ValidUserNamePattern = new Regex("^[a-zA-Z0-9]+[a-zA-Z0-9-_.]*$");
    }


    public bool IsValidUserName(string userName)
    {
      if (userName == null)
        return false;
      if (userName.Length < Configuration.Settings.UserNamePolicy.MinLength || userName.Length > Configuration.Settings.UserNamePolicy.MaxLength)
        return false;

      if (!ValidUserNamePattern.IsMatch(userName))
        return false;

      // These are system names potentially used in URLs
      if (userName == "app" || userName == "api" || userName == "css")
        return false;

      ReadInvalidUserNames();
      if (InvalidUserNames.Contains(userName))
        return false;

      return true;
    }


    private void ReadInvalidUserNames()
    {
      string filename = Configuration.Settings.UserNamePolicy.InvalidUserNameFile;
      if (string.IsNullOrEmpty(filename))
        return;
      filename = FileUtils.MapRelPathToBaseDir(filename);

      FileInfo inf = new FileInfo(filename);
      if (InvalidUserNamesLastRead != null && inf.LastWriteTime <= InvalidUserNamesLastRead.Value)
        return;

      InvalidUserNames = new HashSet<string>();
      FileReloadTimes++;
      using (StreamReader r = new StreamReader(filename))
      {
        string line;
        while ((line = r.ReadLine()) != null)
        {
          line = line.Trim();
          if (line != string.Empty && !line.StartsWith("#"))
          {
            InvalidUserNames.Add(line);
          }
        }
      }

      InvalidUserNamesLastRead = inf.LastWriteTime;
    }
  }
}
