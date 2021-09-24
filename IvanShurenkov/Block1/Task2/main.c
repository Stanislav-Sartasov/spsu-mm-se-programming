#include <stdio.h>
#include <stdbool.h>
#include <stdlib.h>

#define int64 __int64_t
#define SWAP(a, b) do { typeof(a) tmp = a; a = b; b = tmp; } while (0)
#define MIN(a, b) ((a) < (b) ? (a) : (b))
#define MAX(a, b) ((a) > (b) ? (a) : (b))
#define SQ(x) (x) * (x)


int64 gcd(int64 a, int64 b)
{
    while (b)
    {
        a %= b;
        SWAP(a, b);
    }
    return a;
}

int main()
{
    printf("Pythagorean triple\nEnter three positive integer:");
    int64 x, y, z;
    scanf("%d%d%d", &x, &y, &z);
    int64 min = MIN(x, MIN(y, z)), max = MAX(x, MAX(y, z)), mid = x + y + z - min - max;
    bool is_triple = (SQ(min) + SQ(mid) == SQ(max) ? true : false);
    if (is_triple && 1 == gcd(x, y) && 1 == gcd(y, z) && 1 == gcd(x, z))
        printf("This numbers are prime Pythagorean triple");
    else if (is_triple)
        printf("This numbers are Pythagorean triple");
    else
        printf("This numbers aren't Pythagorean triple");
    return 0;
}
