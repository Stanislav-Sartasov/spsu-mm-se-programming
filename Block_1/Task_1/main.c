#include <stdio.h>



int primeCheck(unsigned long n)
{
   for(unsigned long i=2;i<n/2;i++)
   {
   if(n%i==0)
   return 0;
   }
   return 1;

}
int main()
{
	printf("The Mersenne prime numbers within the range[1; 2^31 - 1] are: \n\n");

	for (int i = 1; i <= 31; i++)
	{
		int number = (1ll << i) - 1;
		if (primeCheck(number))
			printf("%d\n", number);
	}

	return 0;
}

