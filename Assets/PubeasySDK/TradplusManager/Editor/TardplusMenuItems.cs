using UnityEditor;
using UnityEngine;

namespace Tardplus.TradplusEditorManager.Editor
{
    public class TardplusMenuItems
    {
        [MenuItem("Pubeasy/PubeasyManager")]
        private static void TradplusMangerWindow()
        {
            TradplusEditorManagerWindow.ShowManager();
        }

        [MenuItem("Pubeasy/Documentation/Access Document")]
        private static void Documentation()
        {
            EditorUtility.DisplayDialog("Documentation", "Pubeasy 文档即将上线。", "OK");
        }

        [MenuItem("Pubeasy/Documentation/DownLoad Plugin")]
        private static void PluginDownLoad()
        {
            EditorUtility.DisplayDialog("Download", "Pubeasy 插件下载页面即将上线。", "OK");
        }

        
    }
}
