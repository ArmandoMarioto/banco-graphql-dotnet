# banco-graphql-dotnet

## Para começar

O primeiro passo é fazer um clone do projeto deste repositório. 
```
git clone https://github.com/ArmandoMarioto/banco-graphql-dotnet.git

e

cd .\banco-graphql-dotnet\
```
É necessario ter instalado o docker em sua maquina.
- https://docs.docker.com/desktop/install/windows-install/

Após ter clonado o repositório e ter o docker instalado, navegue até a pasta do projeto e rode os seguintes comandos:

- Essa parte pode demorar um pouco para os comdados finalizarem.
```
docker-compose build
```
Após finalizar rode:
```
docker-compose up
```

Se tudo ocorrer corretamente os dois container(App e o mysql) vão está rodando.

Agora basta acessar

- http://localhost:8080/graphql/

## Exemplos de como usar a API
```
# Mutation para criar uma conta.
mutation CriarConta{
  upsertConta  {
    conta
    saldo
  }
}
# Mutation para depositar um valor em uma conta.
mutation Depositar{
  depositar (conta: 58747, valor: 140.9) {
    conta
    saldo
  }
}
# Mutation para sacar um valor de uma conta.
mutation Sacar{
  sacar(conta: 58747, valor: 140) {
    conta
    saldo
  }
}
# Query para buscar uma conta/saldo passando o numero da conta.
query Buscar{
  getConta (conta: 26031){
    conta
    saldo
  }
}
# Query para buscar todas as contas.
query BuscarTodos{
  getAllConta {
    conta
    saldo
  }
}
```
