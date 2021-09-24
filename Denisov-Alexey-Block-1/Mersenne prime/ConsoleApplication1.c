#include <stdio.h>
#include <stdbool.h>
#include <math.h>

bool isSimple(int a);

int main()
{
    for (int n = 1; n <= 31; n++)
    {
        if (isSimple(pow(2, n) - 1))
            printf("%d\n", (int)pow(2, n) - 1);
    }
}

bool isSimple(int a)
{
    switch (a)
    {
    case 1:
        return false;
        break;
    case 2:
        return true;
        break;
    default:
        for (int i = 2; i < a; i++)
        {
            if (a % i == 0)
                return false;
        }
        return true;
    }
}