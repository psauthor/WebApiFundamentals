using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using TheCodeCamp.Data;
using TheCodeCamp.Models;

namespace TheCodeCamp.Controllers
{
    [RoutePrefix("api/camps")]
    public class CampsController : ApiController
    {
        private ICampRepository _repository;
        private IMapper _mapper;

        public CampsController(ICampRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [Route()]
        public async Task<IHttpActionResult> Get()
        {
            try
            {
                var result = await _repository.GetAllCampsAsync();

                //Mapping
                var mappedResult = _mapper.Map<IEnumerable<CampModel>>(result);

                return Ok(mappedResult);
            }
            catch (Exception ex)
            {
                // TODO Add Logging
                return InternalServerError(ex);
                //$"There was an error getting your data {ex.Message}"
            }
        }

        [Route("moniker")]
        public async Task<IHttpActionResult> Get(string moniker)
        {
            try
            {
                var result = await _repository.GetCampAsync(moniker);

                return Ok(_mapper.Map<CampModel>(result));
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
