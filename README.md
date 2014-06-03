# Pjax.Mvc

Pjax.Mvc is a library to integrate ASP.NET MVC with [jQuery.pjax](https://github.com/defunkt/jquery-pjax).

## Installation

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

## Setup

After the install is complete, you will need to make a few changes to get it working.

### Layout

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

### Controller

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

## API

### Controller Extensions

#### PjaxFullLoad

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

## License

Microsoft Public License (MS-PL)

http://opensource.org/licenses/MS-PL

