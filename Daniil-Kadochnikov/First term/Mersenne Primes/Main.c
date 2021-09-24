#include <stdio.h>
#include <math.h>


void main()
{
	printf("Prime Mersenne numbers from the interval [1; 2^31 - 1] are:\n");
	for (int n = 1; n < 32; n++)
	{
		int Mersenne_number = pow(2, n) - 1;
		int count = 0;
		for (int coefficicent = 2; coefficicent <= (int)(sqrt(Mersenne_number)); coefficicent++)
		{
			int remain = Mersenne_number % coefficicent;
			if (remain == 0) break;
			else
			{
				count++;
			}
		}
		if (count == (int)(sqrt(Mersenne_number)) - 1 && Mersenne_number != 1)
		{
			printf("%d\n", Mersenne_number);
		}
	}
}