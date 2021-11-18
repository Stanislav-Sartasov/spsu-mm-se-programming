#include "hash_table_function.h"
#include <stdlib.h>
#include <string.h>

int hash_fuction(char* name, int table_size)
{
	int value = 0;
	for (int i = 0; i < strlen(name); i++)
		value += name[i];
	return value % table_size;
}

void init_hash_table(all_students_list** list, int size)
{
	for (int i = 0; i < size; i++)
	{
		list[i] = (all_students_list*)malloc(sizeof(all_students_list));
		list[i]->person = NULL;
		list[i]->student_in_basket = 0;
	}
}

hash_table* create_hash_table(int size)
{
	hash_table* table = (hash_table*)malloc(sizeof(hash_table));
	table->size = size;
	table->count_elements = 0;
	table->list_hash_table = (all_students_list**)malloc(size * sizeof(all_students_list*));
	init_hash_table(table->list_hash_table, size);
	return table;
}

bool is_rebalancing_necessary(int count_now_elements, int items_in_this_basket)
{
	return (items_in_this_basket >= (count_now_elements / 2 + 1) && count_now_elements > 4) ? true : false;
}

void rebalance_hash_table(hash_table* table, int elem_in_basket)
{
	if (is_rebalancing_necessary(table->count_elements, elem_in_basket))
	{
		all_students_list** head = table->list_hash_table;
		hash_table* rebalanced_hash_table = create_hash_table(table->size * 2);
		for (int i = 0; i < table->size; i++)
		{
			if (head[i]->person != NULL)
			{
				do
				{
					add_new_student(rebalanced_hash_table, head[i]->person->name, head[i]->person->age);
					delete_student(table, head[i]->person->name, head[i]->person->age);
				} while (head[i]->person != NULL);
			}
		}
		free(table->list_hash_table);
		*table = *rebalanced_hash_table;
	}
}

all_students_list* scroll_head_to_necessary_head(hash_table* table, int table_size, char* name)
{
	for (int i = 0; i < table->size; i++)
	{
		if (hash_fuction(name, table_size) == i)
			return table->list_hash_table[i];
	}
}

void add_new_student(hash_table* table, char* name, int age)
{
	all_students_list* head = scroll_head_to_necessary_head(table, table->size, name);
	all_students_list* copy_hash_table = head;
	table->count_elements += 1;
	if (head->person == NULL)
	{
		head->person = (student*)malloc(sizeof(student));
		head->person->age = age;
		head->person->next = NULL;
		strcpy(head->person->name, name);
		head->student_in_basket = 1;
	}
	else
	{
		student* person = head->person;
		if (person->next == NULL)
		{
			if (person->age < age)
			{
				person->next = (student*)malloc(sizeof(student));
				person->next->age = age;
				person->next->next = NULL;
				strcpy(person->next->name, name);
			}
			else
			{
				student* first = (student*)malloc(sizeof(student));
				first->next = person;
				first->age = age;
				strcpy(first->name, name);
				head->person = first;
			}
			head->student_in_basket = 2;
		}
		else
		{
			while (person->next != NULL && person->next->age < age)
			{
				person = person->next;
			}
			student* next_person = person->next;
			person->next = (student*)malloc(sizeof(student));
			strcpy(person->next->name, name);
			person->next->age = age;
			person->next->next = next_person;
			rebalance_hash_table(table, ++head->student_in_basket);
		}
	}
}

int search_student_key(hash_table* table, char* name, int age)
{
	student* person = scroll_head_to_necessary_head(table, table->size, name)->person;
	if (person == NULL)
		return false;
	while (person != NULL && person->age <= age)
	{
		
		if (person->age == age && strcmp(&person->name, name) == 0)
		{
			return hash_fuction(name, table->size);
		}
		person = person->next;
	}
	return -1;
}

void print_serching_result(hash_table* table, char* name, int age)
{
	int key = search_student_key(table, name, age);
	if (key != -1)
		printf("%s, %d was founded! His key is: %d\n", name, age, key);
	else
		printf("This person wasn't founded!\n");
}

void delete_student(hash_table* table, char* name, int age)
{
	all_students_list* head = scroll_head_to_necessary_head(table, table->size, name);
	student* person = head->person;
	if (person == NULL)
		return;
	if (strcmp(&person->name, name) == 0 && person->age == age)
	{
		head->person = person->next == NULL ? NULL : person->next;
		head->student_in_basket--;
		table->count_elements--;
		return;
	}
	while (person->next != NULL && person->next->age < age)
	{
		person = person->next;
	};
	while (person->next != NULL && person->next->age == age)
	{
		if ((strcmp(&person->next->name, name) == 0))
		{
			if (person->next->next == NULL)
				head->person->next = NULL;
			else
				head->person->next = person->next->next;
			head->student_in_basket--;
			table->count_elements--;
			break;
		}
		person = person->next;
	}
}

void delete_hash_table(hash_table* table) 
{
	for (int i = 0; i < table->size; i++)
	{
		free(table->list_hash_table[i]);
	}
	free(table->list_hash_table);
}

void print_all_student(hash_table* table)
{
	for (int i = 0; i < table->size; i++)
	{
		all_students_list* head = table->list_hash_table[i];
		student* person = head->person;
		while (person != NULL)
		{
			printf("%s, %d years old in hash_table. His key(number of busket): %d\n", person->name, person->age, i);
			if (person->next == NULL)
				break;
			person = person->next;
		};
	}
}