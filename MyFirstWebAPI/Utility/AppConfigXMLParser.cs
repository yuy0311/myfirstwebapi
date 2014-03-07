using MyFirstWebAPI.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;

namespace MyFirstWebAPI.Models.Utility
{
    public class AppConfigXMLParser
    {
        public static string getXMLValue(string module, string xmltag)
        {
            String xmlfile = HttpContext.Current.Server.MapPath(AppSettingsConstant.xmlfilepath);
            XmlDocument xmldoc = new XmlDocument();
            xmldoc.Load(xmlfile);
            XmlNodeList elements = xmldoc.GetElementsByTagName(module);
            if (elements.Count > 0)
            {
                foreach (XmlNode node in elements)
                {
                    XmlNode childNode = node[xmltag];
                    if (childNode != null)
                        return childNode.InnerText;
                }
            }
            return null;
        }

        public static string getAttributeValue(string module,string attribute)
        {
            String xmlfile = HttpContext.Current.Server.MapPath(AppSettingsConstant.xmlfilepath);
            XmlDocument xmldoc = new XmlDocument();
            xmldoc.Load(xmlfile);
            XmlNodeList elements = xmldoc.GetElementsByTagName(module);
            if (elements.Count > 0)
            { 
                foreach (XmlNode node in elements)
                {
                    return node.Attributes[attribute].Value;
                }
            }
            return null;
        }
    }
}