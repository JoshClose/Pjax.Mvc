using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Pjax.Mvc5;

namespace Example.Controllers
{
	[Pjax]
    public class HomeController : Controller, IPjax
    {
		public bool IsPjaxRequest { get; set; }

		public string PjaxVersion { get; set; }

        public ActionResult Index()
        {
            return View();
        }

	    public ActionResult Page1()
	    {
		    return View();
	    }

		public ActionResult Page2()
		{
			return View();
		}

		public ActionResult Page3()
		{
			return View();
		}
    }
}