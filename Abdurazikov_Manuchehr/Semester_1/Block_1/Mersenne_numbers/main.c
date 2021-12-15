#include <stdio.h>
#include <math.h>

int isSimple(int number)
{
	int sqrt = (int)sqrtf(number);
		
	if (number > sqrt) {
		sqrt = number;
	}

	for (int i = 2; i <= sqrt; i++) {
		if (number % i == 0 && number != i) {
			return 0;
		}
		else if (i == sqrt) {
			return 1;
		}
	}
	return 1;
}

int main(void)
{
	unsigned int		num1 = 1;
	unsigned long int	num2 = 100;
	unsigned int		num1_from_sqrt;
	int 				start = 0;
	unsigned long 		pow;

	printf("\033[33mMerson numbers for 1 <= 2^N - 1 <= 2^31 - 1: ");
	while (num1 <= num2) {
		if (isSimple(num1) && num1 > 1) {
			pow = (long)powf(2, num1) - 1;
			
			if (isSimple(pow)) {
				if (pow > 2147483647) {
					break ;
				}
				if (start == 0) {
					start = 1;
					printf("\033[32m%lu", pow);
				}
				else {
					printf("\033[32m, %lu", pow);
				}
			}
		}
		num1++;
	}
	printf("\n");
	return (0);
}
