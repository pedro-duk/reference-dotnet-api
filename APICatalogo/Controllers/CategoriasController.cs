using APICatalogo.Context;
using APICatalogo.Filters;
using APICatalogo.Models;
using APICatalogo.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Controllers;

[Route("[controller]")]
[ApiController]
public class CategoriasController : ControllerBase
{
    private readonly ICategoriaRepository _repository;
    private readonly ILogger<CategoriasController> _logger;

    public CategoriasController(ICategoriaRepository repository, ILogger<CategoriasController> logger/*, IConfiguration configuration*/)
    {
        _repository = repository;
        _logger = logger;
        // _configuration = configuration;
    }

    // Example of how to read configuration files by injecting the IConfiguration dependence
    // [HttpGet("LerArquivoConfiguracao")]
    // public string GetValores()
    // {
    //     var valor1 = _configuration["chave1"];
    //     var valor2 = _configuration["chave2"];

    //     var secao1 = _configuration["secao1:chave2"];

    //      return $"Chave1 = {valor1}  \nChave2 = {valor2}  \nSeção1 => Chave2 = {secao1}";
    // }

    [HttpGet]
    public ActionResult<IEnumerable<Categoria>> Get()
    {
        var categorias = _repository.GetAll();
        return Ok(categorias);
    }

    // {id:int} restricts the routing
    //      If a non-integer is passed, this method is not invoked
    //      If no valid routes are found, 404 is returned
    //      This is better used to distinguish between two similar routes rather than for validation.
    [HttpGet("{id:int}", Name = "ObterCategoria")]
    // ActionResult<Categoria> allows the method to return any ActionResult and also any object of Categoria.
    public ActionResult<Categoria> Get(int id)
    {
        var categoria = _repository.Get(c => c.CategoriaId == id);

        if (categoria == null)
        {
            _logger.LogWarning($"Categoria com id= {id} não encontrada...");
            return NotFound($"Categoria com id= {id} não encontrada...");
        }
        return Ok(categoria);
    }


    // Model Binding maps data from request to action parameters.
    //      Request Body is bound automatically to Categoria. An explicit bound can be made with [FromBody]
    //      Query strings and path params are also bound automatically (as long as they have the same name)
    //      [BindRequired] makes the response return 400 if binding cannot occur
    //      [BindNever] informs the model binder to never link one property to a parameter - used mostly on models
    [HttpPost]
    public ActionResult Post(Categoria categoria)
    {
        if (categoria is null)
        {
            _logger.LogWarning($"Dados inválidos...");
            return BadRequest("Dados inválidos");
        }

        var categoriaCriada = _repository.Create(categoria);

        return new CreatedAtRouteResult("ObterCategoria", new { id = categoriaCriada.CategoriaId }, categoriaCriada);
    }

    // Example of usage of the ApiLoggingFilter
    // Filter provider uses the given class to find the filter
    [ServiceFilter(typeof(ApiLoggingFilter))]
    [HttpPut("{id:int}")]
    public ActionResult Put(int id, Categoria categoria)
    {
        if (id != categoria.CategoriaId)
        {
            _logger.LogWarning($"Dados inválidos...");
            return BadRequest("Dados inválidos");
        }

        _repository.Update(categoria);
        return Ok(categoria);
    }

    [HttpDelete("{id:int}")]
    public ActionResult Delete(int id)
    {
        var categoria = _repository.Get(c => c.CategoriaId == id);

        if (categoria == null)
        {
            _logger.LogWarning($"Categoria com id={id} não encontrada...");
            return NotFound($"Categoria com id={id} não encontrada...");
        }

        var categoriaExcluida = _repository.Delete(categoria);

        return Ok(categoriaExcluida);
    }
}