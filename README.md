# Instruções para a instalação e execução do projeto do projeto

## 1. Instalação da base de dados:
- Para este projeto é preciso ter instalado o XAMPP Control Panel com a Apache e o MySQL
- Dentro do XAMPP Control Panel, inicie o Apache e o MySQL
- no http://localhost/phpmyadmin/ criar uma nova base de dados chamada ``ticketzone``

## 2. Fazer as Migrations iniciais:
no terminal:
```
cd .\BlazorApp1\
dotnet tool install --global dotnet-ef
dotnet ef migrations add InitialCreate 
dotnet ef database update
```


## 3. Importe da API-KEY:
- criar um ficheiro .env dentro da pasta de BlazorApp1
- escrever as variaveis de ambiente

## 4. Executar o projeto:
- incie o XAMPP Control Panel
- inicie o Apache e o MySQL

na terminal:
```
cd .\BlazorApp1\
dotnet run
```
___

## Conselho para desenvolvedores de front-end:

Caso queriam desenvolver a parte do front-end, poderá ser conveniente usar:
```
dotnet watch
```
em vez de ``dotnet run``, isto serve para que as mudanças das paginas html/blazor/css sejam recarregadas a tempo real