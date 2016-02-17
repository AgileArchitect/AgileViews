#tool "xunit.runner.console"

var target = Argument("target", "Build");
var configuration = Argument("configuration", "Release");

var solution = "./Source/AgileViews.sln";
var project =  "./Source/AgileViews/AgileViews.csproj";

Task("Build")
	.IsDependentOn("Restore-NuGet-Packages")
    .Does(() =>
{
    MSBuild(solution, s => s.SetConfiguration(configuration).SetVerbosity(Verbosity.Quiet));
});

Task("Restore-NuGet-Packages")
    .Does(() =>
{
    NuGetRestore(solution);
});

Task("Test")
 .IsDependentOn("Build")
 .Does(() => {
	XUnit2("Source/AgileViews.Test/bin/Release/*.Test.dll");
 });
 
Task("Package")
	.IsDependentOn("Build")
	.Does(() => {
		var spec = StartProcess("tools/nuget.exe", "spec ");
	}
);

Task("Default")
  .IsDependentOn("Build");

RunTarget(target);
