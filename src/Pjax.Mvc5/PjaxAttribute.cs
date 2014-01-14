// Copyright 2014 Josh Close
// This file is a part of Pjax.Mvc and is licensed under the MS-PL
// See LICENSE.txt for details or visit http://www.opensource.org/licenses/ms-pl.html
using System;
using System.Linq;
using System.Web.Mvc;

namespace Pjax.Mvc5
{
	public class PjaxAttribute : ActionFilterAttribute
	{
		public override void OnActionExecuting( ActionExecutingContext filterContext )
		{
			var pjaxController = filterContext.Controller as IPjax;
			if (pjaxController == null)
			{
				return;
			}

			var pjax = filterContext.HttpContext.Request.Headers[Constants.PjaxHeader];
			bool pjaxBool;
			pjaxController.IsPjaxRequest = bool.TryParse( pjax, out pjaxBool ) && pjaxBool;
			filterContext.Controller.ViewBag.IsPjaxRequest = pjaxBool;

			var version = filterContext.HttpContext.Session[Constants.PjaxVersion] as string ?? Guid.NewGuid().ToString( "N" );
			filterContext.HttpContext.Session[Constants.PjaxVersion] = version;
			filterContext.Controller.ViewBag.PjaxVersion = version;
			pjaxController.PjaxVersion = version;
		}

		public override void OnActionExecuted( ActionExecutedContext filterContext )
		{
			var pjaxController = filterContext.Controller as IPjax;
			if( pjaxController == null || !pjaxController.IsPjaxRequest )
			{
				return;
			}

			if( !filterContext.HttpContext.Response.Headers.AllKeys.Any( k => k.ToUpper() == Constants.PjaxUrl ) )
			{
				var url = filterContext.HttpContext.Request.Url.ToString();
				filterContext.HttpContext.Response.AddHeader( Constants.PjaxUrl, url );
			}

			if( !filterContext.HttpContext.Response.Headers.AllKeys.Any( k => k.ToUpper() == Constants.PjaxVersion ) )
			{
				filterContext.HttpContext.Response.AddHeader( Constants.PjaxVersion, pjaxController.PjaxVersion );
			}
		}
	}
}
