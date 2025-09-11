using Facturama.Models.Response;
using Facturama.Services.BaseService;

namespace Facturama.Services
{
    public class ProfileService : HttpService<Profile, Profile>
    {
        public ProfileService(IHttpClient httpClient) 
            : base(httpClient, "profile/")
        {

        }

        public Profile Retrieve()
        {
            return base.Get("");
        }
    }
}
