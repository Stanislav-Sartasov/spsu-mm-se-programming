# Docker
```
docker build -t examsystemapi:latest .
docker run -p 8080:80 examsystemapi:latest

```

# Результаты нагрузочного тестирования
## Striped сuckoo hash set
### Распределение времени выполнения запросов каждого вида
![StripedCuckooHashSet-add](./images/striped-cuckoo-hs/Add.png)
![StripedCuckooHashSet-contains](./images/striped-cuckoo-hs/Contains.png)
![StripedCuckooHashSet-remove](./images/striped-cuckoo-hs/Remove.png)
### Число клиентов, приводящее к отказу от обслуживания по таймауту в 10 секунд
#### Число клиентов -- 1300, примерное количество записей в словаре -- 215
![StripedCuckooHashSet-reject](./images/striped-cuckoo-hs/bad-1300-215.png)

## Striped hash set
### Распределение времени выполнения запросов каждого вида
![StripedHashSet-add](./images/striped-hs/Add.png)
![StripedHashSet-contains](./images/striped-hs/Contains.png)
![StripedHashSet-remove](./images/striped-hs/Remove.png)
### Число клиентов, приводящее к отказу от обслуживания по таймауту в 10 секунд
#### Число клиентов -- 1400, примерное количество записей в словаре -- 146
![StripedHashSet-reject](./images/striped-hs/bad-1400-146.png)
