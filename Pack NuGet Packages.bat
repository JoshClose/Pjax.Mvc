del /Q NuGet\*.*

.\src\.nuget\NuGet.exe pack .\src\Pjax.Mvc5\Pjax.Mvc5.nuspec -OutputDirectory NuGet
.\src\.nuget\NuGet.exe pack .\src\Pjax.Mvc4\Pjax.Mvc4.nuspec -OutputDirectory NuGet
.\src\.nuget\NuGet.exe pack .\src\Pjax.Mvc3\Pjax.Mvc3.nuspec -OutputDirectory NuGet
