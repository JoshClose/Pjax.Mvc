using AngleSharp.Extensions;
using AngleSharp.Parser.Html;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pjax.MvcCore
{
	/// <summary>
	/// Enabled handling of MoOx/pjax requests.
	/// </summary>
    public class PjaxMiddleware
    {
		private readonly RequestDelegate next;
		private readonly PjaxOptions options;

		/// <summary>
		/// Initializes the middleware.
		/// </summary>
		/// <param name="next">Delegate for the middleware to continue.</param>
		/// <param name="options">Options for the middleware.</param>
		public PjaxMiddleware(RequestDelegate next, IOptions<PjaxOptions> options)
		{
			this.next = next;
			this.options = options.Value;
		}

		/// <summary>
		/// Invoke the middleware.
		/// </summary>
		/// <param name="context">HTTP context.</param>
		public async Task InvokeAsync(HttpContext context)
		{
			// Intercept the content written to the body.
			var originalBodyStream = context.Response.Body;
			var captureBodyStream = new MemoryStream();
			context.Response.Body = captureBodyStream;

			// Run the MVC middleware.
			await next(context);

			// Get the body.
			captureBodyStream.Position = 0;
			var reader = new StreamReader(captureBodyStream);
			var body = await reader.ReadToEndAsync();
			using (captureBodyStream) { }

			// Update body.
			if (IsPjaxRequest(context))
			{
				body = await ReWriteBodyAsync(context, body);
			}

			// Write new body;
			using (var newBodyStream = new MemoryStream())
			using (var writer = new StreamWriter(newBodyStream))
			{
				writer.Write(body);
				writer.Flush();
				newBodyStream.Position = 0;
				await newBodyStream.CopyToAsync(originalBodyStream);
			}
		}

		private bool IsPjaxRequest(HttpContext context)
		{
			bool.TryParse(context.Request.Headers[options.PjaxHeader].FirstOrDefault(), out bool isPjaxRequest);

			return isPjaxRequest;
		}

		private async Task<string> ReWriteBodyAsync(HttpContext context, string body)
		{
			var selectorsJson = context.Request.Headers[options.PjaxSelectorsHeader].FirstOrDefault();
			if (selectorsJson == null)
			{
				return body;
			}

			var selectors = JsonConvert.DeserializeObject<string[]>(selectorsJson);

			var parser = new HtmlParser();
			var document = await parser.ParseAsync(body);

			var pjaxSections = document.QuerySelectorAll(string.Join(",", selectors));
			using (var writer = new StringWriter())
			{
				foreach (var section in pjaxSections)
				{
					section.ToHtml(writer);
				}

				return writer.ToString();
			}
		}
    }
}
