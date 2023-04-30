
using challenge.Interfaces.Services;
using challenge.Models;

namespace challenge.Api
{
    public class Mutation
    {
        public ContaBancaria UpsertConta([Service] IContaService service)
        {
            return service.criarConta();
        }
        public ContaBancaria depositar([Service] IContaService service, int conta, double valor)
        {
            return service.depositar(conta, valor);
        }
        public ContaBancaria sacar([Service] IContaService service, int conta, double valor)
        {
            return service.sacar(conta, valor);
        }
    }
}
