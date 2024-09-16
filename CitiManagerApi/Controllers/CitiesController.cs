using AutoMapper;
using CitiManagerApi.Data.Abstract;
using CitiManagerApi.Dtos;
using CitiManagerApi.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CitiManagerApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CitiesController : ControllerBase
    {
        private readonly IAppRepository _appRepository;
        private readonly IMapper _mapper;
        public CitiesController(IAppRepository appRepository, IMapper mapper)
        {
            _appRepository = appRepository;
            _mapper = mapper;
        }

        // GET: api/<CitiesController>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCities(int id)
        {
            //var dtos = (await _appRepository.GetCitiesAsync(id))
            //    .Select(c =>
            //    {
            //        return new CityForListDto
            //        {
            //            Name = c.Name,
            //             Description = c.Description,
            //              Id = c.Id,
            //               PhotoUrl=c.CityImages.FirstOrDefault(p=>p.IsMain)?.Url
            //        };
            //    });
            var items=await _appRepository.GetCitiesAsync(id);
            var dtos = _mapper.Map<IEnumerable<CityForListDto>>(items);
            return Ok(dtos);
        }

        [HttpGet()]
        public async Task<IActionResult> GetCities()
        {
            //var dtos = (await _appRepository.GetCitiesAsync(id))
            //    .Select(c =>
            //    {
            //        return new CityForListDto
            //        {
            //            Name = c.Name,
            //             Description = c.Description,
            //              Id = c.Id,
            //               PhotoUrl=c.CityImages.FirstOrDefault(p=>p.IsMain)?.Url
            //        };
            //    });
            var items = await _appRepository.GetAllAsync();
            var dtos = _mapper.Map<List<CityForListDto>>(items);
            return Ok(dtos);
        }

        [HttpPost("Add")]
        public async Task<IActionResult> Add([FromBody]CityDto dto)
        {
            var entity = _mapper.Map<City>(dto);
            await _appRepository.AddAsync(entity);
            await _appRepository.SaveAllAsync();
            var returnedDto = _mapper.Map<CityDto>(entity);
            return Ok(returnedDto);
        }


    }
}
