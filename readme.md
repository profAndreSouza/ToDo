# API MVC

## Criando um novo projeto

O comando `dotnet new` é utilizado para criar um novo projeto, arquivo de configuração ou solução com base em um modelo especificado.

[Documentação oficial](https://learn.microsoft.com/en-us/dotnet/core/tools/dotnet-new)

Para criar uma nova aplicação Web API utilizando .NET:
```sh
dotnet new webapi -n ToDoApi
```

Para executar a aplicação:
```sh
dotnet run
```

## RESTFul

RESTFul é um estilo arquitetural que define um conjunto de restrições para a criação de serviços Web. APIs RESTFul seguem os princípios REST para comunicação entre sistemas.

[Saiba mais](https://aws.amazon.com/pt/what-is/restful-api/)

## Arquitetura MVC

A arquitetura MVC (Model-View-Controller) separa a aplicação em três camadas:
- **Model**: Responsável pela lógica de negócios e acesso aos dados.
- **View**: Camada de apresentação, exibe os dados ao usuário.
- **Controller**: Controla a interação entre Model e View, recebendo requisições e retornando respostas.

[Saiba mais](https://aws.amazon.com/pt/what-is/restful-api/)

## Orientação a Objetos

A Programação Orientada a Objetos (POO) é um paradigma de programação baseado em conceitos como classes, objetos, herança, encapsulamento e polimorfismo.

[Leia mais sobre POO](https://www.alura.com.br/artigos/poo-programacao-orientada-a-objetos)

## Pacotes

O comando `dotnet add package` é utilizado para adicionar ou atualizar referências de pacotes em um projeto.

Exemplo de pacotes úceis para aplicações com Entity Framework Core e SQLite:
```sh
dotnet add package Microsoft.EntityFrameworkCore.Sqlite
dotnet add package Microsoft.EntityFrameworkCore.Design
```

## Criando a API ToDo com MVC

### 1. Criar um novo projeto MVC
```sh
dotnet new mvc -n ToDoMvc
cd ToDoMvc
```

### 2. Criar o modelo (Model)
Crie o arquivo `Models/ToDoItem.cs`:
```csharp
namespace ToDoMvc.Models
{
    public class ToDoItem
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public bool IsCompleted { get; set; }
    }
}
```

### 3. Configurar o banco de dados
Crie o arquivo `Data/AppDbContext.cs`:
```csharp
using Microsoft.EntityFrameworkCore;
using ToDoMvc.Models;

namespace ToDoMvc.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}

        public DbSet<ToDoItem> ToDoItems { get; set; }
    }
}
```

Adicione a conexão no `Program.cs`:
```csharp
using Microsoft.EntityFrameworkCore;
using ToDoMvc.Data;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=todo.db"));
builder.Services.AddControllersWithViews();

var app = builder.Build();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.MapControllerRoute(name: "default", pattern: "{controller=ToDo}/{action=Index}/{id?}");
app.Run();
```

### 4. Criar o Controller
Crie `Controllers/ToDoController.cs`:
```csharp
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDoMvc.Data;
using ToDoMvc.Models;

namespace ToDoMvc.Controllers
{
    public class ToDoController : Controller
    {
        private readonly AppDbContext _context;
        public ToDoController(AppDbContext context) { _context = context; }

        public async Task<IActionResult> Index()
        {
            return View(await _context.ToDoItems.ToListAsync());
        }

        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ToDoItem toDoItem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(toDoItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(toDoItem);
        }

        [HttpGet]
        public async Task<IActionResult> GetTasks()
        {
            return Json(await _context.ToDoItems.ToListAsync());
        }
    }
}
```

### 5. Criar as Views
Crie `Views/ToDo/Index.cshtml`:
```html
@model IEnumerable<ToDoMvc.Models.ToDoItem>
<h2>Lista de Tarefas</h2>
<a href="@Url.Action("Create")" class="btn btn-primary">Nova Tarefa</a>
<table class="table mt-3">
    <thead>
        <tr>
            <th>ID</th>
            <th>Título</th>
            <th>Descrição</th>
            <th>Concluído</th>
            <th>Ações</th>
        </tr>
    </thead>
    <tbody>
        @foreach (var item in Model)
        {
            <tr>
                <td>@item.Id</td>
                <td>@item.Title</td>
                <td>@item.Description</td>
                <td>@(item.IsCompleted ? "Sim" : "Não")</td>
                <td>
                    <a href="@Url.Action("Edit", new { id = item.Id })">Editar</a> |
                    <a href="@Url.Action("Delete", new { id = item.Id })">Excluir</a>
                </td>
            </tr>
        }
    </tbody>
</table>
```

### 6. Criar o Banco de Dados e Rodar a API
```sh
dotnet ef migrations add InitialCreate
dotnet ef database update
dotnet run
```
Agora, você pode acessar:
- **Lista de tarefas:** `https://localhost:5001/ToDo`
- **API JSON:** `https://localhost:5001/ToDo/GetTasks`

