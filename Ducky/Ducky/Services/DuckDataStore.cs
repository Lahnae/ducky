﻿using Newtonsoft.Json;
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
    public class DuckDataStore : IDataStore<Sightings>
    {
        List<Sightings> sightings;

        public DuckDataStore()
        {
            sightings = new List<Sightings>();
        }

        public async Task<bool> AddItemAsync(Sightings item)
        {
            

            var client = new HttpClient();
            string Url = Constants.RestUrl+"/sightings";
            var uri = new Uri(string.Format(Url, string.Empty));
            var data = JsonConvert.SerializeObject(item);
            var content = new StringContent(data, Encoding.UTF8, "application/json");
            try
            {
                HttpResponseMessage response = await client.PostAsync(uri, content);
                sightings.Add(item);
                return await Task.FromResult(true);
            }
            catch(HttpRequestException)
            {
                return await Task.FromResult(false);
            }
            catch(TaskCanceledException)
            {
                return await Task.FromResult(false);
            }
            finally
            {
                if(client != null)
                {
                    client.Dispose();
                    client = null;
                }
            }
        }

        public async Task<bool> UpdateItemAsync(Sightings item)
        {
            var _item = sightings.Where((Sightings arg) => arg.Id == item.Id).FirstOrDefault();
            sightings.Remove(_item);
            sightings.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            var _item = sightings.Where((Sightings arg) => arg.Id == id).FirstOrDefault();
            sightings.Remove(_item);

            return await Task.FromResult(true);
        }

        public async Task<Sightings> GetItemAsync(string id)
        {
            return await Task.FromResult(sightings.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<Sightings>> GetItemsAsync(bool forceRefresh = false)
        {
            if (sightings != null && sightings.Any()) return sightings;
            var client = new HttpClient()
            {
                BaseAddress = new Uri(Constants.RestUrl)
            };
            var response = await client.GetAsync("/sightings");
            if (response.IsSuccessStatusCode)
            {
                var usersJson = await response.Content.ReadAsStringAsync();
                var rootobject = JsonConvert.DeserializeObject<List<Sightings>>(usersJson);
                foreach (var user_json in rootobject)
                {
                        sightings.Add(user_json);
                }
                return await Task.FromResult(sightings);
            }
            return await Task.FromResult(sightings);
        }
    }
}
