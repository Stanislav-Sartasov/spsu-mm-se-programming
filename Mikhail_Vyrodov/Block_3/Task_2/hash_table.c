#include <stdlib.h>
#include <stdio.h>
#include <math.h>
#define CRC_32_IEEE_802_3 0x04C11DB7

int hash_func(unsigned int value, int number)
{
	unsigned int stepen = pow(2, 31);
	while (value > CRC_32_IEEE_802_3)
	{
		if (value & stepen)
		{
			value = value << 1;
			value = value ^ CRC_32_IEEE_802_3;
		}
		else
		{
			value = value << 1;
		}
	}
	return (value % number);
}


struct list
{
	int value;
	struct list* next;
};

typedef struct list list;

struct hash_table
{
	int count;
	list* table;
};

typedef struct hash_table htable;


htable create_htable(int number)
{
	htable hash_table;
	int i;
	hash_table.count = (int)(number / 3) + 1;
	hash_table.table = malloc(hash_table.count * sizeof(list));
	for (i = 0; i < hash_table.count; ++i)
	{
		hash_table.table[i].value = NULL;
	}
	return hash_table;
}


void rebalance(htable* hash_table, int number);

void check_balance(htable* hash_table, int number);


int find_elem(int value, htable hash_table)
{
	int hash = hash_func(value, hash_table.count);
	list head = hash_table.table[hash];
	if (head.value == NULL)
	{
		return 0;
	}
	else
	{
		while (head.next != NULL)
		{
			if (head.value == value)
			{
				return 1;
			}
			head = *head.next;
		}
	}
	return 0;
}


void add_elem(int value, htable* hash_table)
{
	int hash;
	list head;
	list* arr;
	arr = hash_table->table;
	hash = hash_func(value, hash_table->count);
	head = arr[hash];
	if (head.value == NULL)
	{
		list first;
		first.value = value;
		first.next = NULL;
		hash_table->table[hash] = first;
	}
	else
	{
		list* head_adress = &(hash_table->table[hash]);
		while (head_adress->next != NULL)
		{
			head_adress = head_adress->next;
		}
		list* last = (list*)malloc(sizeof(list));
		head_adress->next = last;
		last->value = value;
		last->next = NULL;
	}
	int number = (hash_table->count - 1) * 3;
	check_balance(hash_table, number);
}


void delete_elem(int value, htable* hash_table)
{
	int hash;
	list* head;
	hash = hash_func(value, hash_table->count);
	head = &(hash_table->table[hash]);
	if ((*head).value == value)
	{
		head = head->next;
	}
	list* prev = NULL;
	list* current = head;
	while ((current->value != value) && (current->next != NULL))
	{
		prev = current;
		current = current->next;
	}
	if (current->value == value)
	{
		prev->next = current->next;
		free(current);
	}
}


void rebalance(htable* hash_table, int number)
{
	htable rebalanced;
	int i;
	rebalanced.count = hash_table->count * 2;
	rebalanced.table = malloc((rebalanced.count) * sizeof(list));
	for (i = 0; i < rebalanced.count; ++i)
	{
		rebalanced.table[i].value = NULL;
	}
	for (i = 0; i < hash_table->count; ++i)
	{
		list head = hash_table->table[i];
		if (head.value == NULL)
		{
			add_elem(head.value, &rebalanced, number);
		}
		else
		{
			while (head.next != NULL)
			{
				add_elem(head.value, &rebalanced, number);
				head = *head.next;
			}
		}
	}
	check_balance(&rebalanced, number);
	hash_table->count = rebalanced.count;
	hash_table->table = rebalanced.table;
}


void check_balance(htable* hash_table, int number)
{

	int kolv;
	for (int i = 0; i < hash_table->count; ++i)
	{
		kolv = 0;
		list head = hash_table->table[i];
		if (head.value == NULL)
		{
			continue;
		}
		kolv = 1;
		while (head.next != NULL)
		{
			kolv++;
			head = *head.next;
		}
		if (kolv >= (int)(number / 3))
		{
			rebalance(hash_table, number);
			break;
		}
	}
}