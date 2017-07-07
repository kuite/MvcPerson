using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using MvcPerson.DataAccess.Model;

namespace MvcPerson.DataAccess.Write.Repositories
{
    public class XmlWriteRepository : IWriteRepository<Person>
    {
        private readonly string xmlPath;

        public XmlWriteRepository(string xmlPath)
        {
            this.xmlPath = xmlPath;
        }

        public async Task Create(Person item)
        {
            await Task.Run(() =>
            {
                if (!File.Exists(xmlPath))
                {
                    WriteToNewXml(new List<Person> { item });
                }
                else
                {
                    WriteToExistingXml(new List<Person> { item });
                }
            });
        }

        public async Task CreateBulk(IEnumerable<Person> itemCollection)
        {
            await Task.Run(() =>
            {
                if (!File.Exists(xmlPath))
                {
                    WriteToNewXml(itemCollection);
                }
                else
                {
                    WriteToExistingXml(itemCollection);
                }
            });
        }

        private void WriteToNewXml(IEnumerable<Person> ppl)
        {
            XmlWriterSettings xmlWriterSettings = new XmlWriterSettings();
            xmlWriterSettings.Indent = true;
            xmlWriterSettings.NewLineOnAttributes = true;
            using (XmlWriter xmlWriter = XmlWriter.Create(xmlPath, xmlWriterSettings))
            {
                xmlWriter.WriteStartDocument();
                xmlWriter.WriteStartElement("Poeple");

                foreach (Person item in ppl)
                {
                    xmlWriter.WriteStartElement("Person");
                    xmlWriter.WriteElementString("Id", item.Id.ToString());
                    xmlWriter.WriteElementString("Firstname", item.Firstname);
                    xmlWriter.WriteElementString("Lastname", item.Lastname);
                    xmlWriter.WriteEndElement();
                }

                xmlWriter.WriteEndElement();
                xmlWriter.WriteEndDocument();
            }
        }

        private void WriteToExistingXml(IEnumerable<Person> ppl)
        {
            XDocument xDocument = XDocument.Load(xmlPath);
            XElement root = xDocument.Element("Poeple");
            IEnumerable<XElement> rows = root.Descendants("Person");
            XElement firstRow = rows.Last();
            foreach (Person item in ppl)
            {
                firstRow.AddAfterSelf(
                   new XElement("Person",
                   new XElement("Id", item.Id.ToString()),
                   new XElement("Firstname", item.Firstname),
                   new XElement("Lastname", item.Lastname))
                );
            }

            xDocument.Save(xmlPath);
        }
    }
}
