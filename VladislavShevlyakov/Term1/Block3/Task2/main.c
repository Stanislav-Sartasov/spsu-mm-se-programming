#define _CRT_SECURE_NO_WARNINGS

#include <stdio.h>
#include "hash_table.h"

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

int main()
{
	struct list hash_table = initialize_the_structure();

	printf("The program is a hash table that has functions such as inserting a value,"
		"deleting a key, outputting values by key and rebalancing the table.\n\n");

	printf("The principle of operation of a hash table is that when it reaches 80 percent"
		"fullness, the hash function(mod (prime number, where the original prime number is 13))"
		"changes to a new hash function (mod (fifth largest prime number)).\n\n");

	random_filling(&hash_table);
	actions_on_the_table(&hash_table);

	for (int i = 0; i < hash_table.tab_size; i++)
		free(hash_table.table[i]);
	free(hash_table.table);
	free(hash_table.sizes);

	return 0;
}