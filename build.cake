#tool "xunit.runner.console"

var target = Argument("target", "Build");
var configuration = Argument("configuration", "Release");

var solution = "./Source/AgileViews.sln";
var project =  "./Source/AgileViews/AgileViews.csproj";
var nuspec =  "./nuspec/AgileViews.nuspec";

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
	.IsDependentOn("Test")
	.Does(() => {
		
		CreateDirectory("nuget");
		
		var nuGetPackSettings   = new NuGetPackSettings 
		{
			Version                 = "0.1.1",
                                Files                   = new [] 
								{
									new NuSpecContent {Source = "Source/AgileViews/bin/Release/AgileViews.dll", Target = "lib/net45"},
									new NuSpecContent {Source = "Libs/Microsoft.Msagl.dll", Target = "lib/net45"},
									new NuSpecContent {Source = "Libs/Microsoft.Msagl.Drawing.dll", Target = "lib/net45"},
									new NuSpecContent {Source = "Libs/Microsoft.Msagl.GraphViewerGdi.dll", Target = "lib/net45"}
                                },
                                BasePath                = "./",
                                OutputDirectory         = "./nuget",
								Verbosity				= NuGetVerbosity.Detailed
                            };
		
		NuGetPack(nuspec, nuGetPackSettings);
	}
);

Task("Default")
  .IsDependentOn("Build");

RunTarget(target);
