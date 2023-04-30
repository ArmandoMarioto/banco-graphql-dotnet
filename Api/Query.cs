using System;

using challenge.Interfaces.Services;
using challenge.Models;

namespace challenge.Api
{
    public class Query
    {
        public ContaBancaria getConta([Service] IContaService service, int conta)
        {
            return service.buscar(conta);
        }
        public List<ContaBancaria> getAllConta([Service] IContaService service)
        {
            return service.buscarTodos();
        }
    }
}
