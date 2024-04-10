using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Google.Crypto.Tink.Signature;
using CalculatorClientApp.Models;
using System.Text.Json;
using Java.Net;


namespace CalculatorClientApp.Services
{
    internal class MonkeyWebAPIProxy
    {
        private HttpClient client;
        private string baseUrl;
        public static string BaseAddress = DeviceInfo.Platform == DevicePlatform.Android ? "http://10.0.2.2:5013/api/" : "http://localhost:5013/api/";

        public MonkeyWebAPIProxy()
        {
            this.client = new HttpClient();
            this.baseUrl = BaseAddress;
        }

        public async Task<MonkeyList> ReadAllMonkeysGETasync()
        {
            //Set URI to the specific function API
            string url = $"{this.baseUrl}ReadAllMonkeys";
            try
            {
                //Call the server API
                HttpResponseMessage response = await client.GetAsync(url);
                //Check status
                if (response.IsSuccessStatusCode)
                {
                    //Extract the content as string
                    string resContent = await response.Content.ReadAsStringAsync();
                    //Desrialize result
                    JsonSerializerOptions options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    MonkeyList result = JsonSerializer.Deserialize<MonkeyList>(resContent, options);
                    return result;
                }
                else
                {
                    MonkeyList monkeyList = new MonkeyList
                    {
                        Success = false
                    };
                    return monkeyList;
                }
            }
            catch (Exception ex)
            {
                MonkeyList monkeyList = new MonkeyList
                {
                    Success = false
                };
                return monkeyList;
            }

        }
    }
}
