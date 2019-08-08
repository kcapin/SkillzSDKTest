#if UNITY_IOS
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEditor.iOS.Xcode;
using System.IO;
using System.Text.RegularExpressions;

public static class SkillzXcodeAdditions
{
    //[PostProcessBuild(9080)]
    //public static void OnPreSkillzPostProcess(BuildTarget buildTarget, string pathToBuiltProject)
    //{
    //    if (buildTarget == BuildTarget.iOS)
    //    {
    //        string projectPath = pathToBuiltProject + "/Unity-iPhone.xcodeproj/project.pbxproj";

    //        PBXProject pbxProject = new PBXProject();
    //        pbxProject.ReadFromFile(projectPath);
    //        // Disable BITCODE for Skillz
    //        string target = pbxProject.TargetGuidByName("Unity-iPhone");
    //        pbxProject.SetBuildProperty(target, "ENABLE_BITCODE", "NO");
    //        pbxProject.WriteToFile(projectPath);

    //        // temp fix for codesignoncopy for Skillz embeded framework
    //        string contents = File.ReadAllText(projectPath);

    //        // Enable CodeSignOnCopy for the framework
    //        contents = Regex.Replace(contents,
    //            "(?<=Embed Frameworks)(?:.*)(\\/\\* Skillz\\.framework \\*\\/)(?=; };)",
    //            m => m.Value.Replace("/* Skillz.framework */",
    //                "/* Skillz.framework */; settings = {ATTRIBUTES = (CodeSignOnCopy, ); }"));

    //        File.WriteAllText(projectPath, contents);
    //    }
    //}

    [PostProcessBuild(9999)]
    public static void OnPostProcessBuild(BuildTarget buildTarget, string pathToBuiltProject)
    {
        if (buildTarget == BuildTarget.iOS)
        {
            // after Skillz plugin has done it's thing...
            string projectPath = pathToBuiltProject + "/Unity-iPhone.xcodeproj/project.pbxproj";
            string plistPath = pathToBuiltProject + "/Info.plist";
            string entitlementsPath = pathToBuiltProject + "/ios.entitlements";
            string targetName = "Unity-iPhone";

            PBXProject pbxProject = new PBXProject();
            pbxProject.ReadFromFile(projectPath);
            string target = pbxProject.TargetGuidByName(targetName);
            // Disable BITCODE for Skillz
            pbxProject.SetBuildProperty(target, "ENABLE_BITCODE", "NO");
            //pbxProject.AddFrameworkToProject(target, "PassKit.framework", true);
            // add apple pay capability
            //pbxProject.AddCapability(target, PBXCapabilityType.ApplePay, entitlementsPath);
            //pbxProject.AddCapability(target, PBXCapabilityType.PushNotifications, entitlementsPath);
            pbxProject.WriteToFile(projectPath);

            // fix for codesignoncopy for Skillz embeded framework
            string contents = File.ReadAllText(projectPath);
            // Enable CodeSignOnCopy for the framework
            contents = Regex.Replace(contents,
                "(?<=Embed Frameworks)(?:.*)(\\/\\* Skillz\\.framework \\*\\/)(?=; };)",
                m => m.Value.Replace("/* Skillz.framework */",
                    "/* Skillz.framework */; settings = {ATTRIBUTES = (CodeSignOnCopy, ); }"));
            File.WriteAllText(projectPath, contents);

            // add apple pay merchant id
            ProjectCapabilityManager capabilities = new ProjectCapabilityManager(projectPath, entitlementsPath, targetName);
            capabilities.AddApplePay(new string[] { "merchant.com.freerange.skillzSdkTest" });
            // add push notifications entitlement
            capabilities.AddPushNotifications(false);
            capabilities.WriteToFile();

            // app only uses https for login etc.  Set export encryption to false
            PlistDocument plist = new PlistDocument(); // Read Info.plist file into memory
            plist.ReadFromString(File.ReadAllText(plistPath));
            PlistElementDict rootDict = plist.root;
            // we don't use encryption (HTTPS doesn't count)
            string encryptKey = "ITSAppUsesNonExemptEncryption";
            rootDict.SetBoolean(encryptKey, false);
            // remove exit on suspend if it exists.  Key is depricated in iOS 13
            string exitsOnSuspendKey = "UIApplicationExitsOnSuspend";
            if(rootDict.values.ContainsKey(exitsOnSuspendKey))
            {
                rootDict.values.Remove(exitsOnSuspendKey);
            }

            File.WriteAllText(plistPath, plist.WriteToString()); // Override Info.plist
        }
    }
}
#endif