using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.OData;
using Microsoft.Azure.Mobile.Server;
using xPlatAuction.Backend.DataObjects;
using xPlatAuction.Backend.Models;

namespace xPlatAuction.Backend.Controllers
{
    public class BidController : TableController<Bid>
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            MobileServiceContext context = new MobileServiceContext();
            DomainManager = new EntityDomainManager<Bid>(context, Request);
        }

        // GET tables/Bid
        public IQueryable<Bid> GetAllBid()
        {
            return Query(); 
        }

        // GET tables/Bid/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public SingleResult<Bid> GetBid(string id)
        {
            return Lookup(id);
        }

        // PATCH tables/Bid/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task<Bid> PatchBid(string id, Delta<Bid> patch)
        {
             return UpdateAsync(id, patch);
        }

        // POST tables/Bid
        public async Task<IHttpActionResult> PostBid(Bid item)
        {
            Bid current = await InsertAsync(item);
            return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }

        // DELETE tables/Bid/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task DeleteBid(string id)
        {
             return DeleteAsync(id);
        }
    }
}
