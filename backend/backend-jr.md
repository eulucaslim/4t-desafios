# Desafio Técnico — Cadastro de Beneficiários (Backend Júnior)

## Objetivo

Construir uma **API REST** para gerenciar **beneficiários** de um plano de saúde. O sistema deve permitir **CRUD**, aplicar **regras de negócio básicas**, persistir os dados em **banco relacional**, possuir **testes de unidade** e **documentação** (Swagger/OpenAPI).

---

## Escopo Funcional

### 1) Entidades

* **Plano**

  * `id` (UUID/auto)
  * `nome` (string, obrigatório, único)
  * `codigo_registro_ans` (string, obrigatório, único, formato livre)
* **Beneficiário**

  * `id` (UUID/auto)
  * `nome_completo` (string, obrigatório)
  * `cpf` (string, obrigatório, único, 11 dígitos numéricos)
  * `data_nascimento` (date, obrigatório)
  * `status` (enum: `ATIVO` | `INATIVO`, padrão `ATIVO`)
  * `plano_id` (FK para Plano, obrigatório)
  * `data_cadastro` (datetime, default now)

> Observação: você pode adicionar campos auditáveis (`created_at`, `updated_at`) se desejar.

### 2) Regras de Negócio

* **CPF único** no sistema e **válido** (apenas formato: 11 dígitos, checagem dos dígitos verificadores conta como bônus).
* Todo **beneficiário deve estar vinculado a um plano existente**.
* **Exclusão** de beneficiário:

  * Hard delete permitido, **ou** soft delete (bônus).
* **Atualização de plano**:

  * Permitida; manter integridade referencial.
* **Status**:

  * Criar como `ATIVO` por padrão.
  * Permitir mudança para `INATIVO` via endpoint de atualização.

### 3) Operações REST (mínimo)

* CRUD de Planos baseados nos dados de exemplo

* CRUD de Beneficiários baseados nos dados de exemplo

### 4) Respostas & Erros

* Utilize **HTTP status codes** adequados:

  * 201 (Created), 200/204 (OK), 400 (Bad Request), 404 (Not Found), 409 (Conflict — CPF duplicado), 422 (Unprocessable Entity — validações), 500 (Internal Error).
* Estruture erros:

```json
{
  "error": "ValidationError",
  "message": "CPF inválido",
  "details": [{"field":"cpf","rule":"invalid"}]
}
```

### 5) Padrões de Arquitetura & Design
Espera-se que o código não apenas "funcione", mas que seja **sustentável, testável e desacoplado**.

#### Implemente uma arquitetura que atenda os requisitos a seguir: 

* **Camada de Domínio Isolada:** Regras de negócio (validação de CPF, regras de vínculo de plano) devem estar em uma camada de domínio pura, sem dependência direta de frameworks Web ou ORMs .
* **Tratamento de Erros Global:** Implementar um mediador ou interceptador de exceções centralizado, garantindo que a API responda em um formato padronizado (ex: RFC 7807) para qualquer falha.
* **Injeção de Dependência:** Uso obrigatório de inversão de controle para facilitar a substituição de componentes (como o Banco de Dados ou serviços de Terceiros) e a criação de Mocks nos testes.

### 6) Escalabilidade & Performance (Requisitos Não Funcionais)
* **Estratégia de Paginação:** O endpoint de listagem de beneficiários (`GET /api/beneficiarios`) **não deve** retornar todos os registros de uma vez. Implemente paginação (via *Offset* ou *Seek*) com limites de tamanho de página configuráveis.
* **Proteção de Atributos (Mass Assignment):** Garanta que a API aceite apenas os campos permitidos no contrato (ex: impedir que um usuário altere o campo `data_cadastro` através de um `PUT` malicioso).
* **Observabilidade Inicial:**
    * **Health Check:** Disponibilizar um endpoint `/health` que retorne o status da aplicação e a conectividade com o Banco de Dados.
    * **Logging Estruturado:** Os logs de erro e requisição devem ser estruturados (ex: formato JSON), permitindo futura ingestão em ferramentas de análise.

---

## Requisitos Técnicos

### Banco de Dados

* **Relacional** (PostgreSQL, MySQL, SQL Server, SQLite, ou qualquer outro SGBD relacional de sua preferência).
* **Migrations(opcional)** versionadas (usando as ferramentas do ecossistema escolhido).

### API & Projeto

* **Linguagem e framework de sua escolha** - use o que você domina melhor! 
* **Estrutura organizada** seguindo as melhores práticas do ecossistema escolhido (ex.: camadas controller/route, service, repository/ORM, domain/models).
* **Validações** no nível da API e/ou domínio.
* **Configuração** (bônus): separada do código (arquivos .env, config files, etc. - não commitar segredos).
* **Docker** (bônus): compose com app + DB.

### Testes de Unidade (mínimo)

* Serviço de **criação de beneficiário** (CPF duplicado → erro 409).
* Validação de **CPF inválido**.
* **Vinculação ao plano** inexistente → erro 422/404.
* Atualização de **status** para `INATIVO`.
* Listagem com **filtros** (ex.: por `status` e `plano_id`).

