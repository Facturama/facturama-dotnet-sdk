using Facturama.Models;
using RestSharp;

namespace Facturama.Services
{
    public class BranchOfficeService : CrudService<BranchOffice, BranchOffice>
    {
        public BranchOfficeService(RestClient httpClient) 
            : base(httpClient, "branchoffice")
        {

        }
    }
}
