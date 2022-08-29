
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Wox.Plugin;
using System.Windows.Forms;
using mroot_lib;

namespace Wox.Plugin.DevelSandbox
{
    static class Paths
    {

        public static string EtcFolderPath => mroot_lib.Paths.SystemFolders.etc;
        public static string SandboxConfigFolderPath => Path.Combine(EtcFolderPath, "wox_plugins", "devel_sandbox");
        public static string ConfigFilePath => Path.Combine(SandboxConfigFolderPath, "config.xml");

    }

    public class Main : IPlugin, IReloadable
    {
        #region members

        private readonly ProjectsLoader _projectloader = new ProjectsLoader();

        #endregion

        #region wox overrides
        public void Init(PluginInitContext context)
        {
            ReloadData();
        }

        public void ReloadData()
        {
            _projectloader.Config(Paths.ConfigFilePath);
        }

        public List<Result> Query(Query query)
        {
            List<Result> resultList = new List<Result>();

            AddCommands(resultList, query);
            AddOpenConfigCommand(resultList, query);

            return resultList;
        }

        #endregion

        #region commands

        private void AddOpenConfigCommand(List<Result> resultList, Query query)
        {
            if (StringTools.IsEqualOnStart(query.Search, "config", "settings"))
            {
                Result command = new Result
                {
                    Title = "Open config folder",
                    Score = 10,
                    IcoPath = "Images\\settings.png",
                    Action = e =>
                    {
                        ProcessStartInfo pinfo = new ProcessStartInfo
                        {
                            FileName = mroot.substitue_enviro_vars("||dcommander||"),
                            Arguments = $"-P L -T {Paths.SandboxConfigFolderPath}"
                        };
                        Process.Start(pinfo);

                        return true;
                    }
                };
                resultList.Add(command);
            }
        }

        private void AddCommands(List<Result> resultList, Query query)
        {
            foreach (var item in _projectloader.GetProjectDescriptors())
            {
                if (IsEqualOnStart(query.FirstSearch, item.Name) == false)
                {
                    continue;
                }
                Result commandResult = new Result
                {
                    Title = item.Name,
                    SubTitle = item.Language + " , " + item.Description,
                    Score = 1000,
                    IcoPath = item.ImagePath ?? "Images\\sandbox.png",
                    Action = e =>
                    {

                        var testPath = mroot_lib.Paths.TopFolders.test;
                        var dest_fodler = Path.Combine(testPath, Path.GetFileNameWithoutExtension(item.Path));
                        string project_folder_src = item.Path;

                        IO_utilities.CopyDir(project_folder_src, dest_fodler);

                        if (item.PreOpenCommand != null)
                        {
                            ProcessStartInfo pinfo = new ProcessStartInfo();
                            pinfo.FileName = item.PreOpenCommand;
                            Process.Start(pinfo);
                        }

                        var solutionFiles = Directory.GetFiles(dest_fodler, item.FileToOpen, SearchOption.AllDirectories).ToList();

                        if (solutionFiles.Count != 1)
                        {
                            MessageBox.Show(item.Name + " can't find or more files :" + item.FileToOpen);
                        }
                        else
                        {
                            ProcessStartInfo pinfo = new ProcessStartInfo();
                            pinfo.FileName = solutionFiles[0];
                            Process.Start(pinfo);
                        }

                        return true;
                    }
                };

                resultList.Add(commandResult);
            }
        }

        private static bool IsEqualOnStart(string query, params string[] values)
        {
            int lengthQuery = query.Length;
            foreach (var pattern in values)
            {
                int lengthPattern = pattern.Length;
                if (lengthPattern > lengthQuery)
                {
                    if (query.Equals(pattern.Substring(0, lengthQuery), StringComparison.CurrentCultureIgnoreCase))
                    {
                        return true;
                    }
                }
                else
                {
                    if (pattern.Equals(query.Substring(0, lengthQuery), StringComparison.CurrentCultureIgnoreCase))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        #endregion
    }
}
