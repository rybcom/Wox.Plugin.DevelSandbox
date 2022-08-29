using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Wox.Plugin.DevelSandbox
{
    public static class IO_utilities
    {
        public static void CopyDir(
          string sourceDirName,
          string destDirName,
          string copyFileRegexPattern= "(.)",
          string copyDirRegexPattern = "(.)",
          bool copySubDirs = true,
          Action<FileInfo> copyCallback = null)
        {
            // Get the subdirectories for the specified directory.
            DirectoryInfo dir = new DirectoryInfo(sourceDirName);

            if (!dir.Exists)
            {
                throw new DirectoryNotFoundException(
                    "Source directory does not exist or could not be found: "
                    + sourceDirName);
            }

            // If the destination directory doesn't exist, create it.
            if (!Directory.Exists(destDirName))
            {
                Directory.CreateDirectory(destDirName);
            }



            // Get the files in the directory and copy them to the new location.
            var files = dir.GetFiles().Where(f => Regex.IsMatch(f.FullName, copyFileRegexPattern));

            foreach (FileInfo file in files)
            {
                try
                {
                    if (copyCallback !=null)
                    {
                        copyCallback(file);
                    }
                    string temppath = Path.Combine(destDirName, file.Name);
                    file.CopyTo(temppath, true);
                }
                catch (Exception e)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(e.Message);
                    Console.ResetColor();
                }

            }

            var dirs = dir.GetDirectories().Where(d => Regex.IsMatch(d.FullName, copyDirRegexPattern)); ;

            // If copying subdirectories, copy them and their contents to new location.
            if (copySubDirs)
            {
                foreach (DirectoryInfo subdir in dirs)
                {
                    string destTempPath = Path.Combine(destDirName, subdir.Name);
                    CopyDir(subdir.FullName, destTempPath, copyFileRegexPattern, copyDirRegexPattern, copySubDirs, copyCallback);
                }
            }
        }
    }
}
