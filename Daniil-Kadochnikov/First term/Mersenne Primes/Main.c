#include <stdio.h>
#include <math.h>



int main()
{
	printf("The programm shows the prime Mersenne numbers from the interval [1; 2^31 - 1].\n\n");
	printf("Prime Mersenne numbers from the interval [1; 2^31 - 1] are:\n");
	for (int n = 1; n < 32; n++)
	{
		int mersenneNumber = pow(2, n) - 1;
		int count = 0;
		for (int coefficicent = 2; coefficicent <= (int)(sqrt(mersenneNumber)); coefficicent++)
		{
			int remain = mersenneNumber % coefficicent;
			if (remain == 0) break;
			else
			{
				count++;
			}
		}
		if (count == (int)(sqrt(mersenneNumber)) - 1 && mersenneNumber != 1)
		{
			printf(">>> %d\n", mersenneNumber);
		}
	}
	return 0;
}