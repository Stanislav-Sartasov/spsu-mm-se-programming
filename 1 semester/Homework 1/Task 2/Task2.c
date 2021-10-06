#include <stdio.h>
#include <math.h>

int gcd(int a, int b)
{
    int c;
    while (b)
    {
        c = a % b;
        a = b;
        b = c;
    }
    return abs(a);
}

int main()
{
    int x, y, z;
    printf("Enter numbers: ");
    scanf("%d%d%d", &x, &y, &z);
    while ((x <= 0) || (y <= 0) || (z <= 0))
    {
        printf("Invalid value. Please, re-enter: ");
        scanf("%d%d%d", &x, &y, &z);
    }
    if ((x * x + y * y == z * z) || (x * x + z * z == y * y) || (z * z + y * y == x * x))
    {
        if ((gcd(x, y) == 1) && (gcd(x, z) == 1) && (gcd(z, y) == 1))
        {
            printf("Triple is a primitive Pythagorean\n");
        }
        else
        {
            printf("Triple is Pythagorean\n");
        }
    }
    else
        {
            printf("Triple is not Pythagorean\n");
        }
    return 0;
}