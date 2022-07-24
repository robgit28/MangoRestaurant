using System.Text;
using Mango.web.Models;
using Newtonsoft.Json;

namespace Mango.web.Services
{
    public class BaseService : IBaseService
    {
        public ResponseDto responseDto { get; set; }
        public IHttpClientFactory httpClient { get; set; }

        public BaseService(IHttpClientFactory httpClient)
        {
            this.responseDto = new ResponseDto();
            this.httpClient = httpClient;
        }

        // our generic method to handle all our product service methods 
        public async Task<T> SendAsync<T>(ApiRequest apiRequest)
        {
            try
            {
                // create a new client object    
                var client = httpClient.CreateClient("MangoApi"); 
                // create new message to send 
                HttpRequestMessage message = new HttpRequestMessage();
                // configure message 
                message.Headers.Add("Accept", "application/json");
                // here the message is sent 
                message.RequestUri = new Uri(apiRequest.Url);
                client.DefaultRequestHeaders.Clear(); 
                // may not receive data so... 
                if(apiRequest.Data != null)
                {
                    // serialize data 
                    message.Content = new StringContent(JsonConvert.SerializeObject(apiRequest.Data),
                        Encoding.UTF8, "application/json"); 
                }
                //response from client side 
                HttpResponseMessage apiResponse = null; 
                switch(apiRequest.ApiType)
                {
                    case Constants.ApiType.POST:
                        message.Method = HttpMethod.Post;
                        break;
                    case Constants.ApiType.PUT:
                        message.Method = HttpMethod.Put;
                        break;
                    case Constants.ApiType.DELETE:
                        message.Method = HttpMethod.Delete;
                        break;
                    default:
                        message.Method = HttpMethod.Get;
                        break;
                }
                apiResponse = await client.SendAsync(message);
                // convert to string 
                var apiContent = await apiResponse.Content.ReadAsStringAsync();
                var apiResponseDto = JsonConvert.DeserializeObject<T>(apiContent);
                return apiResponseDto;

            }
            catch (Exception ex)
            {
                var dto = new ResponseDto
                {
                    DisplayMessage = "Error",
                    ErrorMessages = new List<string> { Convert.ToString(ex.Message) },
                    IsSuccess = false
                };  
                var result = JsonConvert.SerializeObject(dto);
                var apiResponseDto = JsonConvert.DeserializeObject<T>(result);
                return apiResponseDto; 
            }
        }
       
        public void Dispose()
        {
            GC.SuppressFinalize(true);
        }
    }
}
