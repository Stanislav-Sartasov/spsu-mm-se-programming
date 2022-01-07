#include <stdio.h>
#include <stdlib.h>
#include <math.h>

#define long_number struct long_number
#define num_base 256

long_number
{
	unsigned char* num;
	int length;
};

void big_mult(long_number* big_number,int num)
{
	int res_temp =0, delta_prv=0;
	for (int j = big_number->length - 1; j >= 0; j--)
	{
		res_temp = (big_number->num[j] * num + delta_prv) % num_base;
		delta_prv = (big_number->num[j] * num + delta_prv) / num_base;
		big_number->num[j] = res_temp;
	}
}
void pow_number(int num, int power, long_number* answer)
{
	answer->length = (int)(power * (log(num) / log(num_base))) + 1;
	answer->num = (unsigned char*)calloc(answer->length, sizeof(unsigned char));
	answer->num[answer->length - 1] = 1;

	
	for (int i = 0; i < power; i++)
	{
		big_mult(answer,  num);
	}
}

void printlong_number(long_number* num)
{
	for (int i = 0; i < num->length; i++)
	{
		printf("%02x", num->num[i]);
	}
	free(num->num);
}


int main()
{
	long_number answer;
	pow_number(3, 5000, &answer);
	printf("Answer:");
	printlong_number(&answer);

	return 0;
}