using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using TheCodeCamp.Data;

namespace TheCodeCamp.Controllers
{
    
    public class CampsController : ApiController
    {
        private ICampRepository _repository;

        public CampsController(ICampRepository repository)
        {
            _repository = repository;
        }

        public async Task<IHttpActionResult> Get()
        {
            try
            {
                var result = await _repository.GetAllCampsAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                // TODO Add Logging
                return InternalServerError(ex);
                //$"There was an error getting your data {ex.Message}"
            }
        }
    }
}
