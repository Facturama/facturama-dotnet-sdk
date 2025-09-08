using Facturama.Models;
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
