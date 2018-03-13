using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using System;
using System.Configuration;

namespace CosmosDB100
{
    public class CosmosInit
    {
        //the following items can be identified ehre or in the Web.Config
        private const string EndpointUrl = "DocumentDB URL";
        private const string PrimaryKey = "Authentication Key";

        private const string DatabaseName = "DatabaseName";
        private const string CollectionName = "CollectionName";
        private DocumentClient client;

        private void initialize()
        {
            //creating the database and the collection in case they havent been created previously 
            if (DatabaseName == null)
            {
                //missing database name
                
            }
            else
            {
                client = GetDocumentClient();
                this.client.CreateDatabaseIfNotExistsAsync(new Database { Id = DatabaseName });

                if (CollectionName == null)
                {
                    //missing collection
                    
                }
                else
                {
                    this.client.CreateDocumentCollectionIfNotExistsAsync(UriFactory.CreateDatabaseUri(DatabaseName), new DocumentCollection { Id = CollectionName });
                }
            }
        }

        public DocumentClient GetDocumentClient()
        {
            return new DocumentClient(new Uri(ConfigurationManager.AppSettings["endpoint"]), ConfigurationManager.AppSettings["authKey"]);
        }

        public string GetDatabaseName()
        {
            initialize();
            return DatabaseName;
        }

        public string GetCollectionName()
        {
            initialize();
            return CollectionName;
        }
    }
}