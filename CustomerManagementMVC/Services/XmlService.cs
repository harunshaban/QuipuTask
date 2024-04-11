using CustomerManagementMVC.Models;
using System.Xml.Serialization;

namespace CustomerManagementMVC.Services
{
    public class XmlService
    {
        public List<Customer> ImportClientsFromXml(string xmlFilePath)
        {
            List<Customer> clients = new List<Customer>();

            XmlSerializer serializer = new XmlSerializer(typeof(List<Customer>), new XmlRootAttribute("Clients"));

            using (FileStream fileStream = new FileStream(xmlFilePath, FileMode.Open))
            {
                clients = (List<Customer>)serializer.Deserialize(fileStream);
            }

            return clients;
        }
    }
}
