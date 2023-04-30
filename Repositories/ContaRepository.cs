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

        public IQueryable<Conta> GetAllAsync()
        {
            return _db.Contas.AsQueryable();
        }

        public Conta GetById(double id)
        {
            return _db.Contas.SingleOrDefault(c => c.Numero == id);
        }

        public Conta DepositarAsync(Double id, Double quantidade)
        {
            var conta = GetById(id);
            if (conta != null)
            {
                conta.Saldo += quantidade;
                _db.SaveChanges();
            }
            return conta;
        }

        public Conta SacarAsync(Double id, Double quantidade)
        {
            var conta = GetById(id);
            if (conta != null && conta.Saldo >= quantidade)
            {
                conta.Saldo -= quantidade;
                _db.SaveChanges();
            }
            return conta;
        }

        public Conta Save(Conta conta)
        {
            if (!conta.Numero.HasValue)
            {
                Random random = new Random();
                conta.Numero = random.Next(10000, 99999);
                conta.Saldo = 0;
                _db.Contas.Add(conta);
            }

            _db.SaveChanges();

            return conta;
        }
    }
}
