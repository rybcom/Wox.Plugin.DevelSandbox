using mroot_lib;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace Wox.Plugin.DevelSandbox
{

    internal class ProjectsLoader
    {

        #region members

        private List<ProjectDescriptor> _projectList;

        #endregion 

        #region api

        internal ProjectsLoader()
        {
            _projectList = new List<ProjectDescriptor>();
        }

        internal void Config(string filepath)
        {
            this._projectList.Clear();

            XmlDocument doc = new XmlDocument();
            doc.LoadXml(File.ReadAllText(filepath));

            XmlNodeList items = doc.DocumentElement.SelectNodes("/project_list/project");

            foreach (XmlNode node in items)
            {
                string name = node.Attributes["name"].Value;
                string language= node.Attributes["language"].Value;
                string description = node.Attributes["descrpition"].Value;
                string path = node.Attributes["path"].Value;
                string fileToOpen = node.Attributes["file_to_open"].Value;
                string preOpenCommand = node.Attributes["pre_open_command"]?.Value;
                string imagePath= node.Attributes["image"]?.Value;


                ProjectDescriptor descriptor = new ProjectDescriptor();
                descriptor.Name = name;
                descriptor.Language = language;
                descriptor.Description = description;
                descriptor.Path = mroot.substitue_enviro_vars(path);
                descriptor.FileToOpen = fileToOpen;
                descriptor.PreOpenCommand = mroot.substitue_enviro_vars(preOpenCommand);
                descriptor.ImagePath = imagePath;

                this._projectList.Add(descriptor);
            }
        }


        internal List<ProjectDescriptor> GetProjectDescriptors()
        {
            return this._projectList;
        }

        #endregion 

        #region private methods

        

        #endregion
    }

}
