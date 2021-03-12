using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;

namespace SprintApp.Models
{
    // Class holding MongoDB database methods
    public static class DBConnection
    {

        /*************************
        *        GLOBALS         *
        *************************/
        public static MongoClient client = new MongoClient("");

        public static IMongoDatabase db = client.GetDatabase("");



        /*************************
        *        METHODS         *
        *************************/

        // Retrieves a collection as a List
        public static IMongoCollection<T> GetList<T>(string collName)
        {
            IMongoCollection<T> colls = db.GetCollection<T>(collName);

            return colls;

        } // END GetList<T>()



        // Retrieve a collection as a List using a filter
        public static List<T> GetCollectionAsList<T>(string collName)
        {
            IMongoCollection<T> colls = db.GetCollection<T>(collName);
            var collection = colls.Find(Builders<T>.Filter.Empty).ToList();

            return collection;

        } // END GetCollectionAsList<T>()



        // Returns count of a collection's documents
        public static long GetCollectionCount<T>(string collName)
        {
            IMongoCollection<T> colls = db.GetCollection<T>(collName);

            return colls.CountDocuments(Builders<T>.Filter.Empty);

        } // END GetCollectionCount<T>()



        // Inserts 1 document
        public static void InsertDocument<T>(string collName, T obj)
        {
            IMongoCollection<T> colls = db.GetCollection<T>(collName);

            colls.InsertOne(obj);

        } // END InsertDocument<T>()



        // Updates 1 document
        public static void UpdateDocument<T>(string collName, FilterDefinition<T> filter, UpdateDefinition<T> update)
        {
            IMongoCollection<T> colls = db.GetCollection<T>(collName);

            colls.UpdateOne(filter, update);

        } // END UpdateDocument<T>()



        // Deletes 1 document
        public static void DeleteDocument<T>(string collName, FilterDefinition<T> filter)
        {
            IMongoCollection<T> colls = db.GetCollection<T>(collName);

            colls.DeleteOne(filter);

        } // END DeleteDocument<T>()



    } // END DBConnection class

} // END namespace