using Microsoft.AspNetCore.Mvc;
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
}