
using challenge.Interfaces;
using challenge.Interfaces.Services;
using challenge.Models;

namespace challenge.Services
{
    public class ContaService : IContaService
    {
        private readonly IContaRepository _contaRepository;
        public ContaService(IContaRepository contaRepository)
        {
            _contaRepository = contaRepository;
        }

        public ContaBancaria criarConta()
        {
            return _contaRepository.Save(new ContaBancaria());
        }

        public ContaBancaria depositar(int conta, double valor)
        {
            ContaBancaria contaBancaria = _contaRepository.GetById(conta);
            if (contaBancaria == null)
            {
                throw new GraphQLException("Conta não encontrado.");
            }
            return _contaRepository.DepositarAsync(contaBancaria, valor);
        }

        public ContaBancaria sacar(int conta, double valor)
        {
            ContaBancaria contaBancaria = _contaRepository.GetById(conta);
            if (contaBancaria == null)
                throw new GraphQLException("Conta não encontrado.");
            if (contaBancaria.Saldo < valor)
                throw new GraphQLException("Saldo insuficiente.");
            return _contaRepository.SacarAsync(contaBancaria, valor);
        }
        public ContaBancaria buscar(int conta)
        {
            ContaBancaria contaBancaria = _contaRepository.GetById(conta);
            if (contaBancaria == null)
                throw new GraphQLException("Conta não encontrado.");
            return contaBancaria;
        }
        public List<ContaBancaria> buscarTodos()
        {
            return _contaRepository.GetAllAsync().ToList();
        }
    }
}
