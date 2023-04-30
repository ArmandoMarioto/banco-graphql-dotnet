using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using challenge.Models;

namespace challenge.Interfaces
{
    public interface IContaRepository
    {
        IQueryable<Conta> GetAllAsync();
        Conta GetById(Double id);
        Conta DepositarAsync(Double id, Double quantidade);


        Conta SacarAsync(Double id, Double quantidade);

        Conta Save(Conta entity);
    }
}
