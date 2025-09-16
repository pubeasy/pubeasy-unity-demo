using System;
using System.IO;
using System.Linq;
using UnityEditor;

public static class ExportUnityPackage
{
	private static readonly string[] DefaultAssetPaths = new[]
	{
		"Assets/PubeasySDK",
		"Assets/Plugins",
		"Assets/ExternalDependencyManager",
		"Assets/iOSSetting",
		"Assets/Scenes"
	};

	[MenuItem("Pubeasy/Export .unitypackage")]
	public static void ExportFromMenu()
	{
		Export();
	}

	public static void Export()
	{
		// Ensure output directory exists
		var outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Builds");
		if (!Directory.Exists(outputDir))
		{
			Directory.CreateDirectory(outputDir);
		}

		// Compose filename with date
		var date = DateTime.Now.ToString("yyyyMMdd_HHmm");
		var packageName = $"PubeasySDK_{date}.unitypackage";
		var outputPath = Path.Combine(outputDir, packageName).Replace('\\', '/');

		// Filter only existing asset roots
		var pathsToExport = DefaultAssetPaths.Where(AssetDatabase.IsValidFolder).ToArray();
		if (pathsToExport.Length == 0)
		{
			UnityEngine.Debug.LogError("No valid asset folders found to export.");
			return;
		}

		AssetDatabase.ExportPackage(pathsToExport, outputPath, ExportPackageOptions.Recurse | ExportPackageOptions.IncludeDependencies);
		UnityEngine.Debug.Log($"Exported unitypackage: {outputPath}");
	}

	// Support batch mode: -executeMethod ExportUnityPackage.Export
}


