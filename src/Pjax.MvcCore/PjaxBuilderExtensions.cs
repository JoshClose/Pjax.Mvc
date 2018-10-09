using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pjax.MvcCore
{
	/// <summary>
	/// Extension methods for PJAX middleware.
	/// </summary>
    public static class PjaxBuilderExtensions
    {
		/// <summary>
		/// Adds middleware for handling of MoOx/pjax requests.
		/// </summary>
		/// <param name="app">The <see cref="IApplicationBuilder"/> to extend.</param>
        public static IApplicationBuilder UsePjax(this IApplicationBuilder app)
		{
			return app.UseMiddleware<PjaxMiddleware>();
		}
	}
}
