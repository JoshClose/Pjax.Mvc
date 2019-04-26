# Pjax.Mvc

Pjax.Mvc is a library to integrate ASP.NET MVC with a client side PJAX library. MVC Core integrates with 
[MoOx/pjax](https://github.com/MoOx/pjax) which doesn't require jQuery, and MVC 5/4/3 integrates with 
[jQuery.pjax](https://github.com/defunkt/jquery-pjax). **Note: This library is only the server side implementation. MoOx/Pjax or jquery-pjax are still required for this to work.**

## ASP.NET MVC Core

The MVC Core integration is implemented as middleware. The middleware will take the page generated from
Razor and create a new page that only contains the sections that the PJAX request selectors asked for.
The middleware will only run if the request is a PJAX request.

### Installation

To install Pjax.Mvc, run the following command in the [Package Manager Console](http://docs.nuget.org/docs/start-here/using-the-package-manager-console).

```
PM> Install-Package Pjax.MvcCore
```

### Setup

Register the middleware in `Configure` of your `Startup.cs` file.

```cs
public void Configure(IApplicationBuilder app, IHostingEnvironment env)
{
	if (env.IsDevelopment())
	{
		app.UseDeveloperExceptionPage();
	}
	else
	{
		app.UseExceptionHandler("/Home/Error");
		app.UseHsts();
	}

	app.UseHttpsRedirection();
	app.UseStaticFiles();
	app.UseCookiePolicy();

	// Register Pjax.Mvc
	app.UsePjax();

	app.UseMvc(routes =>
	{
		routes.MapRoute(
			name: "default",
			template: "{controller=Home}/{action=Index}/{id?}");
	});

}
```

### Configuration

You shouldn't ever need to use this, but you can change the name of the headers that Pjax.Mvc is looking for.

Set options in the `ConfigureServices` method of your `Startup.cs` file.

```cs
public void ConfigureServices(IServiceCollection services)
{
	services.Configure<CookiePolicyOptions>(options =>
	{
		// This lambda determines whether user consent for non-essential cookies is needed for a given request.
		options.CheckConsentNeeded = context => true;
		options.MinimumSameSitePolicy = SameSiteMode.None;
	});

	// Configure Pjax.MVC.
	services.Configure<PjaxOptions>(options =>
	{
		options.PjaxHeader = "X-PJAX";
	});

	services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
}
```

## ASP.NET MVC 5/4/3

### Installation

To install Pjax.Mvc, run the following command in the [Package Manager Console](http://docs.nuget.org/docs/start-here/using-the-package-manager-console).

MVC 5
```
PM> Install-Package Pjax.Mvc5
```

MVC 4
```
PM> Install-Package Pjax.Mvc4
```

MVC 3
```
PM> Install-Package Pjax.Mvc3
```

### Setup

After the install is complete, you will need to make a few changes to get it working.

#### Layout

When returning from a pjax request we only want the body returned and not the template. Add this at the beginning of your layout.

```aspx-cs
@if( ViewBag.IsPjaxRequest ?? false )
{
	<title>@ViewBag.Title</title>
	@RenderBody()
	return;
}
<!DOCTYPE html>
...
```

#### Controller

In your controller, add the `PjaxAttribute` to the class or action method. This will setup your controller to receive and send pjax. Your controller must also implement `IPjax`.

```c#
[Pjax]
public abstract class ControllerBase : Controller, IPjax
{
	public bool IsPjaxRequest { get; set; }
	public string PjaxVersion { get; set; }
}
```

Your site is now ready to receive and send pjax calls.

### API

#### Controller Extensions

##### PjaxFullLoad

If you need a response to do a full page load instead of a pjax load, call `PjaxFullLoad` in your action.

One scenario where this is useful is if you need to change layout content. e.g. When a user logs in or out.

```c#
public ActionResult Logout()
{
	WebSecurity.Logout();

	this.PjaxFullLoad();

	return Redirect( "~" );
}
```

This will cause the following page to be a full page load, so any user information that was shown on the screen in the layout can be hidden.

### License

Microsoft Public License (MS-PL)

http://opensource.org/licenses/MS-PL

