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

[Leia mais sobre POO](https://www.alura.com.br/artigos/poo-programacao-orientada-a-objetos?srsltid=AfmBOoqp27m67Pz73CpRdbgW3aOuMSGFfhBOx1tvpEKBhku3BG6VEUAj)

## Pacotes

O comando `dotnet add package` é utilizado para adicionar ou atualizar referências de pacotes em um projeto.

Exemplo de pacotes úteis para aplicações com Entity Framework Core e SQLite:
```sh
dotnet add package Microsoft.EntityFrameworkCore.Sqlite
dotnet add package Microsoft.EntityFrameworkCore.Design
