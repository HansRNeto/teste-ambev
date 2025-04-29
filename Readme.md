## ⚠️ Aplicação Automática de Migrations

Este projeto está configurado para aplicar automaticamente as migrations pendentes do Entity Framework Core ao iniciar a aplicação.

### Como funciona?

Durante a inicialização do projeto, o `Program.cs` executa a verificação e aplicação das migrations que ainda não foram aplicadas ao banco de dados.

Isso garante que o schema da base de dados esteja sempre atualizado com o modelo da aplicação, evitando a necessidade de rodar comandos manuais como:

```bash
dotnet ef database update
```

### Quando isso acontece?
Sempre que a aplicação for iniciada (em ambiente local ou via Docker), as migrations pendentes serão aplicadas automaticamente.

### Requisitos
- O banco de dados precisa estar acessível no momento da inicialização da aplicação.
- As migrations devem ter sido previamente criadas

# 🚀 Como Executar o Projeto

Este projeto pode ser executado tanto usando Docker quanto diretamente via `dotnet run`. Abaixo, explicamos como executar o projeto em ambas as opções.

## 🐳 Executando com Docker
#### ⚠️ Este projeto está configurado para rodar facilmente com Docker. Ao iniciar os containers, tanto a aplicação quanto o banco de dados PostgreSQL são automaticamente levantados e conectados, com todas as configurações já definidas na aplicação.⚠️

### Pré-requisitos

Antes de começar, verifique se você tem os seguintes softwares instalados na sua máquina:

