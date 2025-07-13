#if UNITY_EDITOR && UNITY_IOS

using System.IO;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.iOS.Xcode;

public class TiktokXcodeBuildProcess : IPostprocessBuild
{
    public int callbackOrder { get { return 0; } }

    public void OnPostprocessBuild(BuildTarget target, string path)
    {
        if (target == BuildTarget.iOS)
        {
            // 获取Xcode项目路径
            string projectPath = PBXProject.GetPBXProjectPath(path);
            PBXProject project = new PBXProject();
            project.ReadFromFile(projectPath);

            // 设置Bundle Identifier
            project.SetBuildProperty(project.GetUnityFrameworkTargetGuid(), "ENABLE_BITCODE", "NO");
            project.AddBuildProperty(project.GetUnityFrameworkTargetGuid(), "OTHER_LDFLAGS", "-ObjC");
            // 保存修改
            project.WriteToFile(projectPath);
            
            
            
            // 获取Xcode项目路径
            string plistPath = path + "/Info.plist";
            PlistDocument plist = new PlistDocument();
            plist.ReadFromFile(plistPath);

            // 添加配置项
            // PlistElementDict rootDict = plist.root;
            // rootDict.SetString("NSUserTrackingUsageDescription", "Allow tracking");

            // 保存修改
            File.WriteAllText(plistPath, plist.WriteToString());
        }
    }
}
#endif