# Ambev Developer Evaluation

## Descrição
Este é um projeto de avaliação para desenvolvedores, onde será implementada uma API para gerenciamento de vendas, seguindo boas práticas de arquitetura como **DDD (Domain-Driven Design)**, **Repository Pattern**, e testes automatizados.

## Tecnologias Utilizadas
- **.NET 6+** (ASP.NET Web API)
- **Entity Framework Core** (ORM)
- **PostgreSQL** (Banco de Dados)
- **xUnit** (Testes Unitários)
- **FluentValidation** (Validação de Dados)
- **Docker** (Containerização)
- **Swagger** (Documentação da API)

## Estrutura do Projeto
A solução segue uma estrutura modular baseada em **DDD**:

```
Ambev.DeveloperEvaluation/
│── Adapters/
│   ├── Driven/Infrastructure/Ambev.DeveloperEvaluation.ORM
│── Drivers/
│   ├── WebApi/Ambev.DeveloperEvaluation.WebApi
│── Core/
│── Crosscutting/
│── Tests/
│   ├── Functional/
│   ├── Integration/
│   ├── Unit/
```

- **Adapters** → Contém a camada de infraestrutura, incluindo ORM e Repositórios.
- **Drivers** → Contém a Web API.
- **Core** → Contém as regras de negócio e entidades.
- **Crosscutting** → Contém serviços compartilhados, como Middleware e Mapeamentos.
- **Tests** → Contém os testes unitários, de integração e funcionais.

## Configuração do Ambiente
1. **Clone o repositório**
   ```sh
   git clone https://github.com/seu-usuario/Ambev.DeveloperEvaluation.git
   cd Ambev.DeveloperEvaluation
   ```

2. **Configurar o banco de dados**
   - No arquivo `appsettings.json`, configure a string de conexão do PostgreSQL:
   ```json
   "ConnectionStrings": {
      "DefaultConnection": "Host=localhost;Database=ambev_dev;Username=postgres;Password=admin"
   }
   ```

3. **Executar as migrações do banco**
   ```sh
   dotnet ef database update
   ```

4. **Executar a API**
   ```sh
   dotnet run --project src/Ambev.DeveloperEvaluation.WebApi
   ```
   
5. **Acessar a documentação da API**
   - Acesse [http://localhost:5000/swagger](http://localhost:5000/swagger)

## Testes
Para rodar os testes unitários e de integração:
```sh
dotnet test
```

## Regras de Negócio
A API de vendas deve seguir as seguintes regras:
- Compras acima de **4 itens** idênticos têm **10% de desconto**.
- Compras entre **10 e 20 itens** idênticos têm **20% de desconto**.
- Não é possível vender mais de **20 itens** idênticos.
- Compras abaixo de **4 itens** não recebem desconto.


## Autor
[Agriton Cunha](https://github.com/agritoncunha)

---

Esse README cobre a estrutura do projeto, instalação, execução e regras de negócio. 🚀

