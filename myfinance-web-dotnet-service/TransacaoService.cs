using Microsoft.EntityFrameworkCore;
using myfinance_web_dotnet_domain.Entities;
using myfinance_web_dotnet_infra;
using myfinance_web_dotnet_service.Interfaces;

namespace myfinance_web_dotnet_service;

public class TransacaoService : ITransacaoService
{
    private readonly MyFinanceDbContext _dbContext;
    
    public TransacaoService(MyFinanceDbContext dbContext)
    {
        dbContext = dbContext;
    }

    public void Cadastrar(Transacao entidade)
    {
        var dbSet = _dbContext.Transacao;

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
        var planoConta = new Transacao{Id = id};
        _dbContext.Attach(planoConta);
        _dbContext.Remove(planoConta);
        _dbContext.SaveChanges();
    }

    public List<Transacao> ListarRegistros()
    {
        var dbSet = _dbContext.Transacao;
        return dbSet.ToList();
    }

    public Transacao RetornarRegistro(int id)
    {
        return _dbContext.Transacao.FirstOrDefault(x => x.Id == id);
    }
}