using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    public class DesignController : ControllerBase
    {
        [HttpGet]
        [Route("api/design/{code}")]
        public async Task Create(string code)
        {
        }

        /*[HttpPost]
        [Route("api/design")]
        public DesignCreateWebResponseModel Create(DesignCreateWebRequestModel model)
        {
        }

        [HttpPut]
        [Route("api/design/{code}")]
        public async Task<DesignEditWebResponseModel> Edit(string code, DesignEditWebRequestModel model)
        {
        }

        [HttpDelete]
        [Route("api/design/{code}")]
        public async Task Delete(string code)
        {
        }

        [HttpPost]
        [Route("api/design/{code}/attribute")]
        public async Task<AddAttributeWebResponseModel> AddAttribute(string code, AddAttributeWebRequestModel model)
        {
        }

        [HttpPut]
        [Route("api/design/{designCode}/attribute/{code}")]
        public async Task<EditAttributeWebResponseModel> EditAttribute(string designCode, string code,
            EditAttributeWebRequestModel model)
        {
        }

        [HttpDelete]
        [Route("api/design/{designCode}/attribute/{code}")]
        public async Task<RemoveAttributeWebResponseModel> RemoveAttribute(string designCode, string code,
            RemoveAttributeWebRequestModel model)
        {
        }*/
    }
}