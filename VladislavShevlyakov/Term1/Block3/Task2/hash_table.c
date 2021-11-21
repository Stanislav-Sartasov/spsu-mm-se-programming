#define _CRT_SECURE_NO_WARNINGS

#include <stdio.h>
#include "hash_table.h"

void insert_from_copy(struct list* func)
{
	func->key = hash_function(*func);

	func->sizes[func->key] += 1;
	func->table[func->key] = (int*)realloc(func->table[func->key], sizeof(int) * func->sizes[func->key]);
	func->table[func->key][func->sizes[func->key] - 1] = func->value;
}

void rebalancing_the_table(struct list* func)
{
	if ((func->total_numbers / func->tab_size) >= 0.8) // >=80% - rebalance
	{
		struct list copy;
		copy.tab_size = func->tab_size;
		copy.table = (int**)malloc(sizeof(int*) * copy.tab_size);
		copy.sizes = (int*)malloc(sizeof(int) * copy.tab_size);
		for (int i = 0; i < copy.tab_size; i++)
		{
			copy.sizes[i] = func->sizes[i];
			copy.table[i] = (int*)malloc(sizeof(int) * copy.sizes[i]);
		}

		for (int i = 0; i < copy.tab_size; i++) // assigning the values of the original copy
		{
			for (int j = 1; j < copy.sizes[i]; j++)
			{
				copy.table[i][j] = func->table[i][j];
			}
		}

		func->tab_size = find_following_primes(func->tab_size);

		for (int i = 0; i < copy.tab_size; i++) //
			free(func->table[i]);				// 
		free(func->table);						// deleting original values
		free(func->sizes);						//

		func->table = (int**)malloc(sizeof(int*) * func->tab_size);
		func->sizes = (int*)malloc(sizeof(int) * func->tab_size);
		for (int i = 0; i < func->tab_size; i++)
		{
			func->table[i] = (int*)malloc(sizeof(int) * 1);
			func->sizes[i] = 0;
			func->table[i][0] = 0;
		}

		for (int i = 0; i < copy.tab_size; i++) // transferring values from a copy to the original using a new hash function
		{
			for (int j = 1; j < copy.sizes[i]; j++)
			{
				func->value = copy.table[i][j];
				insert_from_copy(func);
			}
		}

		for (int i = 0; i < copy.tab_size; i++) //
			free(copy.table[i]);                // deleting copy values
		free(copy.table);                       //     
		free(copy.sizes);                       //           
	}
}

int find_following_primes(int tab_size)
{
	tab_size++;
	for (int count = 0; count < 5; tab_size++)
	{
		_Bool is_prime = 0;
		for (int i = 2; i <= tab_size / 2; i++)
		{
			if (tab_size % i == 0)
			{
				is_prime = 1;
				break;
			}
		}
		if (!is_prime)
			count++;
	}
	tab_size--;
	return tab_size;
}

int hash_function(struct list func)
{
	return func.value % func.tab_size;
}

void find_values(struct list* func)
{
	if (func->key >= 0 && func->key < func->tab_size)
	{
		if (func->sizes[func->key] > 0)
		{
			printf("A total of %d values correspond to the key %d:\n", func->sizes[func->key], func->key);
			for (int i = 0; i < func->sizes[func->key]; i++)
			{
				printf("[%d] = %d\n", i, func->table[func->key][i]);
			}
		}
		else
			printf("The values of this key are empty.\n");
	}
	else
		printf("This key does not exist.\n");
}

void insert_value(struct list* func)
{
	func->key = hash_function(*func);

	_Bool is_find = 0;
	for (int i = 1; i < func->sizes[func->key]; i++)
	{
		if (func->value == func->table[func->key][i])
		{
			is_find = 1;
			break;
		}
	}

	if (!is_find)
	{
		func->sizes[func->key] += 1;
		func->table[func->key] = (int*)realloc(func->table[func->key], sizeof(int) * func->sizes[func->key]);
		func->table[func->key][func->sizes[func->key] - 1] = func->value;
	}
}

void delete_key(struct list* func)
{
	if (func->key >= 0 && func->key <= (func->tab_size - 1))
	{
		func->sizes[func->key] = 0;
		func->table[func->key] = (int*)realloc(func->table[func->key], sizeof(int) * func->sizes[func->key]);
		printf("All values of this key have been deleted.\n");
	}
	else
		printf("This key does not exist.\n");
}

void random_filling(struct list* func)
{
	char what;
	printf("Do you want to fill the hash-table with random values?('1' - yes, '0' - no)\n");
	what = getchar();

	while (what != '1' || what != '0')
	{
		if (what == '1')
		{
			int how_many = 0;
			printf("How many numbers?\n");
			scanf("%d", &how_many);

			for (int i = 0; i < how_many; i++)
			{
				func->value = rand();
				insert_value(func);
				func->total_numbers++;
				rebalancing_the_table(func);
			}
			printf("The number are generated!\n");
			break;
		}
		else if (what == '0')
			break;
		else
		{
			printf("Incorrect input, please enter '1' or '0'\n");
			while (getchar() != '\n');
			what = getchar();
		}
	}
}

void actions_on_the_table(struct list* func)
{
	char what;
	printf("\nWhat to do ('1' - insert, '2' - find, '3' - delete, '4' - quit)?\n");
	while (getchar() != '\n');
	what = getchar();

	while (what != '4')
	{
		if (what == '1')
		{
			printf("Insert: ");
			scanf("%d", &func->value);
			insert_value(func);
			func->total_numbers++;
			rebalancing_the_table(func);
		}
		else if (what == '2')
		{
			printf("Find: ");
			scanf("%d", &func->key);
			find_values(func);
		}
		else if (what == '3')
		{
			printf("Delete: ");
			scanf("%d", &func->key);
			delete_key(func);
		}
		while (getchar() != '\n');
		printf("\nWhat to do ('1' - insert, '2' - find, '3' - delete, '4' - quit)?\n");
		what = getchar();
	}
}

struct list initialize_the_structure()
{
	struct list func;
	func.tab_size = 13, func.total_numbers = 0;
	func.table = (int**)malloc(sizeof(int*) * func.tab_size);
	func.sizes = (int*)malloc(sizeof(int) * func.tab_size);
	for (int i = 0; i < func.tab_size; i++)
	{
		func.sizes[i] = 0;
		func.table[i] = (int*)malloc(sizeof(int) * 1);
		func.table[i][0] = 0;
	}
	return func;
}