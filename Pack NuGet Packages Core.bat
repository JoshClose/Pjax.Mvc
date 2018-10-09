del /S /Q NuGet\*.*

dotnet pack src\Pjax.MvcCore -c release -o ..\..\NuGet\Pack
dotnet pack src\Pjax.MvcCore -c release -o ..\..\NuGet\Symbols --include-symbols

pause
