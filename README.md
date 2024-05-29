# Веб приложение для поиска и ранжирования подходящих методов увелечения нефтеотдачи пластов для заданного набора суммарных свойств коллектора, жидкости и газов

## Архитектурные решения и паттерны

- SPA + WebApi
- Clean Architecture
- DDD
- CQRS

## Стек технологий

### Для frontend-а использовались:

- [React v18.2](https://react.dev/)
- [Axios v1.6](https://axios-http.com/ru/docs/intro)
- [Tanstack Query v5.24](https://tanstack.com/query/v5/docs/framework/react/overview)
- [Tanstack Router v1.18](https://tanstack.com/router/v1/docs/framework/react/overview)
- [Tanstack Table v8.13](https://tanstack.com/table/v8/docs/introduction)
- [React Hook Form v7.51](https://react-hook-form.com/)
- [Recharts v2.12](https://recharts.org/en-US/)
- [Tailwind CSS v3.4](https://tailwindcss.com/docs/installation)
- [Typescript v5.2](https://www.typescriptlang.org/docs/)
- [Vite v5.1](https://vitejs.dev/)

### Для backend-a использовались:

- [ASP.NET Core Web Api v8.0](https://learn.microsoft.com/en-us/aspnet/core/?view=aspnetcore-8.0)
- [ASP.NET Core Authentication v8](https://learn.microsoft.com/en-us/aspnet/core/security/authentication/?view=aspnetcore-8.0)
- [Carter v8.0](https://github.com/CarterCommunity/Carter)
- [Entity Framework Core v8.0](https://learn.microsoft.com/ru-ru/ef/core/)
- [Swashbuckle v6.4](https://github.com/domaindrivendev/Swashbuckle.AspNetCore)
- [FluentValidation v11.9](https://docs.fluentvalidation.net/en/latest/)
- [Mapster v7.4](https://github.com/MapsterMapper/Mapster)
- [MediatR v12.2](https://github.com/jbogard/MediatR)
- [Bcrypt.Net-Next v4.0](https://github.com/BcryptNet/bcrypt.net)
- [Newtonsoft.Json v13.0](https://www.newtonsoft.com/json)
- [Npgsql v8](https://www.npgsql.org/)

### Базы данных: [PostgreSQL v16](https://www.postgresql.org/docs/16/index.html)

### Веб-сервер/балансировщик нагрузки: [Nginx](https://nginx.org/ru/docs/)

## Инструкция использования

Вам необходимо установить локально:

- [.NET CORE v8](https://dotnet.microsoft.com/en-us/download)
- [NodeJS v20](https://nodejs.org/en/download/package-manager), также необходимо установить пакетный менеджер [yarn](https://classic.yarnpkg.com/en/docs/install#windows-stable)
- [PostgreSQL v16](https://www.postgresql.org/download/) или запустить в контейнере Docker

### В среде локальной разработки

Первым делом запустите PostgreSQL на вашем компьютере вручную или с помощью Docker, используя комманду в терминале

```sh
docker-compose up -d database
```

Для запуска backend-а необходимо перейти в директорию /api и выполнить команду в терминале

```sh
dotnet run --project .\src\Api\
```

> Важно!!!
> При запуске проекта будут применины миграции, расположенные в директории /api/src/Infrastructure/Database/Migrations

Перед запуском frontend убедитесь в установке зависимостей

```sh
yarn install
```

Для запуска frontend-a необходимо перейти в директорию /web и выполнить команду

```sh
yarn dev
```

### В среде production

Используйте команду для запуска всего проекта, включая frontend и backend, базы данных, балансировщик нагрузки

```
docker-compose up --build -d
```
