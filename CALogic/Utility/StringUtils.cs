using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace ChannelAdvisor
{
    /// <summary>
    /// This class represents utilities for working with strings
    /// </summary>
    public static class StringUtils
    {
        /// <summary>
        /// Remove all non-numeric characters from the string
        /// </summary>
        /// <param name="aInputString">Input string</param>
        /// <returns>String which contains only numeric characters</returns>
        public static string RemoveNonNumericCharacters(string aInputString)
        {
            char[] NumericSymbols = new char[10] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
            StringBuilder lResult = new StringBuilder();
            foreach (char lChar in aInputString.ToCharArray())
                if (Array.IndexOf<char>(NumericSymbols, lChar) > -1)
                    lResult.Append(lChar);

            return lResult.ToString();
        }

        public static T ConvertTo<T>(this XmlNode node) where T: class
        {
            StringBuilder buffer = new StringBuilder();

            StringWriter sw = new StringWriter(buffer);
            sw.Write(node.OuterXml);

            XmlRootAttribute xRoot = new XmlRootAttribute();
            xRoot.ElementName = node.LocalName;
            xRoot.IsNullable = true;
            xRoot.Namespace = node.NamespaceURI;
            XmlSerializer ser = new XmlSerializer(typeof(T), xRoot);
            T result = ser.Deserialize(new StringReader(buffer.ToString())) as T;

            return result;
        }
    }
}
