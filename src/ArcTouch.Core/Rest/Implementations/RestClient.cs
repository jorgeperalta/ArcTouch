using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using ArcTouch.Core.Constants;
using ArcTouch.Core.Rest.Interfaces;
using MvvmCross.Base;
using MvvmCross.Logging;

namespace ArcTouch.Core.Rest.Implementations
{
    public class RestClient : IRestClient
    {
        private readonly IMvxJsonConverter _jsonConverter;
        private readonly IMvxLog _mvxLog;
        private readonly HttpClient _httpClient;

        public RestClient(IMvxJsonConverter jsonConverter, IMvxLog mvxLog)
        {
            _jsonConverter = jsonConverter;
            _mvxLog = mvxLog;
            _httpClient = new HttpClient();
        }

        public async Task<TResult> MakeApiCall<TResult>(string url, HttpMethod method, object data = null) where TResult : class
        {
            url = url.Replace("http://", "https://");

           
            using (var request = new HttpRequestMessage { RequestUri = new Uri(AppConstants.TmdbBaseUrl + url), Method = method })
            {
                // add content
                if (method != HttpMethod.Get)
                {
                    var json = _jsonConverter.SerializeObject(data);
                    request.Content = new StringContent(json, Encoding.UTF8, "application/json");
                    request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                }

                var response = new HttpResponseMessage();
                try
                {
                    response = await _httpClient.SendAsync(request).ConfigureAwait(false);
                }
                catch (Exception ex)
                {
                    _mvxLog.ErrorException("MakeApiCall failed", ex);
                }

                var stringSerialized = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

                // deserialize content
                return _jsonConverter.DeserializeObject<TResult>(stringSerialized);
            }
            
        }
    }
}
