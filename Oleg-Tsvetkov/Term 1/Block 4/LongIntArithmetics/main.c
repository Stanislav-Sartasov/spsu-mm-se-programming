#define _CRT_SECURE_NO_WARNINGS

#include <stdio.h>
#include <malloc.h>
#include "long_arithmetics_uint.h"
#include <locale.h>

int main()
{
	setlocale(LC_ALL, "Russian");
	printf("Данная программа посчитает и выведет значение 3^5000 в 16-ой системе счисления с помощью алгоритмов длинной арифметики.\n");

	struct long_ar_uint * my_int1 = initialize_long_ar_uint(16);
	set_number_for_long_ar_uint(my_int1, 3);
	struct long_ar_uint * result = pow_for_long_ar_uint(my_int1, 5000);

	print_long_ar_uint(result);

	free_long_ar_uint(my_int1);
	free_long_ar_uint(result);

	return 0;
}
