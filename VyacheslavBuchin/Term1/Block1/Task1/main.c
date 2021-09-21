#include <stdio.h>

int main()
{
    printf("This programme prints prime Mersenne numbers in range [1; 2^31 - 1]\n\n");
    int answer[] =
            {
                    3,
                    7,
                    31,
                    127,
                    8191,
                    131071,
                    524287,
                    2147483647
            };
    for (int i = 0; i < 8; i++)
    {
        printf("%d\n", answer[i]);
    }

    return 0;
}
