using System;
using System.Diagnostics;
using System.IO;
using System.Security;
using JetBrains.Annotations;
using Renci.SshNet;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace Editor.PostPlayerBuildHandling
{
    public class PostPlayerBuildHandler
    {
        private const string RELATIVE_PATH_TO_AUTOMATION_FOLDER = "/../../Automation";
        private const string BATCH_FILE_NAME = "create_auto_run_unity_exe.bat";
        private const string RELATIVE_PATH_TO_INSTALLER_EXE_TEMP_OUTPUT_DIR = "/../../Automation/TempInstaller";
        private static readonly string INSTALLER_EXE_NAME = PlayerSettings.productName + ".exe";
        
        [PostProcessBuild(1)]
        public static void OnPostprocessBuild(BuildTarget target, string pathToBuiltProjectExe)
        {
            string pathToBuiltProjectDir = pathToBuiltProjectExe.Replace(Path.GetFileName(pathToBuiltProjectExe), "");

            if (!IsRunningOnCi() && (target == BuildTarget.StandaloneWindows || target == BuildTarget.StandaloneWindows64))
            {
                CreateInstaller(pathToBuiltProjectDir);
                UploadInstaller();
            }
            
            Environment.SetEnvironmentVariable("Test", "Test");
        }

        private static bool IsRunningOnCi()
        {
            return !String.IsNullOrEmpty(Environment.GetEnvironmentVariable("CI"));
        }

        private static void CreateInstaller(string pathToBuiltProject)
        {
            ProcessStartInfo  processStartInfo = new ProcessStartInfo(BATCH_FILE_NAME,  GetArguments(pathToBuiltProject));
            processStartInfo.WorkingDirectory = Application.dataPath + RELATIVE_PATH_TO_AUTOMATION_FOLDER;                                                                         
            processStartInfo.CreateNoWindow = false;
            Process proc = Process.Start(processStartInfo);
            proc.WaitForExit();
        }
        
        private static string GetArguments(string pathToBuiltProject)
        {
            return pathToBuiltProject + " " + GetPathToInstallerExeTempDir() + " " + PlayerSettings.productName;
        }

        private static string GetPathToInstallerExeTempDir()
        {
            return (Application.dataPath + RELATIVE_PATH_TO_INSTALLER_EXE_TEMP_OUTPUT_DIR).Replace(@"\", "/");
        }

        private static string GetPathToInstallerExeTempFile()
        {
            return Application.dataPath + RELATIVE_PATH_TO_INSTALLER_EXE_TEMP_OUTPUT_DIR +  @"/" + INSTALLER_EXE_NAME;
        }

        private static string GetRemoteUploadingPath()
        {
            return "/mnt/storage/public/game_builds/" + PlayerSettings.productName;
        }

        private static string GetRemoteUploadingFile()
        {
            return GetRemoteUploadingPath() + "/" + INSTALLER_EXE_NAME;
        }

        private static void UploadInstaller()
        {
            var connectionInfo = new ConnectionInfo("192.168.1.166",
                "admin",
                new PasswordAuthenticationMethod("admin", "&*g2wUXcshx5^Lw9U4^vAqv8"));
            using (var client = new SftpClient(connectionInfo))
            {
                client.Connect();
                if (!client.Exists(GetRemoteUploadingPath()))
                {
                    client.CreateDirectory(GetRemoteUploadingPath());
                }
                using (FileStream fsSource = new FileStream(GetPathToInstallerExeTempFile(),
                     FileMode.Open, FileAccess.Read))
                 {
                     client.UploadFile(fsSource, GetRemoteUploadingFile());
                 }
             }
         }
     }
 }