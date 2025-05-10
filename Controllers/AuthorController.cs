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
public class AuthorController : ControllerBase
{
    private readonly IAuthorService _authorService;
    private readonly IMapper _mapper;

    public AuthorController(IAuthorService authorService, IMapper mapper)
    {
        _authorService = authorService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<AuthorResponse>>> GetAllAuthor()
    {
        var entities = await _authorService.GetAllAuthorsAsync();
        var mappedEntity = _mapper.Map<List<AuthorResponse>>(entities);
        return Ok(mappedEntity);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<AuthorResponse>> GetAuthor(Guid id)
    {
        var entity = await _authorService.GetAuthorByIdAsync(id);
        var mappedEntity = _mapper.Map<AuthorResponse>(entity);
        return Ok(mappedEntity);
    }

    [HttpPost]
    public async Task<ActionResult<AuthorResponse>> Create([FromBody] AuthorRequest request)
    {
        CreateAuthorValidator validationRules = new();
        ValidationResult validationResult = validationRules.Validate(request);

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var entity = _mapper.Map<Author>(request);
        var mappedEntity = await _authorService.CreateAuthorAsync(entity);
        var response = _mapper.Map<AuthorResponse>(mappedEntity);
        return StatusCode(201, response);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<AuthorResponse>> Update(Guid id, [FromBody] AuthorRequest request)
    {
        UpdateAuthorValidator validationRules = new();
        ValidationResult validationResult = validationRules.Validate(request);

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var entity = _mapper.Map<Author>(request);
        var mappedEntity = await _authorService.UpdateAuthorAsync(id, entity);
        var response = _mapper.Map<AuthorResponse>(mappedEntity);
        return StatusCode(201, response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var entity = await _authorService.GetAuthorByIdAsync(id);
        await _authorService.DeleteAuthorAsync(id);
        return Ok(new { message = "Deleted successfully" });
    }
}
