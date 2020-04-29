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

        [Route("{moniker}", Name = "GetCamp")]
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

        [Route()]
        public async Task<IHttpActionResult> Post(CampModel model)
        {
            try
            {
                if (await _campRepository.GetCampAsync(model.Moniker) != null)
                {
                    ModelState.AddModelError("Moniker", "Moniker in use.");
                }
                if (ModelState.IsValid)
                {
                    var camp = _mapper.Map<Camp>(model);

                    _campRepository.AddCamp(camp);

                    if (await _campRepository.SaveChangesAsync())
                    {
                        var newModel = _mapper.Map<CampModel>(camp);
                        return CreatedAtRoute("GetCamp", new { moniker = newModel.Moniker }, newModel);
                    }
                    else
                    {
                        return InternalServerError();
                    }
                }
            }
            catch (Exception ex)
            {
                //TODO: Add logging
                return InternalServerError(ex);
            }
            return BadRequest(ModelState);
        }

        [Route("{moniker}")]
        public async Task<IHttpActionResult> Put(string moniker, CampModel model)
        {
            try
            {
                var camp = await _campRepository.GetCampAsync(moniker);
                if (camp == null)
                    return NotFound();

                _mapper.Map(model, camp);

                if (await _campRepository.SaveChangesAsync())
                {
                    return Ok(_mapper.Map<CampModel>(camp));
                }
                else
                {
                    return InternalServerError();
                }
            }
            catch (Exception ex)
            {
                //TODO: Add logging
                return InternalServerError(ex);
            }
        }

        [Route("{moniker}")]
        public async Task<IHttpActionResult> Delete(string moniker)
        {
            try
            {
                var camp = await _campRepository.GetCampAsync(moniker);
                if (camp == null)
                    return NotFound();

                _campRepository.DeleteCamp(camp);
                if (await _campRepository.SaveChangesAsync())
                    return Ok();
                else
                    return InternalServerError();
            }
            catch (Exception ex)
            {
                //TODO: Add logging
                return InternalServerError(ex);
            }
        }
    }
}
