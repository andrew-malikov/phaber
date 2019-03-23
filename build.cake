#addin nuget:?package=Cake.Git

var target = Argument("target", "Default");
var solution = Argument("solution", "Phaber.sln");
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
   DotNetCoreClean(solution);
});

Task("Build")
   .IsDependentOn("Restore")
   .IsDependentOn("Clean")
   .Does(() => {
      var settings = new DotNetCoreBuildSettings {
         Configuration = configuration
      };

      DotNetCoreBuild(solution, settings);
   });

Task("Test")
   .Does(() => {
         var projects = GetFiles("./*Test/*.csproj");
         
         var settings = new DotNetCoreTestSettings() {
            Configuration = configuration,
            NoBuild = true
         };
         
         foreach(var project in projects) 
            DotNetCoreTest(project.FullPath, settings);   
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
   .IsDependentOn("Test")
   .IsDependentOn("CleanArtifacts")
   .Does(() => {
      var settings = new DotNetCorePackSettings {
         OutputDirectory = artifactsDir,
         Configuration = configuration,
         NoBuild = true
      };

      DotNetCorePack(solution, settings);
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

Task("Default")
   .IsDependentOn("Build")
   .IsDependentOn("Test");

RunTarget(target);
