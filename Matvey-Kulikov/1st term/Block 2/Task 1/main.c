#include <stdio.h>
#include <string.h>
#include <stdint.h>
#define SURNAME "Kulikov"
#define NAME "Matvey"
#define PATRONYMIC "Konstantinovich"

void print_bits(int16_t size_type, void* number_pointer)
{
	uint64_t number;
	if (size_type == 32)
	{
		number = *(uint32_t*)number_pointer >> 32;
	}
	else if (size_type == 64)
	{
		number = *(uint64_t*)number_pointer;

	}
	for (int16_t i = size_type - 1; i >= 0; i--)
	{
		uint64_t bitmask = (uint64_t)1 << i;
		uint64_t masked = number & bitmask;
		uint64_t bit = masked >> i;
		printf("%lu", bit);
	}
}

int main()
{
	int32_t basic_number = strlen(SURNAME) * strlen(NAME) * strlen(PATRONYMIC); // 630
	printf("This program outputs composition of surname, name and patronymic (%d) in binary notation of:\n", basic_number);

	int32_t negative_int32 = -1 * basic_number;
	printf("- Negative int32: ");
	print_bits((int16_t)sizeof(int32_t) * 8, &negative_int32);

	float positive_float = (float)basic_number;
	printf("\n- Positive float: ");
	print_bits((int16_t)sizeof(float) * 8, &positive_float);

	double negative_double = (double)basic_number * -1;
	printf("\n- Negative double: ");
	print_bits((int16_t)sizeof(double) * 8, &negative_double);
	return 0;
}
