using AutoMapper;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Patikadev_RestfulApi.Domain;
using Patikadev_RestfulApi.DTO;
using Patikadev_RestfulApi.Services.Interfaces;
using Patikadev_RestfulApi.Services.Validations;

namespace Patikadev_RestfulApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BookController : ControllerBase
{
    private readonly IBookService _bookService;
    private readonly IMapper _mapper;

    public BookController(IBookService bookService, IMapper mapper)
    {
        _bookService = bookService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<BookResponse>>> GetAllProduct()
    {
        var entities = await _bookService.GetAllBooksAsync();
        var mappedEntity = _mapper.Map<List<BookResponse>>(entities);
        return Ok(mappedEntity);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<BookResponse>> GetProduct(Guid id)
    {
        var entity = await _bookService.GetBookByIdAsync(id);
        var mappedEntity = _mapper.Map<BookResponse>(entity);
        return Ok(mappedEntity);
    }

    [HttpPost]
    public async Task<ActionResult<BookResponse>> Create([FromBody] BookRequest request)
    {
        CreateBookValidator validationRules = new();
        ValidationResult validationResult = validationRules.Validate(request);
       
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var entity = _mapper.Map<Book>(request);
        var mappedEntity = await _bookService.CreateBookAsync(entity);
        var response = _mapper.Map<BookResponse>(mappedEntity);
        return StatusCode(201, response);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<BookResponse>> Update(Guid id, [FromBody] BookRequest request)
    {
        UpdateBookValidator validationRules = new();
        ValidationResult validationResult = validationRules.Validate(request);

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var entity = _mapper.Map<Book>(request);
        var mappedEntity = await _bookService.UpdateBookAsync(id,entity);
        var response = _mapper.Map<BookResponse>(mappedEntity);
        return StatusCode(201, response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var entity = await _bookService.GetBookByIdAsync(id);
        if (entity is null)
            return NotFound();

        await _bookService.DeleteBookAsync(id);
        return Ok(new { message = "Deleted successfully" });
    }
}
