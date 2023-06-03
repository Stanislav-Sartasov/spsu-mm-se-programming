# Конфигурация запуска образа
В папке с Dockerfile
```
docker build -t deanery .
docker run -p 80:80 deanery
```
# Нагрузочное тестирование
## LazySet
![lazy-add](./images/lazy-add.png)
![lazy-remove](./images/lazy-remove.png)
![lazy-contains](./images/lazy-contains.png)
### Отказ от обслуживания при 1550 rps, примерное число записей 190
![lazy-contains](./images/lazy-fault.png)

## StripedCuckooHashSet
![lazy-add](./images/striped-add.png)
![lazy-remove](./images/striped-remove.png)
![lazy-contains](./images/striped-contains.png)
### Отказ от обслуживания при 1600 rps, примерное число записей 210
![lazy-contains](./images/striped-fault.png)