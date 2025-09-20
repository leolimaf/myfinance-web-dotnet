using Microsoft.AspNetCore.Mvc;
using myfinance_web_dotnet_domain.Entities;
using myfinance_web_dotnet_service.Interfaces;
using myfinance_web_dotnet.Models;

namespace myfinance_web_dotnet.Controllers;

[Route("[controller]")]
public class PlanoContaController : Controller
{
    private readonly ILogger<PlanoContaController> _logger;
    private readonly IPlanoContaService _planoContaService;

    public PlanoContaController(ILogger<PlanoContaController> logger, IPlanoContaService planoContaService)
    {
        _logger = logger;
        _planoContaService = planoContaService;
    }
    
    [HttpGet]
    [Route("[action]")]
    public IActionResult Index()
    {
        var planoContas = _planoContaService.ListarRegistros();
        var planoContaModels = new List<PlanoContaModel>();

        foreach (var planoConta in planoContas)
        {
            var itemPlanoConta = new PlanoContaModel
            {
                Id = planoConta.Id,
                Descricao = planoConta.Descricao,
                Tipo = planoConta.Tipo
            };
            planoContaModels.Add(itemPlanoConta);
        }

        ViewBag.PlanoContas = planoContaModels;
        
        return View();
    }
    
    [HttpGet]
    [Route("[action]")]
    [Route("[action]/{Id}")]
    public IActionResult Cadastrar(int? id)
    {
        if (id == null) 
            return View();
        
        var planoConta = _planoContaService.RetornarRegistro(Convert.ToInt32(id));
        var planoContaModel = new PlanoContaModel
        {
            Id = planoConta.Id,
            Descricao = planoConta.Descricao,
            Tipo = planoConta.Tipo
        };
        
        return View(planoContaModel);
    }
    
    [HttpPost]
    [Route("[action]")]
    [Route("[action]/{Id}")]
    public IActionResult Cadastrar(PlanoContaModel planoContaModel)
    {
        var planoConta = new PlanoConta
        {
            Id = planoContaModel.Id,
            Descricao = planoContaModel.Descricao,
            Tipo = planoContaModel.Tipo
        };
        
        _planoContaService.Cadastrar(planoConta);
        
        return RedirectToAction("Index");
    }
    
    [HttpGet]
    [Route("[action]/{Id}")]
    public IActionResult Excluir(int? id)
    {
        _planoContaService.Excluir(Convert.ToInt32(id));
        
        return RedirectToAction("Index");
    }
}