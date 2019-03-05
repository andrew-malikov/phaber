#addin nuget:?package=Cake.Git

var taskToExecute = Argument("task", "Default");
var target = Argument("target", "Phaber.sln");
var configuration = Argument("configuration", "Release");

var artifactsDir = "./artifacts/";

var currentBranch = Argument<string>("currentBranch", GitBranchCurrent("./").FriendlyName);
var isReleaseBuild = string.Equals(
   currentBranch, "master", StringComparison.OrdinalIgnoreCase
);

var nugetSource = "https://api.nuget.org/v3/index.json";
var nugetApiKey = Argument<string>("nugetApiKey", null);

Task("Restore").Does(() => {
   DotNetCoreRestore();
});

Task("Clean").Does(() => {
   DotNetCoreClean(target);
});

Task("Build")
   .IsDependentOn("Restore")
   .IsDependentOn("Clean")
   .Does(() => {
      DotNetCoreBuild(target);
   });

Task("CleanArtifacts").Does(() => {
   if (DirectoryExists(artifactsDir)) {
      DeleteDirectory(
         artifactsDir, 
         new DeleteDirectorySettings {
             Recursive = true,
             Force = true
         }
      );
   }

   CreateDirectory(artifactsDir);
});

Task("Pack")
   .IsDependentOn("CleanArtifacts")
   .Does(() => {
      var settings = new DotNetCorePackSettings {
         OutputDirectory = artifactsDir,
         Configuration = configuration,
         NoBuild = true
      };

      DotNetCorePack(target, settings);
   });

Task("Publish")
   .IsDependentOn("Pack")
   .Does(() => {
      var pushSettings = new DotNetCoreNuGetPushSettings {
            Source = nugetSource,
            ApiKey = nugetApiKey
      };

      var pkgs = GetFiles($"{artifactsDir}*.nupkg");
      foreach(var pkg in pkgs) {
         Information($"Publishing \"{pkg}\"");
         DotNetCoreNuGetPush(pkg.FullPath, pushSettings);
      }
   });

if (isReleaseBuild)
   Task("Default")
      .IsDependentOn("Build")
      .IsDependentOn("Publish");
else
   Task("Default")
      .IsDependentOn("Build");

RunTarget(taskToExecute);
