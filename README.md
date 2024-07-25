# ASPReactPasswordManager
My personal project on ASP.NET, React, Next.js and PostgreSQL with Docker

# Установка
Порядок выполнения:
1. API
2. Frontend

API:
1. Открыть PowerShell и выбрать папку с решением (команда: cd [path]\PasswordManagerWebApp\API)
2. Выполнить команду: docker-compose up -d
3. Выполнить команду: dotnet tool install --global dotnet-ef (если dotnet ef глобально не установлен)
4. Выполнить команду: dotnet ef migrations add init -s .\PasswordManager.API\ -p .\PasswordManager.DataAccess\
5. Выполнить команду: dotnet ef database update -s .\PasswordManager.API\ -p .\PasswordManager.DataAccess\
6. Запустить проект в одной из следующих программ: Visual Studio, Rider, Visual Studio Code
При запуске выбирать http.
В случае ошибки на этапе с подключением docker, возможно предварительно необходимо будет установить официальный образ postgres.

Frontend:
1. Открыть проект в Visual Studio Code
2. Открыть в ней Terminal и выбрать папку с решением (команда: cd [path]\PasswordManagerWebApp\frontend\passwordmanager)
3. Выполнить команду: npm run dev (необходимо будет дождаться полной компиляции)
4. Перейти в браузере по выведенному адресу (по умолчиню localhost:3000)
