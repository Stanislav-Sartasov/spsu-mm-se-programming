## Usage

```Bash
docker build -t <tag> <path to folder with Dockerfile>
```

```Bash
docker run -p 80:80 <tag>
```

## Max users

- Optimistic set - 660 users with 16k entries
- Lazy set - 1000 users with 7k entries

## No load

### Optimistic set

![](boxplots/optimistic_no_load.png)

### Lazy set

![](boxplots/lazy_no_load.png)

## 50 RPS

### Optimistic set

![](boxplots/optimistic_50rps.png)

### Lazy set

![](boxplots/lazy_50rps.png)

## 100 RPS

### Optimistic set

![](boxplots/optimistic_100rps.png)

### Lazy set

![](boxplots/lazy_100rps.png)