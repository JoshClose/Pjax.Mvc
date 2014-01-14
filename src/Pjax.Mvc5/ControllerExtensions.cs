// Copyright 2014 Josh Close
// This file is a part of Pjax.Mvc and is licensed under the MS-PL
// See LICENSE.txt for details or visit http://www.opensource.org/licenses/ms-pl.html
using System;
using System.Web.Mvc;

namespace Pjax.Mvc5
{
	public static class ControllerExtensions
	{
		public static void PjaxFullLoad( this ControllerBase controller )
		{
			controller.ControllerContext.HttpContext.Session[Constants.PjaxVersion] = Guid.NewGuid().ToString( "N" );
		}
	}
}
