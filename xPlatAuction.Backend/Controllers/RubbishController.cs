using Microsoft.Azure.Mobile.Server.Config;
using System.Collections.Generic;
using System.Web.Http;
using xPlatAuction.Backend.DataObjects;

namespace xPlatAuction.Backend.Controllers
{
    [MobileAppController]
    //[Authorize]
    public class RubbishController : ApiController
    {
        public IEnumerable<Rubbish> Get()
        {
            return new List<Rubbish>
            {
                new Rubbish {Name = "Bob", Description = "1"},
                new Rubbish {Name = "Jon", Description = "2"}
            };
        }

        public Rubbish Get(int id)
        {
            return new Rubbish {Name = "Jon", Description = "2"};
        }
    }
}
