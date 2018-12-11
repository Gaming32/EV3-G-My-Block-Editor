using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Compression;
using System.Xml;

namespace Display_EV3_G_My_Block.EV3P
{
    /// <summary>
    /// A class for storing data for an EV3 Program file (*.ev3p)
    /// </summary>
    class EV3P
    {
        public enum ProgramTypes {Program = 0, MyBlock = 1};

        public string path { get; set; }
        public string name { get; set; }
        public ProgramTypes type { get; set; }
        public Dictionary<string, MyBlock_Parameter> parameters { get; set; }

        /// <summary>
        /// Creates an EV3P object with the specifed relative path, name, and whether it's a program or My Block
        /// </summary>
        /// <param name="path">The relative path of the EV3P</param>
        /// <param name="name">The Name of the EV3P</param>
        /// <param name="type">Whether the EV3P is a program or My Block</param>
        public EV3P(string path, string name, ProgramTypes type)
        {
            this.path = path;
            this.name = name;
            this.type = type;
        }

        public EV3P(ZipArchiveEntry ev3p)
        {
            try
            {
                ZipArchiveEntry data = ev3p.Archive.GetEntry(ev3p.Name + ".mbxml");
                XmlDocument mbxml = new XmlDocument();
                mbxml.Load(data.Open());
                this.type = ProgramTypes.MyBlock;

                XmlNode dataNode = mbxml.DocumentElement.SelectSingleNode("/EditorDefinitions/PolyGroups/PolyGroup/Block");
                foreach(XmlNode parameter in dataNode.ChildNodes)
                {
                    if(parameter.Name == "ParamInfo")
                    {
                        parameters.Add(parameter.Attributes["Name"].InnerText, new MyBlock_Parameter(
                            parameter.Attributes["DisplayName"].InnerText,
                            parameter.Attributes["Direction"].InnerText,
                            parameter.Attributes["Identification"].InnerText,
                            parameter.Attributes["DataType"].InnerText));
                    }
                }
            }
            catch (Exception)
            {
                this.type = ProgramTypes.Program;
            }
            finally
            {
                this.path = ev3p.FullName;
                this.name = ev3p.Name.TrimEnd(".ev3p".ToCharArray());
            }
        }

        public override string ToString()
        {
            string returnValue = "";

            //Return path
            returnValue += "Path =       ";
            returnValue += path;

            //Return type
            returnValue += "\r\n";
            returnValue += "Type =       ";
            if (type == ProgramTypes.MyBlock)
                returnValue += "MyBlock";
            else
                returnValue += "Program";

            //Return parameters
            returnValue += "\r\n";
            if (type == ProgramTypes.MyBlock)
            {
                List<string> strParameters = new List<string>();
                foreach (KeyValuePair<string, MyBlock_Parameter> parameter in parameters)
                    strParameters.Add($"[Name={parameter.Key}; DisplayName={parameter.Value.displayName}; Direction={parameter.Value.direction}; Image={parameter.Value.imageId}; DataType={parameter.Value.dataType}");
                returnValue += $@"Parameters = {String.Join("\r\n             ", strParameters.ToArray())}";
            }
            return returnValue;
        }
    }
}
