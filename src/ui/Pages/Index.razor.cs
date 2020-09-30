using System;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text.Json;
using System.Collections.Generic;
using Microsoft.AspNetCore.Components;
using System.ComponentModel.DataAnnotations;
using cqrs_ui.models;

namespace cqrs_ui.pages {

    public class IndexComponent : ComponentBase
    {
        protected int numKeys = 50;
        protected List<AesKey> keys;

        [Inject]
        protected HttpClient httpClient { get; set; }

        [Inject]
        protected KeyService req { get; set; }

        protected async Task HandleHttpRequest()
        {
            if( Uri.TryCreate( new Uri(req.Uri), $"{req.UriPath}/{numKeys}", out Uri uri) ) {
                var request = new HttpRequestMessage(HttpMethod.Post, uri);
                request.Headers.Add("Ocp-Apim-Subscription-Key", req.SubscriptionKey);
            
                var response = await httpClient.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {  
                    keys = JsonSerializer.Deserialize<List<AesKey>>(await response.Content.ReadAsStringAsync());
                }
            }
        }

        protected Task HandleSaveConfig() {
            return Task.FromResult(true);
        }
    }
}