# TestTask1
ASP.NET Core, Razor Page, EF Core, PostgreSQL, DaData API, Docker compose.
# How to run
git clone https://github.com/Rigatum/TestTask1.git  <br>
cd TestTask1  <br>
docker-compose build  <br>
docker-compose up -d  <br>
http://localhost/80  <br>

При первом запуске после поднятия базы и веб приложения в докере - приложение не будет работать примерно минуту, так как будет работать sql script, который вносит начальные данные в базу (1000 инсертов).
