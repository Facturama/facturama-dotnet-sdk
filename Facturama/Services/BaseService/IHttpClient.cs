using Facturama.Models.Request;
using System.Threading.Tasks;

namespace Facturama.Services.BaseService
{
    public interface IHttpClient
    {
        string GetBaseUrl();
        TO Post<TO, TI>(string url, TI data, HttpRequestOptions options = null);
        Task<TO> PostAsync<TO, TI>(string url, TI data, HttpRequestOptions options = null);
        TO Get<TO>(string url, HttpRequestOptions options = null);
        Task<TO> GetAsync<TO>(string url, HttpRequestOptions options = null);
        TO Put<TO, TI>(string url, TI data, HttpRequestOptions options = null);
        Task<TO> PutAsync<TO, TI>(string url, TI data, HttpRequestOptions options = null);
        TO Delete<TO>(string url, HttpRequestOptions options = null);
        Task<TO> DeleteAsync<TO>(string url, HttpRequestOptions options = null);
    }
}
