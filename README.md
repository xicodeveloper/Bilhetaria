# Instruções para a instalação e execução do projeto do projeto


## 1. Fazer as Migrations iniciais:
no terminal:
```
cd .\BlazorApp1\
dotnet tool install --global dotnet-ef
dotnet ef migrations add InitialCreate 
dotnet ef database update
```


## 2. Importe da API-KEY:
- criar um ficheiro .env dentro da pasta de BlazorApp1
- escrever as variaveis de ambiente


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