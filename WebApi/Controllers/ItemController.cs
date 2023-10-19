using System.Linq;
using System.Threading.Tasks;
using Engine.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;
using WebApi.Converters;

namespace WebApi.Controllers
{
    [ApiController]
    public class ItemController : ControllerBase
    {
        private IEngine Engine { get; }
        public ItemController(IEngine engine)
        {
            Engine = engine;
        }

        [HttpGet]
        [Route("api/item/{id}")]
        public async Task<ItemWebModel> Get(string id)
        {
            var item = Engine.Get(id);
            return item.ToWebModel();
        }

        [HttpPost]
        [Route("api/item")]
        public async Task<ItemWebModel> Create(ItemCreateWebRequestModel model)
        {
            var item = Engine.Create(model.DesignCode, model.Values.Select(x => x.ToDomainModel()).ToArray());
            return item.ToWebModel();
        }

        [HttpPost]
        [Route("api/item/query")]
        public async Task<QueryWebResponseModel> Query(QueryWebRequestModel model)
        {
            var items = Engine.Query(model.Query).Select(ItemConverters.ToWebModel).ToArray();
            return new QueryWebResponseModel(items);
        }
    }
}
