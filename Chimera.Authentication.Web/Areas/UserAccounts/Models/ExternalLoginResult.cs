﻿using System.Web.Mvc;
using Microsoft.Web.WebPages.OAuth;


namespace Xyperico.Authentication.Web.Areas.Account.Models
{
  internal class ExternalLoginResult : ActionResult
  {
    public ExternalLoginResult(string provider, string returnUrl)
    {
      Provider = provider;
      ReturnUrl = returnUrl;
    }

    public string Provider { get; private set; }
    public string ReturnUrl { get; private set; }

    public override void ExecuteResult(ControllerContext context)
    {
      OAuthWebSecurity.RequestAuthentication(Provider, ReturnUrl);
    }
  }
}