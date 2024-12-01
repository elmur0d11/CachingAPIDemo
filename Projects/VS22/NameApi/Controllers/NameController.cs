using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NameApi.Data;
using NameApi.Dtos;
using NameApi.Models;
using NameApi.Services;
using System.Xml.Linq;

namespace NameApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NameController : ControllerBase
    {
        private readonly INameRepository _nameRepository;
        private readonly IMapper _mapper;
        private readonly ICacheService _cacheService;
        private readonly AppDbContext _context;

        public NameController(INameRepository nameRepository, IMapper mapper, AppDbContext context, ICacheService cacheService)
        {
            _mapper = mapper;
            _nameRepository = nameRepository;
            _context = context;
            _cacheService = cacheService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateName(NameCreatedDto nameCreatedDto)
        {
            var nameModel = _mapper.Map<Name>(nameCreatedDto);
            await _nameRepository.CreateProduct(nameModel);
            await _nameRepository.SaveChangesAsync();

            var nameReadDto = _mapper.Map<NameReadDto>(nameModel);

            return Created("", nameModel);
        }

        [HttpGet("All")]
        public async Task<IActionResult> GetNames()
        {
            try
            {
                var cacheNames = _cacheService.GetData<IEnumerable<Name>>("names");

                if (cacheNames != null)
                {
                    return Ok(_mapper.Map<IEnumerable<NameReadDto>>(cacheNames));
                }

                var names = await _context.Names.ToListAsync();

                var expiryTime = DateTime.UtcNow.AddMinutes(2);
            
                _cacheService.SetData<IEnumerable<Name>>("names", names, expiryTime);

                return Ok(_mapper.Map<IEnumerable<NameReadDto>>(names));

            }
            catch (Exception ex)
            {
                return BadRequest($"{ex} - Application chichvordi. This is Uzbekistan");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetName(int id)
        {
            try
            {
                var cacheKey = $"name_{id}";

                var cachedName = _cacheService.GetData<Name>(cacheKey);
                if (cachedName != null)
                {
                    return Ok(_mapper.Map<NameReadDto>(cachedName));
                }

                var name = await _nameRepository.Get(id);
                if (name == null)
                    return NotFound();

                var expiryTime = DateTimeOffset.Now.AddMinutes(2);
                _cacheService.SetData(cacheKey, name, expiryTime);

                return Ok(_mapper.Map<NameReadDto>(name));
            }
            catch (Exception ex)
            {
                return BadRequest("ERROR: This is Uzbekistan");
            }
        }

    }
}
