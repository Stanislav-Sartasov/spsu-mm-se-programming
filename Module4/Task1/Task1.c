#include <stdio.h>
#include <stdlib.h>

#define long_number struct long_number

#define num_base 268435456

long_number
{
	int num;
	long_number* next;
};

void freelong_number(long_number* num)
{
	long_number* ptr_prv, * ptr;

	ptr = num;

	while (ptr->next != NULL)
	{
		ptr_prv = ptr;
		ptr = ptr->next;

		free(ptr_prv);
	}

	free(ptr);
}

long_number* createlong_number(int num)
{
	long_number* result = malloc(sizeof(long_number));
	result->num = num;
	result->next = NULL;

	return result;
}

int get_size(long_number* num)
{
	int k = 1;

	while (num->next != NULL)
	{
		k++;
		num = num->next;
	}

	return k;
}

long_number* big_mult(long_number* num1, long_number* num2)
{
	long_number* result = malloc(sizeof(long_number));
	result->num = 0;
	result->next = NULL;

	int size1 = get_size(num1);
	int size2 = get_size(num2);

	long_number* small, * big;

	if (size1 > size2)
	{
		small = num2;
		big = num1;
	}
	else
	{
		small = num1;
		big = num2;
	}

	long_number* res_chk = result;

	while (small != NULL)
	{
		int delta_prv = 0;
		int delta_next = 0;

		long_number* big_temp = big;
		long_number* res_temp = res_chk;


		while (big_temp->next != NULL)
		{
			delta_next = small->num * big_temp->num;

			res_temp->num += delta_next % num_base + delta_prv;

			delta_prv = delta_next / num_base + res_temp->num / num_base;

			res_temp->num %= num_base;

			if (res_temp->next == NULL)
			{
				res_temp->next = malloc(sizeof(long_number));
				res_temp->next->num = 0;
				res_temp->next->next = NULL;
			}

			big_temp = big_temp->next;
			res_temp = res_temp->next;
		}

		delta_next = small->num * big_temp->num;

		if (res_temp == NULL)
		{
			res_temp = malloc(sizeof(long_number));
			res_temp->num = 0;
			res_temp->next = NULL;
		}

		res_temp->num += delta_next % num_base + delta_prv;

		delta_prv = delta_next / num_base + res_temp->num / num_base;

		res_temp->num %= num_base;

		if (delta_prv != 0)
		{
			if (res_temp->next == NULL)
			{
				res_temp->next = malloc(sizeof(long_number));
				res_temp->next->num = 0;
				res_temp->next->next = NULL;
			}

			res_temp->next->num += delta_prv;
		}

		small = small->next;
		res_chk = res_chk->next;
	}

	return result;

}

long_number* big_sum(long_number* num1, long_number* num2)
{
	long_number* result = calloc(1, sizeof(long_number));
	long_number* return_value = result;
	result->next = NULL;
	result->num = 0;

	while ((num1->next != NULL) && (num2->next != NULL))
	{
		result->num += num1->num + num2->num;
		result->next = calloc(1, sizeof(long_number));

		result->next->num = 0;
		result->next->next = NULL;

		if (result->num >= num_base)
		{
			result->next->num = result->num / num_base;
			result->num = result->num % num_base;
		}

		num1 = num1->next;
		num2 = num2->next;
		result = result->next;
	}

	result->num += num1->num + num2->num;

	if (result->num >= num_base)
	{
		result->next = calloc(1, sizeof(long_number));
		result->next->next = NULL;
		result->next->num = result->num / num_base;
		result->num = result->num % num_base;
	}

	long_number* cnt = NULL;

	if (num1->next != NULL)
	{
		cnt = num1->next;
	}
	else if (num2->next != NULL)
	{
		cnt = num2->next;
	}

	if (cnt != NULL)
	{
		while (cnt != NULL)
		{
			if (result->next == NULL)
			{

				result->next = calloc(1, sizeof(long_number));
				result->next->num = 0;
				result->next->next = NULL;

			}

			result = result->next;
			result->num += cnt->num;

			if (result->num >= num_base)
			{
				if (result->next == NULL)
				{

					result->next = calloc(1, sizeof(long_number));
					result->next->num = 0;
					result->next->next = NULL;

				}

				result->next->num = result->num / num_base;
				result->num = result->num % num_base;
			}

			result = result->next;
			cnt = cnt->next;
		}
	}


	return return_value;
}

long_number* pow(long_number* num, int power)
{
	long_number* temp = malloc(sizeof(long_number));
	*temp = *num;

	for (int i = 1; i < power; i++)
	{
		*temp = *big_mult(temp, num);
	}

	return temp;
}

void printlong_number(long_number* num)
{
	if (num->next == NULL)
	{
		if (num->num != 0)
			printf("%x", num->num);
		return;
	}

	printlong_number(num->next);
	printf("%07x", num->num);
}

int main()
{
	int num = 3;
	long_number* answer = createlong_number(num);
	*answer = *pow(answer, 5000);
	printf("Answer:");
	printlong_number(answer);
	freelong_number(answer);

	return 0;
}