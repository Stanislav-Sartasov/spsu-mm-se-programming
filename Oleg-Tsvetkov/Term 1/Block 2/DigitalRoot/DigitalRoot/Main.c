#include <stdio.h>
#include <malloc.h>
#include <locale.h>

long long max(long long first, long long second)
{
	return first > second ? first : second;
}

long long digital_root(long long num)
{
	return 1 + ((num - 1) % 9);
}

int main()
{
	setlocale(LC_ALL, "Russian");
	printf("Назовём суммой цифровых корней сумму цифровых корней отдельных множителей в разложении составного числа.\n");
	printf("Пусть mdrs(n) - максимальная сумма цифровых корней для числа n.\n");
	printf("Данная программа вычислит сумму mdrs(n) для n=[2; 999999]\n");

	long long* arr_mdrs = (long long*)malloc(sizeof(long long) * 1000000);
	long long result_sum = 0;

	for (int i = 2; i < 10; ++i)
	{
		arr_mdrs[i] = i;
		result_sum += i;
	}
	for (int i = 10; i <= 999999; ++i)
	{
		arr_mdrs[i] = digital_root(i);
		for (int j = 2; j * j <= i + 1; ++j)
		{
			if (i % j == 0)
			{
				arr_mdrs[i] = max(arr_mdrs[i], arr_mdrs[j] + arr_mdrs[i / j]);
			}
		}
		result_sum += arr_mdrs[i];
	}

	printf("Ответ: %lld", result_sum);

	free(arr_mdrs);

	return 0;
}