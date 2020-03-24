using Kusto.Data;
using Kusto.Data.Net.Client;
using System;
using System.Data;

namespace ConsoleRepro
{
    class Program
    {
        static void Main(string[] args)
        {
            string cluster = "https://ustxsf1devax.southcentralus.kusto.windows.net";
            string database = "ustxsf1-dev-data-explr-test";
            string aadAuthority = "ericsson.com";

            var connString = new KustoConnectionStringBuilder(cluster)
            {
                FederatedSecurity = true,
                InitialCatalog = database,
                Authority = aadAuthority
            };

            var adminClient = KustoClientFactory.CreateCslAdminProvider(connString);
            string csl = $@".show database ['{database}'] schema as json";
            using (IDataReader reader = adminClient.ExecuteControlCommand(database, csl))
            {
                reader.Read();
                string json = reader[0].ToString();
                Console.WriteLine(json);
            }

            Console.WriteLine("Press ENTER to exit");
            Console.ReadLine();
        }
    }
}
