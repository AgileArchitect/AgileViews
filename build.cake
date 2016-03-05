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
	MSBuild(solution, s => {	
		s.SetConfiguration(configuration)
		 //.WithProperty("TreatWarningsAsErrors", "True")
		 .SetVerbosity(Verbosity.Quiet);
	});
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
		
		CreateDirectory("nuget");
		
		var nuGetPackSettings   = new NuGetPackSettings 
		{
			Version                 = "0.1.3",
                                Files                   = new [] 
								{
									new NuSpecContent {Source = "AgileViews.dll", Target = "lib"},
									new NuSpecContent {Source = "Microsoft.Msagl.dll", Target = "lib"},
									new NuSpecContent {Source = "Microsoft.Msagl.Drawing.dll", Target = "lib"},
									new NuSpecContent {Source = "Microsoft.Msagl.GraphViewerGdi.dll", Target = "lib"}
                                },
                                BasePath                = "./Source/AgileViews/bin/Release",
                                OutputDirectory         = "./nuget",
								Verbosity				= NuGetVerbosity.Detailed
                            };
		
		NuGetPack(nuspec, nuGetPackSettings);
	}
);

Task("Default")
  .IsDependentOn("Build");

RunTarget(target);
