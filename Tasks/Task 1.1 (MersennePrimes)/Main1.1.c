#include <stdio.h>
#include <math.h>

int main()
{
    int N, t, j, f;

    printf("Mersenne prime numbers on [1,..2^31-1] :\n");
    t = 4;
    for (N = 1; N <= 30; N++)
    {
        f = 1;
        for (j = 3; j < pow((t - 1), (0.5)); j = j + 2)
        {
            if ((t - 1) % j == 0)
            {
                f = 0;
                break;
            }
        }
        if (f)
        {
            printf("%d\n", t - 1);
        }
        t = t * 2;
    }
    return 0;
}
