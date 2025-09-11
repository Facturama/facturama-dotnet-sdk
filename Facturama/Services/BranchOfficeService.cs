using Facturama.Models;
using Facturama.Services.BaseService;
using RestSharp;

namespace Facturama.Services
{
    public class BranchOfficeService : CrudService<BranchOffice, BranchOffice>
    {
        public BranchOfficeService(IHttpClient httpClient) 
            : base(httpClient, "branchoffice/")
        {

        }
    }
}
