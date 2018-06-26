namespace TwoCS.TimeTracker.Services
{
    using System;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Newtonsoft.Json.Linq;

    public interface ILocationService
    {
        Task<JObject> GetLocactionAsync(string ipAddress);
    }

    public class LocationService : ILocationService
    {
        public async Task<JObject> GetLocactionAsync(string ipAddress)
        {
            JObject location = null;

            try
            {


                using (var client = new HttpClient())
                {
                    string result = await client.GetStringAsync("http://freegeoip.net/json/" + ipAddress);

                    location = JObject.Parse(result);
                }
            }
            catch (Exception ex)
            {

            }

            return location;
        }
    }
}
