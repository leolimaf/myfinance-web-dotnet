using Microsoft.EntityFrameworkCore;
using myfinance_web_dotnet_domain.Entities;
using myfinance_web_dotnet_infra;
using myfinance_web_dotnet_service.Interfaces;

namespace myfinance_web_dotnet_service;

public class PlanoContaService : IPlanoContaService
{
    private readonly MyFinanceDbContext _dbContext;
    
    public PlanoContaService(MyFinanceDbContext dbContext)
    {
        dbContext = dbContext;
    }

    public void Cadastrar(PlanoConta entidade)
    {
        var dbSet = _dbContext.PlanoConta;

        if (entidade == null)
        {
            dbSet.Add(entidade);
        }
        else
        {
            dbSet.Attach(entidade);
            _dbContext.Entry(entidade).State = EntityState.Modified;
        }

        _dbContext.SaveChanges();
    }

    public void Excluir(int id)
    {
        var planoConta = new PlanoConta{Id = id};
        _dbContext.Attach(planoConta);
        _dbContext.Remove(planoConta);
        _dbContext.SaveChanges();
    }

    public List<PlanoConta> ListarRegistros()
    {
        var dbSet = _dbContext.PlanoConta;
        return dbSet.ToList();
    }

    public PlanoConta RetornarRegistro(int id)
    {
        return _dbContext.PlanoConta.FirstOrDefault(x => x.Id == id);
    }
}