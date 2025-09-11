using System.Collections.Generic;

namespace Facturama.Services.BaseService
{
    public abstract class CrudService<TI, TO> : HttpService<TI, TO> where TO : new()
    {
        protected CrudService(IHttpClient httpClient, string uri) 
            : base(httpClient, uri)
        {

        }

        public virtual TO Retrieve(string id)
        {
            return Get(id);
        }

        public virtual List<TO> List()
        {
            return Get<List<TO>>(null);
        }

        public virtual TO Create(TI obj)
        {
            return Post(obj);
        }

        public virtual TO Remove(string id)
        {
            return Delete(id);
        }

        public virtual TO Update(TI model, string id)
        {
            return Put(model, id);
        }
    }
}
