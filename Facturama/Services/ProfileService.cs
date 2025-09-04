using System;
using System.Collections.Generic;
using System.Text;
using Facturama.Models.Response;
using RestSharp;

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
