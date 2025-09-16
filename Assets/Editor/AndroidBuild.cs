using System;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEngine;

namespace BuildTools
{
    public static class AndroidBuilder
    {
        public static void BuildAPK()
        {
            try
            {
                string[] enabledScenePaths = EditorBuildSettings.scenes
                    .Where(scene => scene.enabled)
                    .Select(scene => scene.path)
                    .ToArray();

                if (enabledScenePaths.Length == 0)
                {
                    throw new InvalidOperationException("No enabled scenes found in EditorBuildSettings.");
                }

                string projectRoot = Directory.GetCurrentDirectory();
                string outputDirectory = Path.Combine(projectRoot, "Builds", "Android");
                string outputPath = Path.Combine(outputDirectory, "tradplus_demo.apk");

                if (!Directory.Exists(outputDirectory))
                {
                    Directory.CreateDirectory(outputDirectory);
                }

                // Ensure we generate APK, not AAB; prefer Development build for testing
                EditorUserBuildSettings.buildAppBundle = false;
                EditorUserBuildSettings.exportAsGoogleAndroidProject = false;
                EditorUserBuildSettings.androidBuildType = AndroidBuildType.Development;
                EditorUserBuildSettings.development = true;
                EditorUserBuildSettings.connectProfiler = false;

                BuildPlayerOptions options = new BuildPlayerOptions
                {
                    scenes = enabledScenePaths,
                    locationPathName = outputPath,
                    target = BuildTarget.Android,
                    targetGroup = BuildTargetGroup.Android,
                    options = BuildOptions.Development
                };

                Debug.Log($"[AndroidBuilder] Building APK to: {outputPath}");
                BuildReport report = BuildPipeline.BuildPlayer(options);
                BuildSummary summary = report.summary;

                if (summary.result == BuildResult.Succeeded)
                {
                    Debug.Log($"[AndroidBuilder] Build succeeded: {summary.totalSize} bytes");
                }
                else
                {
                    throw new Exception($"Build failed: {summary.result}\nErrors: {GetBuildErrors(report)}");
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"[AndroidBuilder] Exception: {ex.Message}\n{ex.StackTrace}");
                EditorApplication.Exit(1);
            }
        }

        private static string GetBuildErrors(BuildReport report)
        {
            try
            {
                var errors = report.steps
                    .SelectMany(step => step.messages)
                    .Where(msg => msg.type == LogType.Error)
                    .Select(msg => msg.content)
                    .ToArray();
                return string.Join("\n", errors);
            }
            catch
            {
                return string.Empty;
            }
        }
    }
}


