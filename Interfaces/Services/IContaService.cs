using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using challenge.Models;

namespace challenge.Interfaces.Services
{
    public interface IContaService
    {
        ContaBancaria criarConta();
        ContaBancaria sacar(int conta, double valor);
        ContaBancaria depositar(int conta, double valor);
        ContaBancaria buscar(int conta);
        List<ContaBancaria> buscarTodos();
    }
}