### Documentação

* **Swagger/OpenAPI** acessível (ex.: `/swagger` ou `/docs`).
* README com:

  * Visão geral
  * Stack utilizada
  * Como rodar (local e via Docker, se houver)
  * Como rodar **testes**
  * Decisões de projeto (trade-offs)
  * Exemplos de requisições (curl ou HTTPie)

#### Para este desafio, a documentação é tão importante quanto o código:

1.  **Diagrama de Arquitetura:** Entregar um diagrama (pode ser C4 Model - Nível 2, ou um diagrama de blocos simples) que ilustre a topologia da solução (API, DB, Camadas Internas).
2.  **Registro de Decisões (ADR - Architectural Decision Records):** No README, inclua uma seção justificando pelo menos **três decisões técnicas** tomadas. Exemplos:
    * Por que escolheu esse padrão de pastas/arquitetura?
    * Por que escolheu essa estratégia de Paginação?
    * Como você lidou com a integridade referencial entre Beneficiário e Plano?
3.  **Análise de Trade-offs:** Cite uma limitação da sua implementação atual e como você a resolveria se o sistema precisasse escalar para 1 milhão de beneficiários ativos.


## Desafio Teórico
**Pergunta de Design:**
Imagine que, após a criação de um beneficiário, o sistema precise:
1.  Enviar um e-mail de boas-vindas.
2.  Notificar um sistema externo de Auditoria Governamental.

Descreva brevemente (no README) como você alteraria a arquitetura para garantir que a API continue rápida e que uma falha no sistema de e-mail não impeça a criação do beneficiário.

## Dados de Exemplo

Planos:

```json
[
  {"nome":"Plano Bronze","codigo_registro_ans":"ANS-100001"},
  {"nome":"Plano Prata","codigo_registro_ans":"ANS-100002"},
  {"nome":"Plano Ouro","codigo_registro_ans":"ANS-100003"},
  {"nome":"Plano Diamante","codigo_registro_ans":"ANS-100004"},
  {"nome":"Plano Executivo","codigo_registro_ans":"ANS-100005"}
]
```

Beneficiários:

```json
[
  {"nome_completo":"João Pereira","cpf":"11144477735","data_nascimento":"1988-01-10","status":"ATIVO","plano":"Plano Prata"},
  {"nome_completo":"Ana Souza","cpf":"98765432100","data_nascimento":"1995-09-03","status":"ATIVO","plano":"Plano Bronze"},
  {"nome_completo":"Carlos Silva","cpf":"12345678901","data_nascimento":"1985-03-15","status":"ATIVO","plano":"Plano Ouro"},
  {"nome_completo":"Maria Santos","cpf":"10987654321","data_nascimento":"1992-07-22","status":"INATIVO","plano":"Plano Diamante"},
  {"nome_completo":"Pedro Oliveira","cpf":"11122233344","data_nascimento":"1990-12-05","status":"ATIVO","plano":"Plano Executivo"}
]
```

---

## Casos de Teste Recomendados

1. **Criar beneficiário válido** → 201 + corpo com `id`.
2. **Criar beneficiário com CPF duplicado** → 409.
3. **Criar beneficiário com plano inexistente** → 422/404.
4. **Atualizar status para INATIVO** → 200 e `status` atualizado.
5. **Listar beneficiários filtrando por status=ATIVO** → retorna apenas ativos.
6. **Buscar beneficiário por id inexistente** → 404.
7. **Excluir beneficiário** → 204 (ou 200 com `deleted=true` se soft delete).

---

## Requisitos Não Funcionais

* Respostas em **JSON**.
* Logs simples de requisição/erro (não vazar dados sensíveis).
* Tratamento de erros centralizado.(opcional)

---

## Entrega

* Repositório público (GitHub/GitLab/Azure DevOps) contendo:

  * Código fonte
  * Migrations / scripts SQL
  * Testes
  * README
  * Swagger/OpenAPI
  * Documentação
  * Padrões de Arquitetura e Design
  * (Bônus) Escalabilidade & Performance
  * (Bônus) Desafio Teórico
  * (Bônus) `docker-compose.yml`

### Como rodar (exemplo esperado no README)

* Comandos para subir a aplicação (ex.: `make up`, `docker compose up -d`, `npm start`, `dotnet run`, `mvn spring-boot:run`, `go run main.go`, etc.)
* Comandos para executar testes (ex.: `make test`, `npm test`, `dotnet test`, `mvn test`, `go test`, `pytest`, etc.)

---

## Observações & Limites

* **Não** é necessário front-end.
* **Sem** integração com APIs externas.
* **ORM/Query Builder** de sua escolha (ex.: JPA/Hibernate, Entity Framework, Sequelize, Prisma, SQLAlchemy, GORM, Eloquent, ActiveRecord, Diesel, etc.).
* Evite bibliotecas que “escondam” toda a lógica de validação de CPF (implemente a checagem mínima ou documente a escolha).