using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using challenge.Database;
using challenge.Interfaces;
using challenge.Models;

namespace challenge.Repositories
{
    public class ContaRepository : IContaRepository
    {
        private readonly BancoContext _db;
        public ContaRepository(BancoContext db) => _db = db;

        public IQueryable<ContaBancaria> GetAllAsync()
        {
            return _db.ContasBancaria.AsQueryable();
        }

        public ContaBancaria GetById(int id)
        {
            return _db.ContasBancaria.SingleOrDefault(c => c.Conta == id);
        }

        public ContaBancaria DepositarAsync(ContaBancaria conta, Double quantidade)
        {
            if (conta != null)
            {
                conta.Saldo += quantidade;
                _db.ContasBancaria.Update(conta);
                _db.SaveChangesAsync();
            }
            return conta;
        }

        public ContaBancaria SacarAsync(ContaBancaria conta, Double quantidade)
        {
            if (conta != null && conta.Saldo >= quantidade)
            {
                conta.Saldo -= quantidade;
                _db.ContasBancaria.Update(conta);
                _db.SaveChangesAsync();
            }
            return conta;
        }

        public ContaBancaria Save(ContaBancaria conta)
        {
            if (!conta.Conta.HasValue)
            {
                Random random = new Random();
                conta.Conta = random.Next(10000, 99999);
                conta.Saldo = 0;
                _db.ContasBancaria.Add(conta);
            }

            _db.SaveChanges();

            return conta;
        }
    }
}
