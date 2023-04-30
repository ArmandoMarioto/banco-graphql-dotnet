using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using challenge.Models;

namespace challenge.Interfaces
{
    public interface IContaRepository
    {
        IQueryable<ContaBancaria> GetAllAsync();
        ContaBancaria GetById(int id);
        ContaBancaria DepositarAsync(ContaBancaria conta, Double quantidade);


        ContaBancaria SacarAsync(ContaBancaria conta, Double quantidade);

        ContaBancaria Save(ContaBancaria conta);
    }
}
