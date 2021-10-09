#include <stdio.h>

int P(int x, int y)
{
    while (x != y)
    {
        if (x > y)
        {
            x = x - y;
        }
        else
        {
            y = y - x;
        }
    }
    return x;
}

int main()
{

    int a, b, c;
    printf("Enter any three positive integers:\n");
    scanf("%d%d%d", &a, &b, &c);
    if ((a * a == b * b + c * c) || (b * b == a * a + c * c) || (c * c == b * b + a * a))
    {
        if ((P(a, b) == 1) || (P(b, c) == 1) || (P(a, c) == 1))
        {
            printf("These numbers are Primitive phythagorean triplets\n");
        }
        else
        {
            printf("These numbers are not Primitive phythagorean triplets\n");
        }
    }
    else
    {
        printf("These numbers are not Pythagorean triplets\n");
    }
    return 0;
}