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
Число клиентов -- 1300, примерное количество записей в словаре -- 215
![StripedCuckooHashSet-reject](./images/striped-cuckoo-hs/bad-1300-215.png)

## Coarse grained hash set
### Распределение времени выполнения запросов каждого вида
![CoarseGrainedHashSet-add](./images/coarse-grained-hs/Add.png)
![CoarseGrainedHashSet-contains](./images/coarse-grained-hs/Contains.png)
![CoarseGrainedHashSet-remove](./images/coarse-grained-hs/Remove.png)
### Число клиентов, приводящее к отказу от обслуживания по таймауту в 10 секунд
Число клиентов -- 1100, примерное количество записей в словаре -- 171
![CoarseGrainedHashSet-reject](./images/coarse-grained-hs/bad-1100-171.png)
