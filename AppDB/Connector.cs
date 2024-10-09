using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB;
using MongoDB.Bson;
using MongoDB.Driver;
using Clicker.Models;
using System.Collections.ObjectModel;
namespace Clicker.AppDB
{
    public class Connector
    {
        static readonly string key = "linkDoBazyMongo";
        static readonly string dbName = "ClickerApp";
        static readonly string collectionName = "clickers";

        //stan powodzenia danej operacji w danej chwili
        public bool connectorState {  get; set; } = false;
        

        static MongoClient client = new MongoClient(key);
        static IMongoDatabase db = client.GetDatabase(dbName);
        static IMongoCollection<ClickerModel> collection = db.GetCollection<ClickerModel>(collectionName);


        public ObservableCollection<ClickerModel> Clickers { get; set; } = new ObservableCollection<ClickerModel>();

        public async Task insertClicker(ClickerModel clicker)
        {
            try
            {
                var filter = Builders<ClickerModel>.Filter.Eq(p => p.Name, clicker.Name);
                var foundClicker = await collection.Find(filter).FirstOrDefaultAsync();
                if (foundClicker != null)
                {
                    connectorState = false;
                }
                else 
                {
                    try
                    {
                        await collection.InsertOneAsync(clicker);
                        connectorState = true;
                    }
                    catch
                    {
                        connectorState = false;
                    }
                }
            }
            catch
            {
                connectorState = false;
            }
        }

        public async Task getAllClickers()
        {
            var res = await collection.FindAsync(_ => true);
            var allClickers = await res.ToListAsync();
            Clickers.Clear();

            if (allClickers != null)
            {
                foreach (var clicker in allClickers)
                {
                    Clickers.Add(clicker);
                }
                connectorState = true;
            }
            else
            {
                connectorState = false;
            }
        }
        public async Task deleteOneClicker(string name)
        {
            if (name != null)
            {
                var filter = Builders<ClickerModel>.Filter.Eq(p => p.Name, name);
                var res = await collection.DeleteOneAsync(filter);
                connectorState = true;
            }
            else
            {
                connectorState = false;
            }
        }

        public async Task saveAllClickers(ObservableCollection<ClickerModel> Clickers)
        {
            foreach (var clicker in Clickers)
            {
                var filter = Builders<ClickerModel>.Filter.Eq(p => p.Id, clicker.Id);
                var update = Builders<ClickerModel>.Update
                .Set(p => p.Value, clicker.Value);

                try
                {
                    var res = await collection.UpdateOneAsync(filter, update);

                    if (res.ModifiedCount == 0)
                    {
                        connectorState = false;
                    }
                }
                catch
                {
                    connectorState = false;
                }
            }

        }


    }
}
