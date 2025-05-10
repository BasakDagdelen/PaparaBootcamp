using AutoMapper;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Patikadev_RestfulApi.Domain;
using Patikadev_RestfulApi.DTO.Request;
using Patikadev_RestfulApi.DTO.Response;
using Patikadev_RestfulApi.Services.Interfaces;
using Patikadev_RestfulApi.Services.Validations;

namespace Patikadev_RestfulApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class GenreController : ControllerBase
{
    private readonly IGenreService _genreService;
    private readonly IMapper _mapper;

    public GenreController(IGenreService genreService, IMapper mapper)
    {
        _genreService = genreService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<GenreResponse>>> GetAllGenre()
    {
        var entities = await _genreService.GetAllGenreAsync();
        var mappedEntity = _mapper.Map<List<GenreResponse>>(entities);
        return Ok(mappedEntity);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<GenreResponse>> GetGenre(Guid id)
    {
        var entity = await _genreService.GetGenreByIdAsync(id);
        var mappedEntity = _mapper.Map<GenreResponse>(entity);
        return Ok(mappedEntity);
    }

    [HttpPost]
    public async Task<ActionResult<GenreResponse>> Create([FromBody] GenreRequest request)
    {
        CreateGenreValidatior validationRules = new();
        ValidationResult validationResult = validationRules.Validate(request);

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var entity = _mapper.Map<Genre>(request);
        var mappedEntity = await _genreService.CreateGenreAsync(entity);
        var response = _mapper.Map<GenreResponse>(mappedEntity);
        return StatusCode(201, response);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<GenreResponse>> Update(Guid id, [FromBody] GenreRequest request)
    {
        UpdateGenreValidator validationRules = new();
        ValidationResult validationResult = validationRules.Validate(request);

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var entity = _mapper.Map<Genre>(request);
        var mappedEntity = await _genreService.UpdateGenreAsync(id, entity);
        var response = _mapper.Map<GenreResponse>(mappedEntity);
        return StatusCode(201, response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var entity = await _genreService.GetGenreByIdAsync(id);
        if (entity is null)
            return NotFound();

        await _genreService.DeleteGenreAsync(id);
        return Ok(new { message = "Deleted successfully" });
    }
}
