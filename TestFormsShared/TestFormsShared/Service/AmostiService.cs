using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Android.Support.Design.Widget;
using Newtonsoft.Json;
using Org.Apache.Http.Client.Params;
using TestFormsShared.Models;

namespace TestFormsShared.Service
{
    public class AmostiService
    {
        private HttpClient client;

        public AmostiService()
        {
            client = new HttpClient();
            client.MaxResponseContentBufferSize = 2560000;
        }

        public async Task<List<Game>> GetGames()
        {
            WebRequest request = WebRequest.CreateHttp("https://app.amosti.net/api/v1/gameNames");
            string responseValue = null;
            List<string> names;

            using (var response = await request.GetResponseAsync())
            {
                using (var stream = response.GetResponseStream())
                {
                    if (stream != null)
                    {
                        using (var reader = new StreamReader(stream))
                        {
                            responseValue = await reader.ReadToEndAsync();
                            // Lite clean, men gjør jobben
                            responseValue = responseValue.Replace("\"", string.Empty);
                            responseValue = responseValue.Replace("[", string.Empty);
                            responseValue = responseValue.Replace("]", string.Empty);

                        }
                    }
                }
            }

            names = responseValue.Split(',').ToList<string>();

            if (names.Count > 0)
            {
                List<Game> gamenames = new List<Game>();
                foreach (var name in names)
                {
                    gamenames.Add(new Game()
                    {
                        GameName = name
                    });
                }
                return gamenames;
            }
            else
            {
                return null;
            }
        }

        public async Task<List<GameScoreBoard>> GetGameScores(string gamename)
        {
            WebRequest request =
                WebRequest.CreateHttp(String.Format(
                    "https://app.amosti.net/api/v1/gameScoreBoard?gameName={0}&count=30", gamename));
            string responseValue = null;

            using (var response = await request.GetResponseAsync())
            {
                using (var stream = response.GetResponseStream())
                {
                    if (stream != null)
                    {
                        using (var reader = new StreamReader(stream))
                        {
                            responseValue = await reader.ReadToEndAsync();

                        }
                    }
                }
            }

            var sresponse = JsonConvert.DeserializeObject<List<GameScoreBoard>>(responseValue);

            if (sresponse != null)
            {
                return sresponse;
            }
            else
            {
                return null;
            }
        }

        public async Task AddHighScore(GameScoreBoard record, bool isNewItem = false)
        {
            var restUrl = "https://app.amosti.net/api/v1/addScoreBoardData/";

            var uri = new Uri(restUrl);
            var recordJSON = JsonConvert.SerializeObject(record, Formatting.Indented);

            var content = new StringContent(recordJSON, Encoding.UTF8, "application/json");

            HttpResponseMessage response = null;
            if (isNewItem)
            {
                response = await client.PostAsync(uri, content);
            }
            
        }

    }
}
