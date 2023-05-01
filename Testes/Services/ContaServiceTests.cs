using System.Linq;
using challenge.Models;
using challenge.Testes;
using Xunit;

namespace challenge.Testes.Services
{
    [Collection("ContaServiceTests")]
    public class ContaServiceTests
    {
        private readonly ContaServiceTestsFixture _teste;

        public ContaServiceTests(ContaServiceTestsFixture teste)
        {
            _teste = teste;
        }

        [Fact]
        public void CriarConta()
        {
            var conta = _teste.ContaService.criarConta();

            Assert.NotNull(conta);
            Assert.NotEqual(0, conta.Conta);
            Assert.Equal(0, conta.Saldo);
        }

        [Fact]
        public void Depositar()
        {
            var conta = _teste.ContaService.criarConta();

            var deposito = _teste.ContaService.depositar(conta.Conta ?? 0, 100);

            Assert.Equal(100, deposito.Saldo);
        }

        [Fact]
        public void Sacar()
        {
            var conta = _teste.ContaService.criarConta();
            _teste.ContaService.depositar(conta.Conta ?? 0, 100);

            var saque = _teste.ContaService.sacar(conta.Conta ?? 0, 50);

            Assert.Equal(50, saque.Saldo);
        }

        [Fact]
        public void Sacar_SaldoInsuficiente()
        {
            var conta = _teste.ContaService.criarConta();

            var exception = Assert.Throws<GraphQLException>(() => _teste.ContaService.sacar(conta.Conta ?? 0, 50));

            Assert.Equal("Saldo insuficiente.", exception.Message);
        }

        [Fact]
        public void Buscar()
        {
            var conta = _teste.ContaService.criarConta();

            var result = _teste.ContaService.buscar(conta.Conta ?? 0);

            Assert.Equal(conta.Conta, result.Conta);
            Assert.Equal(conta.Saldo, result.Saldo);
        }

        [Fact]
        public void BuscarTodos()
        {
            _teste.Dispose();
            _teste.ContaService.criarConta();
            _teste.ContaService.criarConta();

            var result = _teste.ContaService.buscarTodos();

            Assert.Equal(2, result.Count());
        }
    }
}
