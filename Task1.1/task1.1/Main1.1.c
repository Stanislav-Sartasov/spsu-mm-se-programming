#include <stdio.h>
#include <cmath>

int main()
{
    int N, t, j, f;
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
    printf("123123");
    return 0;
}
