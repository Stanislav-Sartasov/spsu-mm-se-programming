#include <stdlib.h> 
#include <stdio.h> 
#include <math.h>
#include <stdbool.h>

bool check_number(int n)
{
	if (n > 1)
	{
		for (int i = 2; i < sqrt(n) + 1; i++)
		{
			if (n % i == 0)
			{
				return false;
			}
		}
		return true;
	}
	return false;
}

int main()
{
	system("chcp 1251");
	system("cls");

	printf("����� ���� ������� ����� �������� �� ������� [1; 2^(31) - 1]: \n");

	for (int i = 1; i <= 31; i++)
	{
		if (check_number((int)pow(2, i) - 1))
		{
			printf("%d \n", (int)pow(2, i) - 1);
		}
	}
	return 0;
}
