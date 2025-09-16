using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Callbacks;

#if UNITY_IOS
using UnityEditor.iOS.Xcode;
#endif

namespace BuildTools
{
    public static class iOSBuildFixes
    {
#if UNITY_IOS
        [PostProcessBuild(45)]
        public static void ApplyFixes(BuildTarget buildTarget, string buildPath)
        {
            if (buildTarget != BuildTarget.iOS)
            {
                return;
            }

            RemoveMnoThumbFromXcodeProject(buildPath);
            EnsurePodfileHook(buildPath);
        }

        private static void RemoveMnoThumbFromXcodeProject(string buildPath)
        {
            string pbxPath = PBXProject.GetPBXProjectPath(buildPath);
            if (!File.Exists(pbxPath)) return;

            var project = new PBXProject();
            project.ReadFromFile(pbxPath);

            var targetGuids = new List<string>();

            // Unity 2019.3+
            try
            {
                string main = project.GetUnityMainTargetGuid();
                if (!string.IsNullOrEmpty(main)) targetGuids.Add(main);
            }
            catch { }

            try
            {
                string framework = project.GetUnityFrameworkTargetGuid();
                if (!string.IsNullOrEmpty(framework)) targetGuids.Add(framework);
            }
            catch { }

            // 兼容旧版本 Unity 的 Target Guid
            try
            {
                string legacy = project.TargetGuidByName("Unity-iPhone");
                if (!string.IsNullOrEmpty(legacy)) targetGuids.Add(legacy);
            }
            catch { }

            targetGuids = targetGuids.Distinct().ToList();

            var removeFlags = new List<string> { "-mno-thumb" };
            foreach (var guid in targetGuids)
            {
                project.UpdateBuildProperty(guid, "OTHER_CFLAGS", null, removeFlags);
                project.UpdateBuildProperty(guid, "OTHER_CPLUSPLUSFLAGS", null, removeFlags);
            }

            project.WriteToFile(pbxPath);
        }

        private static void EnsurePodfileHook(string buildPath)
        {
            string podfilePath = Path.Combine(buildPath, "Podfile");
            if (!File.Exists(podfilePath)) return;

            string content = File.ReadAllText(podfilePath);

            // 确保使用 CocoaPods CDN 源
            const string cdnLine = "source 'https://cdn.cocoapods.org/'";
            if (!content.Contains(cdnLine))
            {
                content = cdnLine + "\n" + content;
            }

            // 注入 post_install 钩子，剔除 pods 中的 -mno-thumb
            if (!content.Contains("post_install do |installer|"))
            {
                string hook =
                    "\npost_install do |installer|\n" +
                    "  (installer.pods_project.targets + installer.aggregate_targets.map(&:user_project).flat_map(&:native_targets)).uniq.each do |t|\n" +
                    "    t.build_configurations.each do |config|\n" +
                    "      %w[OTHER_CFLAGS OTHER_CPLUSPLUSFLAGS].each do |key|\n" +
                    "        flags = config.build_settings[key]\n" +
                    "        if flags.is_a?(Array)\n" +
                    "          config.build_settings[key] = flags.reject { |f| f == '-mno-thumb' }\n" +
                    "        elsif flags.is_a?(String)\n" +
                    "          config.build_settings[key] = flags.gsub(/\\s*-mno-thumb\\s*/, ' ')\n" +
                    "        end\n" +
                    "      end\n" +
                    "    end\n" +
                    "  end\n" +
                    "end\n";

                content += hook;
            }

            File.WriteAllText(podfilePath, content);
        }
#endif
    }
}


