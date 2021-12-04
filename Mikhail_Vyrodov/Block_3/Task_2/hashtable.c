#include <stdlib.h>
#include <stdio.h>
#include <math.h>


int hash_func(unsigned int value, int number)
{
	unsigned int CRC_32_IEEE_802_3 = 0x04C11DB7;
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


int find_list_element(int value, list* head)
{
	int position = hash_func(value, 10);
	while ((head->value != value) && (head->next != NULL))
	{

		head = head->next;
	}
	if (head->next == NULL && head->value != value)
	{
		return 0;
	}
	return 1;
}


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


void add_list_element(list* head, int value)
{
	while (head->next != NULL)
	{
		head = head->next;
	}
	list* last = (list*)malloc(sizeof(list));
	head->next = last;
	last->value = value;
	last->next = NULL;
}


void delete_list_element(list* head, int value)
{
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


void add_first_list_element(list* head, int value)
{
	list first;
	first.value = value;
	first.next = NULL;
	*head = first;
}


void add_elem(int value, htable* hashtable, int number)
{
	int hash;
	list head;
	list* arr;
	arr = hashtable->table;
	hash = hash_func(value, hashtable->count);
	head = arr[hash];
	if (head.value == NULL)
	{
		add_first_list_element(&(hashtable->table[hash]), value);
	}
	else
	{
		add_list_element(&(hashtable->table[hash]), value);
	}
	check_balance(hashtable, number);
}


void delete_elem(int value, htable* hashtable)
{
	int hash;
	list head;
	list* arr;
	hash = hash_func(value, hashtable->count);
	arr = hashtable->table;
	head = arr[hash];
	delete_list_element(&(hashtable->table[hash]), value);
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