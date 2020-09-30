using System;
using System.Web;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using cqrs_ui.models;

namespace cqrs_ui.pages {
    public class DetailsComponent : ComponentBase
    {
        [Parameter]
        public string id { get; set; }
        protected string region { get; set; } = String.Empty;
        public AesKey key { get; set; }

        private Uri uri; 

        [Inject]
        protected HttpClient httpClient { get; set; }

        [Inject]
        protected KeyService req { get; set; }

        protected override async Task OnInitializedAsync()
        {
            if( Uri.TryCreate( new Uri(req.Uri), $"{req.UriPath}/{id}", out uri) ) {
                await SendRequest();
            }
        }

        protected async Task HandleHttpRequest()
        {
            if( region != String.Empty ) {
                var uriBuilder = new UriBuilder(uri);
                var query = HttpUtility.ParseQueryString(uriBuilder.Query);
                query["forcedRegion"] = region;
                uriBuilder.Query = query.ToString();
                uri = uriBuilder.Uri;         
            }            

            await SendRequest();
        }

        private async Task SendRequest()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, uri);
            request.Headers.Add("Ocp-Apim-Subscription-Key", req.SubscriptionKey);
        
            var response = await httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {  
                key = JsonSerializer.Deserialize<AesKey>(await response.Content.ReadAsStringAsync());
            }  
        }
        
    }
}