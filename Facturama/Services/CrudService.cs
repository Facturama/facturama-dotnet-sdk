using System.Collections.Generic;
using RestSharp;

namespace Facturama.Services
{
    public abstract class CrudService<TI, TO> : HttpService<TI, TO> where TO : new()
    {
        protected CrudService(IHttpClient httpClient, string uri) 
            : base(httpClient, uri)
        {

        }

        public virtual TO Retrieve(string id)
        {
            return base.Get(id);
        }

        public virtual List<TO> List()
        {
            return base.GetList();
        }

        public virtual TO Create(TI obj)
        {
            return base.Post(obj);
        }
        public virtual TO Create3(TI obj)
        {
            return base.Post(obj);
        }
        public virtual TO CreateRet(TI obj)
        {
            return base.Post(obj);
        }
        public virtual TO CreateRet2(TI obj)
        {
            return base.Post(obj);
        }

        public virtual TO Remove(string id)
        {
            return base.Delete(id);
        }

        public virtual TO Update(TI model, string id)
        {
            return base.Put(model, id);
        }
    }
}
