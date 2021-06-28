using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;

namespace Student_RestApi.Infrastructure
{
    public class XMLGenerator
    {
        public XMLGenerator()
        {
            this.Create();
        }

        private void Create()
        {
            XmlDocument document = new XmlDocument();
            try
            {
                document.Load(@"..\Students.xml");
            } catch (Exception e)
            {

            }
            XmlNode existingRoot = document.SelectSingleNode("STUDENTS");

            if (existingRoot == null)
            {
                XmlElement root = document.CreateElement("STUDENTS");
                document.AppendChild(root);
            }
            document.Save(@"..\Students.xml");
        }

        public bool AddChild(object childAttributes, string childName = "STUDENT")
        {
            XmlDocument document = new XmlDocument();
            document.Load(@"..\Students.xml");
            XmlNode root = document.SelectSingleNode("STUDENTS");
            XmlElement child = document.CreateElement(childName);

            var propertyList = childAttributes.GetType().GetProperties();
            XmlAttribute id = document.CreateAttribute("ID");
            int lastID = 0;
            try
            {
                lastID = Int32.Parse(root.LastChild?.Attributes["ID"].Value);
            } catch (Exception e) { }
            lastID++;
            id.Value = lastID.ToString();
            child.Attributes.Append(id);

            foreach (var property in propertyList)
            {
                XmlAttribute attribute = document.CreateAttribute(property.Name);
                attribute.Value = property.GetValue(childAttributes)?.ToString();
                child.Attributes.Append(attribute);
            }

            root.AppendChild(child);
            document.Save(@"..\Students.xml");

            return true;
        }

        public bool UpdateChild(int id, object childAttributes, string childName = "STUDENT")
        {
            XmlDocument document = new XmlDocument();
            document.Load(@"..\Students.xml");
            string query = $"STUDENTS/{childName}[@ID='{id}']";
            XmlNodeList children = document.SelectNodes(query);

            foreach (XmlNode child in children)
            {
                var propertyList = childAttributes.GetType().GetProperties();
                foreach (var property in propertyList)
                {
                    child.Attributes[property.Name].Value = property.GetValue(childAttributes)?.ToString();
                    ((XmlElement)child).SetAttribute(property.Name, property.GetValue(childAttributes)?.ToString());
                }
            }
            document.Save(@"..\Students.xml");

            return true;
        }

        public List<Student_RestApi.Models.Student> GetChildren(string childName = "STUDENT")
        {
            XmlDocument document = new XmlDocument();
            document.Load(@"..\Students.xml");
            XmlNode root = document.SelectSingleNode("STUDENTS");
            XmlNodeList users = document.SelectNodes($"STUDENTS/{childName}");
            List<Student_RestApi.Models.Student> _arr = new List<Student_RestApi.Models.Student>();

            foreach (XmlNode user in users)
            {
                Models.Student u = new Models.Student();
                u.name = user.Attributes["name"].Value;
                u.details = user.Attributes["details"].Value;
                u.id = Int32.Parse(user.Attributes["ID"].Value);

                _arr.Add(u);
            }


            return _arr;
        }

        public bool RemoveChild(int id, string childName = "STUDENT")
        {
            XmlDocument document = new XmlDocument();
            document.Load(@"..\Students.xml");
            XmlNode root = document.SelectSingleNode("STUDENTS");
            XmlNodeList user = document.SelectNodes($"STUDENTS/{childName}[@ID='{id.ToString()}']");

            foreach (XmlNode x in user)
            {
                root.RemoveChild(x);
            }
            document.Save(@"..\Students.xml");

            return true;
        }
    }
}
