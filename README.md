# Fatec Jundiaí - Projeto de Graduação de Curso
### Sobre ###
Esse projeto é um software web para gestão de oficios, editais, memorandos e portarias utilizando o .net core 2.1 com razor pages, entity framework e mysql

Súmario
=================
<!--ts-->
   * [Sobre](#Sobre)
   * [Tabela de Conteudo](#tabela-de-conteudo)
   * [Instalação](#Instalação)
   
   * [Tecnologias](#Tecnologias)
<!--te-->
  
 
  # Instalação
  Para executar o projeto são necessarios os seguintes programas:
  
<!--ts-->
   * [.Net Core 2.1(SDK)](https://dotnet.microsoft.com/download/dotnet-core/2.1)
   * [MYSQL Server](https://dev.mysql.com/downloads/mysql/)
   * [Visual Studio Code](https://code.visualstudio.com/) ou [Visual Studio](https://visualstudio.microsoft.com/pt-br/downloads/)
      * [Para usuarios do VSCODE baixar a extensão do C#](https://visualstudio.microsoft.com/pt-br/downloads/)
<!--te-->
# Começando
Após instalar os programas necessarios  basta abrir a pasta do projeto e abrir o arquivo appsettings.json
```
{
  "Logging": {
    "LogLevel": {
      "Default": "Warning"
    }
  },
  "AllowedHosts": "*",
  "ConnectionStrings": {
    "OficioMVCContext": "Server=localhost;port=3307;uid=root;password= - ;database=developDB"
  }
```
Altere a linha de OficioMVCContext para  o connection string do seu banco de dados MySql, atlerando servidor,porta,uid e senha se necessario.

Caso esteja executando a aplicação em ambiente de teste, execute o comando dotnet ef update database para criar o database em seu banco de dados SQL

o script para execução do banco também se encontra na pasta DATA/Create.SQL

após isso na pasta da solução execute dotnet run para executar a aplicação.

Em ambiente de testes o sistema cadastra dois usuarios:
Com nome teste e master ambos com senhas iguais aos seus nomes
# Tecnologias

.Net Core versão 2.1
MySQL versão 5.6 
Entity Framework core 2.0
Razor pages
HTML
CSS
JavaScript
JQUERY
