using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.Search;
using System.Configuration;
using SearchDemo.Data;

namespace DataIndexer
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Vehicle Data Indexer");

            // Get Keys from App.Config
            string searchServiceName = ConfigurationManager.AppSettings["SearchServiceName"];
            string adminApiKey = ConfigurationManager.AppSettings["SearchServiceAdminApiKey"];
            string indexName = ConfigurationManager.AppSettings["SearchIndexName"];

            // Create Search Client
            SearchServiceClient serviceClient 
                = new SearchServiceClient(searchServiceName, 
                                          new SearchCredentials(adminApiKey));

            // Get the Index from the Search Service
            /* GetClient is easier for admin work but queries should create SearchIndexClient directly */
            SearchIndexClient indexClient = serviceClient.Indexes.GetClient(indexName);

            // Get Documents from SQL to Index
            SqlContext db = new SqlContext();            
            for(int i = 1; i < 400; i++) // 40,000 in 100 page sizes
            {
                int skip = 100 * i;
                Console.WriteLine("Start " + skip);
                IndexVehicles(skip, db, indexClient);
            }
            Console.WriteLine();
            

            Console.WriteLine();
            Console.WriteLine("Finished! - Press Any Key");
            Console.ReadKey();
        }

        private static void IndexVehicles(int skip, SqlContext db, SearchIndexClient indexClient)
        {            
            var vehiclesToIndex = db.Vehicles.OrderBy(y => y.Id).Skip(skip).Take(100);

            // Convert SQL Relational Models to Flat Models
            List<VehicleFlat> docs = new List<VehicleFlat>();
            foreach (var vehicle in vehiclesToIndex)
            {
                docs.Add(vehicle.ToFlat());
            }

            // Create a list of indexer actions to upload docs
            var batch = Microsoft.Azure.Search.Models.IndexBatch.Upload(docs);

            // Call the IndexClient to attempt the actions
            try
            {
                indexClient.Documents.Index(batch);
            }
            catch (IndexBatchException e)
            {
                // Sometimes when your Search service is under load, indexing will fail for some of the documents in
                // the batch. Depending on your application, you can take compensating actions like delaying and
                // retrying. For this simple demo, we just log the failed document keys and continue.
                Console.WriteLine(
                    "Failed to index some of the documents: {0}",
                    String.Join(", ", e.IndexingResults.Where(r => !r.Succeeded).Select(r => r.Key)));
            }
        }
    }
}
