using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

[assembly: Xamarin.Forms.Dependency(typeof(Ducky.DuckDataStore))]
namespace Ducky
{
    public class DuckDataStore : IDataStore<Item>
    {
        List<Item> items;

        public DuckDataStore()
        {
            items = new List<Item>();
        }

        public async Task<bool> AddItemAsync(Item item)
        {
            

            var client = new HttpClient();
            string Url = "http://192.168.0.11:8081/sightings";
            var uri = new Uri(string.Format(Url, string.Empty));
            var data = JsonConvert.SerializeObject(item);
            var content = new StringContent(data, Encoding.UTF8, "application/json");
            try
            {
                HttpResponseMessage response = await client.PostAsync(uri, content);
                items.Add(item);
                Debug.WriteLine("Response on AddItemAsync :: " + response);
                return await Task.FromResult(true);
            }
            catch(HttpRequestException hre)
            {
                Debug.WriteLine("HttpRequestEx :: "+ hre);
            }
            catch(TaskCanceledException)
            {
                Debug.WriteLine("Request Canceled");
            }
            finally
            {
                if(client != null)
                {
                    client.Dispose();
                    client = null;
                }
            }
            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(Item item)
        {
            var _item = items.Where((Item arg) => arg.Id == item.Id).FirstOrDefault();
            items.Remove(_item);
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            var _item = items.Where((Item arg) => arg.Id == id).FirstOrDefault();
            items.Remove(_item);

            return await Task.FromResult(true);
        }

        public async Task<Item> GetItemAsync(string id)
        {
            return await Task.FromResult(items.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<Item>> GetItemsAsync(bool forceRefresh = false)
        {
            if (items != null && items.Any()) return items;
            var client = new HttpClient()
            {
                BaseAddress = new Uri("http://192.168.0.11:8081")
            };
            var response = await client.GetAsync("/sightings");
            if (response.IsSuccessStatusCode)
            {
                var usersJson = await response.Content.ReadAsStringAsync();
                var rootobject = JsonConvert.DeserializeObject<List<Item>>(usersJson);
                foreach (var user_json in rootobject)
                {
                        items.Add(user_json);
                }
                return await Task.FromResult(items);
            }
            return await Task.FromResult(items);
        }
    }
}
