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

struct hashtable
{
	int count;
	list* table;
};

typedef struct hashtable htable;


htable create_htable(int number)
{
	htable hashtable;
	int i;
	hashtable.count = (int)(number / 3) + 1;
	hashtable.table = malloc(hashtable.count * sizeof(list));
	for (i = 0; i < hashtable.count; ++i)
	{
		hashtable.table[i].value = NULL;
	}
	return hashtable;
}


void rebalance(htable* hashtable, int number);

void check_balance(htable* hashtable, int number);


int find_elem(int value, htable hashtable)
{
	int hash = hash_func(value, hashtable.count);
	list head = hashtable.table[hash];
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


void add_elem(int value, htable* hashtable)
{
	int hash;
	list head;
	list* arr;
	arr = hashtable->table;
	hash = hash_func(value, hashtable->count);
	head = arr[hash];
	if (head.value == NULL)
	{
		list first;
		first.value = value;
		first.next = NULL;
		hashtable->table[hash] = first;
	}
	else
	{
		list* head_1 = &(hashtable->table[hash]);
		while (head_1->next != NULL)
		{
			head_1 = head_1->next;
		}
		list* last = (list*)malloc(sizeof(list));
		head_1->next = last;
		last->value = value;
		last->next = NULL;
	}
	int number = (hashtable->count - 1) * 3;
	check_balance(hashtable, number);
}


void delete_elem(int value, htable* hashtable)
{
	int hash;
	list* head;
	hash = hash_func(value, hashtable->count);
	head = &(hashtable->table[hash]);
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


void rebalance(htable* hashtable, int number)
{
	htable rebalanced;
	int i;
	rebalanced.count = hashtable->count * 2;
	rebalanced.table = malloc((rebalanced.count) * sizeof(list));
	for (i = 0; i < rebalanced.count; ++i)
	{
		rebalanced.table[i].value = NULL;
	}
	for (i = 0; i < hashtable->count; ++i)
	{
		list head = hashtable->table[i];
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
	hashtable->count = rebalanced.count;
	hashtable->table = rebalanced.table;
}


void check_balance(htable* hashtable, int number)
{

	int kolv;
	for (int i = 0; i < hashtable->count; ++i)
	{
		kolv = 0;
		list head = hashtable->table[i];
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
			rebalance(hashtable, number);
			break;
		}
	}
}