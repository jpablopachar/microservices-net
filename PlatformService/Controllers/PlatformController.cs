using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PlatformService.Data.Platforms;
using PlatformService.Dtos;
using PlatformService.Models;

namespace PlatformService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlatformController : ControllerBase
    {
        private readonly IPlatformRepository _repository;
        private IMapper _mapper;

        public PlatformController(IPlatformRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PlatformResponseDto>>> GetPlatforms()
        {
            var platforms = await _repository.GetAllPlatforms();

            return Ok(_mapper.Map<IEnumerable<PlatformResponseDto>>(platforms));
        }

        [HttpGet("{id}", Name = "GetPlatformById")]
        public async Task<ActionResult<PlatformResponseDto>> GetPlatformById(int id)
        {
            var platform = await _repository.GetPlatformById(id);

            if (platform is null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<PlatformResponseDto>(platform));
        }

        [HttpPost]
        public async Task<ActionResult<PlatformResponseDto>> CreateProperty([FromBody] PlatformRequestDto platformRequestDto)
        {
            var platform = _mapper.Map<Platform>(platformRequestDto);

            await _repository.CreatePlatform(platform);
            await _repository.SaveChanges();

            var platformResponseDto = _mapper.Map<PlatformResponseDto>(platform);

            return CreatedAtRoute(nameof(GetPlatformById), new { platformResponseDto.Id }, platformResponseDto);
        }
    }
}