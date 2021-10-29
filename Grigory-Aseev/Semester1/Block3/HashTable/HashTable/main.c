#include "hash_table.h"
#include <stdio.h>
#include <inttypes.h>


static void test_by_decimal();
static void test_by_string();
static void test_by_real();

int main()
{
	printf("This program demonstrates the capabilities of a hash table in various types (decimal, real, string).\n");
	test_by_decimal();
	test_by_string();
	test_by_real();
	return 0;
}

static void test_by_decimal()
{
	printf("Decimal Test Run\n");
	struct table* tab = (struct table*)malloc(sizeof(struct table));
	create_table(tab, DECIMAL_ELEM, DECIMAL_ELEM);
	printf("Adding a decimal key-value pair...\n");
	for (size_t i = 0; i < 64; i++)
	{
		add_elem(tab, i*7, i * 1000 - 7);
	}
	print_hash_table(tab);
	printf("Searching for some element by key...\n");
	struct list* el;
	for (size_t i = 20; i < 33; i++)
	{
		el = find_elem(tab, i * 7);
		if (el == NULL)
		{
			printf("the key(%d) element not found\n", i * 7);
		}
		else
		{
			printf("the key(%I64d) element found: %I64d\n", el->key.decimal, el->value.decimal);
		}
	}
	printf("Removing these items...\n");
	for (size_t i = 20; i < 33; i++)
	{
		del_elem(tab, i * 7);
	}
	print_hash_table(tab);
	printf("Searching for these items...\n");
	for (size_t i = 20; i < 33; i++)
	{
		el = find_elem(tab, i * 7);
		if (el == NULL)
		{
			printf("the key(%d) element not found\n", i*7);
		}
		else
		{
			printf("the key(%I64d) element found: %I64d\n", el->key.decimal, el->value.decimal);
		}
	}
	free_table(tab);
}

static void test_by_real()
{
	printf("Real Test Run\n");
	struct table* tab = (struct table*)malloc(sizeof(struct table));
	create_table(tab, DECIMAL_ELEM, REAL_ELEM);
	printf("Adding a decimal key and a real value pair...\n");
	for (size_t i = 0; i < 64; i++)
	{
		double x = i * 10 - 0.777;
		double* f = (double*)malloc(sizeof(double));
		*f = x;
		add_elem(tab, i * 7, (void*)f);
	}
	print_hash_table(tab);

	struct list* el;
	printf("Searching for some element by key...\n");
	for (size_t i = 20; i < 33; i++)
	{
		el = find_elem(tab, i * 7);
		if (el == NULL)
		{
			printf("the key(%d) element not found\n", i * 7);
		}
		else
		{
			printf("the key(%I64d) element found: %.15f\n", el->key.decimal, el->value.real);
		}
	}
	printf("Removing these items...\n");
	for (size_t i = 20; i < 33; i++)
	{
		del_elem(tab, i * 7);
	}
	print_hash_table(tab);
	printf("Searching for these items...\n");
	for (size_t i = 20; i < 33; i++)
	{
		el = find_elem(tab, i * 7);
		if (el == NULL)
		{
			printf("the key(%d) element not found\n", i * 7);
		}
		else
		{
			printf("the key(%I64d) element found: %.15f\n", el->key.decimal, el->value.real);
		}
	}
	free_table(tab);
}

static void test_by_string()
{
	printf("String Test Run\n");

	char months[12][10] = {
	"January", "February", "March", "April", "May",
	"June", "July", "August", "September", "October",
	"November", "December"
	};
	char days[][12] = {
	"Monday", "Tuesday", "Wednesday", "Thursday",
	"Friday", "Saturday", "Sunday"
	};

	struct table* tab = (struct table*)malloc(sizeof(struct table));
	create_table(tab, STRING_ELEM, STRING_ELEM);
	printf("Adding a string key-value pair...\n");
	for (size_t i = 0; i < 12; i++)
	{
		add_elem(tab, months[i], days[i%7]);
	}
	print_hash_table(tab);

	struct list* el;
	printf("Searching for some element by key...\n");
	for (size_t i = 2; i < 7; i++)
	{
		el = find_elem(tab, months[i]);
		if (el == NULL)
		{
			printf("the key(%s) element not found\n", months[i]);
		}
		else
		{
			printf("the key(%s) element found: %s\n", el->key.string, el->value.string);
		}
	}
	printf("Removing these items...\n");
	for (size_t i = 2; i < 7; i++)
	{
		del_elem(tab, months[i]);
	}
	print_hash_table(tab);
	printf("Searching for these items...\n");
	for (size_t i = 2; i < 7; i++)
	{
		el = find_elem(tab, months[i]);
		if (el == NULL)
		{
			printf("the key(%s) element not found\n", months[i]);
		}
		else
		{
			printf("the key(%s) element found: %s\n", el->key.string, el->value.string);
		}
	}
	free_table(tab);
}