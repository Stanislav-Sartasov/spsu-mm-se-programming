#include <stdio.h>
#include <math.h>
#include <stdbool.h>

bool isPrime(int x)
{
	if (x > 1)
	{
		for (int i = 2; i <= (int)(pow(x, 0.5)) + 1; i++)
		{
			if (x % i == 0)
			{
				return false;
			}
		}
		return true;
	}
	else
		return false;
}


int main()
{
    int a = 0;
    double b = 0, c = 0;

    for (a = 1; a <= 31; a++) 
    {
        b = pow(2, a);
        c = b - 1;

        if (isPrime((int)c))
        {
            printf("Mersenne prime number is equal to: %d\n", (int)c);
        }
    }
    return 0;
}

