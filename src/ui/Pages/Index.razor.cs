using System;
using System.Threading.Tasks;
using System.Net.Http;
using System.Collections.Generic;
using System.Text.Json;
using Microsoft.AspNetCore.Components;

namespace aeskeys.pages {
    public class IndexBase : ComponentBase
    {
        protected AesRequest req = new AesRequest();
        protected List<AesKey> keys;
        protected HttpClient httpClient = new HttpClient();
        protected async Task HandleHttpRequest()
        {
            if( Uri.TryCreate( new Uri(req.Uri), $"/passwords/{req.TotalKeys}", out Uri uri) ) {
                var request = new HttpRequestMessage(HttpMethod.Post, uri);
                request.Headers.Add("Ocp-Apim-Subscription-Key", req.SubscriptionKey);
            
                var response = await httpClient.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {  
                    keys = JsonSerializer.Deserialize<List<AesKey>>(await response.Content.ReadAsStringAsync());
                }
            }
        }
    }
}