- [Docker](https://www.docker.com/)
- [Docker Compose](https://docs.docker.com/compose/)

### Passos para Executar via Docker

### 1. **Clone o repositório** (caso ainda não tenha clonado):

```bash
git clone https://github.com/HansRNeto/teste-ambev.git
```
   
### 2. **Suba os containers com Docker Compose**:
Na Raiz do projeto execute o seguinte comando:
```bash
docker compose up --build -d
```
- Obs.: Caso queira manter o terminal para interação no container, remove o parametro "-d"

### 3. **Acesse a aplicação**:

Após o Docker iniciar os serviços, a aplicação estará disponível em:
   
- https://localhost:8081/swagger/index.html (com HTTPS)
- http://localhost:8080/swagger/index.html (com HTTP)

## 🖥️ Executando Localmente (Sem Docker)

### Pré-requisitos

Antes de começar, verifique se você tem os seguintes softwares instalados na sua máquina:

- [SDK do .NET 7.0 ou superior](https://dotnet.microsoft.com/download/dotnet)
- [Banco de Dados PostgreSQL](https://www.postgresql.org/download/)

### Passos para Executar Localmente

1. ### **Clone o repositório**:

Caso ainda não tenha clonado o repositório, execute o seguinte comando:

```bash
git clone https://github.com/HansRNeto/teste-ambev.git
```
   
2. ### Instale as dependências:

Execute o comando abaixo para restaurar as dependências do projeto:
```bash
dotnet restore
```

3. ### Configure a string de conexão com o banco de dados:

No arquivo appsettings.json, configure a string de conexão do PostgreSQL.
    
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Port=5432;Username=seu_usuario;Password=sua_senha;Database=seu_banco"
  }
}
```

Certifique-se de ter o PostgreSQL instalado e rodando na sua máquina.
4. ### Executar a aplicação:

Agora, execute o projeto localmente com o seguinte comando:

```bash
dotnet run
```

O servidor estará disponível em:
- https://localhost:7181/swagger/index.html (com HTTPS)
- http://localhost:5119/swagger/index.html (com HTTP)

## ✅ Execução dos Testes

Este projeto contém testes automatizados para garantir a qualidade e o bom funcionamento do código.

### Como executar os testes

Para rodar os testes localmente, siga os passos abaixo:

1. Certifique-se de que todas as dependências estejam instaladas e o projeto compilado:
```bash
dotnet build
```
   
2. Execute os testes com o seguinte comando:
```bash
dotnet test
```
   
### Detalhes
- Os testes estão localizados no diretório tests/
- É possível rodar testes de forma mais detalhada com:
```bash
dotnet test --logger "console;verbosity=detailed"
```

### 🧪 Observações
- Certifique-se de que o banco de dados de teste esteja configurado corretamente, se os testes dependerem dele.
- Para garantir testes isolados e confiáveis, recomenda-se o uso de banco em memória (InMemory) ou mocks para dependências externas.

## 📝 Curls dos Endpoints para Testes

Aqui estão alguns exemplos de **CURL** para testar os principais endpoints da aplicação. Você pode rodá-los diretamente no terminal para simular requisições HTTP à API.

### 1. **Produtos**

- Criar Produto
```bash
curl --location 'https://localhost:8081/api/Products' \
--header 'accept: text/plain' \
--header 'Content-Type: application/json' \
--data '{
  "name": "Pack Skol 6 unidades",
  "description": "Pack com 6 latas de 350ml de Skol Pilsen",
  "price": 24.90
}'
```

- Listar Produtos
```bash
curl --location 'https://localhost:8081/api/Products?pageNumber=1&pageSize=25' \
--header 'accept: text/plain'
```
- Pegar Produto
```bash
curl --location 'https://localhost:8081/api/Products/4bbc8767-1ee4-43d3-a0a6-17d78d1ddadd' \
--header 'accept: text/plain'
```
- Atualizar Produto
```bash
curl --location --request PUT 'https://localhost:8081/api/Products/4bbc8767-1ee4-43d3-a0a6-17d78d1ddadd' \
--header 'accept: text/plain' \
--header 'Content-Type: application/json' \
--data '{
    "name": "Copo Long Drink Brahma",
    "description": "Copo acrílico de 300ml com logo Brahma impresso",
    "price": 9.90,
    "isActive": true
}'
```
- Excluir Produto
```bash
curl --location --request DELETE 'https://localhost:8081/api/Products/0f964a5d-fc0a-4693-90b1-c0c588e3bf9d' \
--header 'accept: text/plain'
```

### 2. **Clientes**

- Criar Cliente
```bash
curl --location 'https://localhost:8081/api/Customers' \
--header 'accept: text/plain' \
--header 'Content-Type: application/json' \
--data-raw '{
  "name": "Cliente I",
  "email": "cliente.one@gmail.com"
}'
```

- Listar Clientes
```bash
curl --location 'https://localhost:8081/api/Customers?pageNumber=1&pageSize=25' \
--header 'accept: text/plain'
```
- Pegar Cliente
```bash
curl --location 'https://localhost:8081/api/Customers/{id}' \
--header 'accept: text/plain'
```
- Atualizar Cliente
```bash
curl --location --request PUT 'https://localhost:8081/api/Customers/{id}' \
--header 'accept: text/plain' \
--header 'Content-Type: application/json' \
--data-raw '{
    "name": "Cliente I",
    "email": "cliente.one@gmail.com",
    "isActive": true
}'
```
- Excluir Cliente
```bash
curl --location --request DELETE 'https://localhost:8081/api/Products/{id}' \
--header 'accept: text/plain'
```

### 3. **Filiais**

- Criar Filial
```bash
curl --location 'https://localhost:8081/api/Branchs' \
--header 'accept: text/plain' \
--header 'Content-Type: application/json' \
--data '{
  "name": "Filial I",
  "address": "Rua das Palmeiras, 123 - Centro, São Paulo - SP"
}'
```

- Listar Filias
```bash
curl --location 'https://localhost:8081/api/Branchs?pageNumber=1&pageSize=25' \
--header 'accept: text/plain'
```
- Pegar Filial
```bash
curl --location 'https://localhost:8081/api/Branchs/{id}' \
--header 'accept: text/plain'
```
- Atualizar Filial
```bash
curl --location --request PUT 'https://localhost:8081/api/Branchs/{id}' \
--header 'accept: text/plain' \
--header 'Content-Type: application/json' \
--data '{
  "name": "Filial I",
  "address": "Rua das Palmeiras, 123 - Centro, São Paulo - SP",
  "isActive": true
}'
```
- Excluir Filial
```bash
curl --location --request DELETE 'https://localhost:8081/api/Branchs/{id}' \
--header 'accept: text/plain'
```

### 4. **Vendas**

- Criar Venda
```bash
curl --location 'https://localhost:8081/api/Sales' \
--header 'accept: text/plain' \
--header 'Content-Type: application/json' \
--data '{
  "saleNumber": "001",
  "customerId": "{customerIds}",
  "customerName": "{customerName}",
  "branchId": "{branchId}",
  "branchName": "{branchName}",
  "saleItems": [
    {
      "productId": "{productId}",
      "productName": {productName},
      "quantity": 3
    }
  ]
}'
```

- Listar Vendas
```bash
curl --location 'https://localhost:8081/api/Sales?pageNumber=1&pageSize=25' \
--header 'accept: text/plain'
```
- Pegar Venda
```bash
curl --location 'https://localhost:8081/api/Sales/{id}' \
--header 'accept: text/plain'
```
- Atualizar Venda
```bash
curl --location --request PUT 'https://localhost:8081/api/Sales/{id}' \
--header 'accept: text/plain' \
--header 'Content-Type: application/json' \
--data '{
  "saleNumber": "002",
  "customerId": "{customerId}",
  "customerName": "{customerName}",
  "branchId": "{branchId}",
  "branchName": "{branchName}",
  "isCancelled": false
}'
```
- Excluir Venda
```bash
curl --location --request DELETE 'https://localhost:8081/api/Sales/{id}' \
--header 'accept: text/plain'
```

### 🧪 Observação
- Todos os valores são representativos, sendo necessário adequa-los para o seu ambiente de desenvolvimento.

## Para mais informações acesse o swagger
- https://localhost:8081/swagger/index.html (DOCKER)
- https://localhost:7181/swagger/index.html (LOCAL)