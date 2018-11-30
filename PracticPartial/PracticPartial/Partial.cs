using System;
using System.Collections.Generic;
using System.IO;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demos
{

    class Client : IComparable<Client>
    {
        static readonly float TVA = 1.9f;

        String _name;
        DateTime _lastContact;
        float _orderValue;

        public String name
        {
            get { return _name; }
            set { _name = value; }
        }

        public DateTime lastContact
        {
            get { return _lastContact; }
            set { _lastContact = value; }
        }

        public float orderValue
        {
            get { return _orderValue; }
            set { _orderValue += value * (1 + TVA); }
        }

        public int CompareTo(Client obj)
        {
            return (int)(orderValue - obj.orderValue);
        }
    }

    class ManageClients 
    {

        delegate List<Client> InfoGetter();

        private List<Client> allClients = new List<Client>();

        private void readAllClients()
        {
            StreamReader reader = new StreamReader(@"C:\Users\andra\Desktop\MyFile.txt");
            String line = reader.ReadLine();
            try
            {
                while (line != null)
                {
                    String[] info = line.Split();
                    Client client = new Client();
                    
                    client.name = info[0];
                    client.lastContact = DateTime.UtcNow;
                    client.orderValue = Int32.Parse(info[1]);

                    allClients.Add(client);

                    line = reader.ReadLine();

                }
                
            }
            catch (Exception e)
            {
                Console.WriteLine(e.InnerException);
            }
            finally
            {
                reader.Dispose();
            }
        }

        private List<Client> getLeastRelevantThreeClients()
        {
            List<Client> result = new List<Client>();
            readAllClients();
            allClients.Sort();
            for (int i = 0; i < 3; i++)
                result.Add(allClients[i]);

            return result;
        }

        private List<Client> getMostRelevantThreeClients()
        {
            List<Client> result = new List<Client>();
            readAllClients();
            allClients.Sort();
            for (int i = allClients.Count - 1; i > allClients.Count - 4; i--)
            {
                result.Add(allClients[i]);
            }

            return result;
        }

        public List<Client> getRequestedInformation(String preferance)
        {
            InfoGetter selectedMethod;
            if (preferance == "Profit")
                selectedMethod = new InfoGetter(getMostRelevantThreeClients);
            else
                selectedMethod = new InfoGetter(getLeastRelevantThreeClients);

            return selectedMethod();
                
        }

    }

    


    class Partial
    {

        static void Main(string[] args)
        {
            ManageClients managing = new ManageClients();
            List<Client> clients = managing.getRequestedInformation("Profit");

            IsolatedStorageFile isolatedStorage = IsolatedStorageFile.GetUserStoreForDomain();
            isolatedStorage.CreateDirectory("DemoData");
            IsolatedStorageFileStream fileStream = new IsolatedStorageFileStream(@"DemoData\Private.txt", FileMode.Create, FileAccess.Write, isolatedStorage);

            StreamWriter writer = new StreamWriter(fileStream);
            foreach (Client client in clients)
            {
                writer.WriteLine(client.name + " " + client.orderValue);
            }
            writer.Close();
            isolatedStorage.Close();
            // Read the fileIsolatedStorageFile
             isolatedStorage = IsolatedStorageFile.GetUserStoreForDomain();
            IsolatedStorageFileStream fileStream2 = new IsolatedStorageFileStream
                (@"DemoData\Private.txt",FileMode.Open, FileAccess.Read, isolatedStorage);

            StreamReader reader = new StreamReader(fileStream2);
            string line = reader.ReadLine();
            while (line != null){
                Console.WriteLine("{0}", line);
                line = reader.ReadLine();
            }
            reader.Close();
            isolatedStorage.Close();

            Console.Read();
        }

    }
}
