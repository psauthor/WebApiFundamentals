using AutoMapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using TheCodeCamp.Data;
using TheCodeCamp.Models;

namespace TheCodeCamp.Controllers
{
    [RoutePrefix("api/camps")]
    public class CampsController : ApiController
    {
        private readonly ICampRepository _campRepository;
        private readonly IMapper _mapper;

        public CampsController(ICampRepository campRepository, IMapper mapper)
        {
            _campRepository = campRepository;
            _mapper = mapper;
        }

        [Route()]
        public async Task<IHttpActionResult> Get(bool includeTalks = false)
        {
            try
            {
                var result = await _campRepository.GetAllCampsAsync(includeTalks);

                var mappedResult = _mapper.Map<IEnumerable<CampModel>>(result);

                return Ok(mappedResult);
            }
            catch (Exception ex)
            {
                //TODO: Add logging
                return InternalServerError(ex);
            }
        }

        [Route("{moniker}")]
        public async Task<IHttpActionResult> Get(string moniker)
        {
            try
            {
                var result = await _campRepository.GetCampAsync(moniker);

                if (result == null)
                {
                    return NotFound();
                }

                return Ok(_mapper.Map<CampModel>(result));
            }
            catch (Exception ex)
            {
                //TODO: Add logging
                return InternalServerError(ex);
            }
        }

        [Route("SearchByDate/{eventDate:datetime}")]
        [HttpGet]
        public async Task<IHttpActionResult> SearchByDate(DateTime eventDate, bool includeTalks = false)
        {
            try
            {
                var result = await _campRepository.GetAllCampsByEventDate(eventDate, includeTalks);

                if (result == null)
                {
                    return NotFound();
                }

                return Ok(_mapper.Map<IEnumerable<CampModel>>(result));
            }
            catch (Exception ex)
            {
                //TODO: Add logging
                return InternalServerError(ex);
            }
        }
    }
}
