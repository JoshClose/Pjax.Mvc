using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pjax.MvcCore
{
	/// <summary>
	/// Options for the PJAX middleware
	/// </summary>
    public class PjaxOptions
    {
		/// <summary>
		/// Gets or sets the name of the requested with header.
		/// </summary>
		public string RequestedWithHeader { get; set; } = "X-Requested-With";

		/// <summary>
		/// Gets or sets the name of the PJAX header.
		/// </summary>
		public string PjaxHeader { get; set; } = "X-PJAX";

		/// <summary>
		/// Gets or sets the name of the PJAX selectors header.
		/// </summary>
		public string PjaxSelectorsHeader { get; set; } = "X-PJAX-Selectors";

		/// <summary>
		/// Gets or sets the name of the PJAX redirect url header.
		/// </summary>
		public string PjaxRedirectUrlHeader { get; set; } = "X-PJAX-URL";
	}
}
