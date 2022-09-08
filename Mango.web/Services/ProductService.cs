using Mango.web.Models;

namespace Mango.web.Services
{
    public class ProductService : BaseService, IProductService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        
        public ProductService(IHttpClientFactory httpClientFactory) : base(httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<T> GetAllProductsAsync<T>()
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                Url = Constants.ProductAPIBase + "/api/products",
                ApiType = Constants.ApiType.GET,
                AccessToken = ""
            }); 
        }

        public async Task<T> GetProductByIdAsync<T>(int id)
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                Url = Constants.ProductAPIBase + "/api/products/" + id,
                ApiType = Constants.ApiType.GET,
                AccessToken = ""
            });
        }

        public async Task<T> CreateProductAsync<T>(ProductDto productDto)
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                Url = Constants.ProductAPIBase + "/api/products",
                ApiType = Constants.ApiType.POST,
                Data = productDto,
                AccessToken = ""
            }); 
        }
        
        public async Task<T> UpdateProductAsync<T>(ProductDto productDto)
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                Url = Constants.ProductAPIBase + "/api/products",
                ApiType = Constants.ApiType.PUT,
                Data = productDto,
                AccessToken = ""
            });
        }

        public async Task<T> DeleteProductAsync<T>(int id)
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                Url = Constants.ProductAPIBase + "/api/products/" + id,
                ApiType = Constants.ApiType.DELETE,
                AccessToken = ""
            });
        }
    }
}
