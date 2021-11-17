#include "hash_table.h"
#include <stdlib.h>
#include <stdio.h>

int get_index_for_element(struct hash_table* table, int element)
{
	int result = element % table->current_mod;
	return result >= 0 ? result : result + table->current_mod;
}

struct linked_sorted_list** create_array_of_lists(struct hash_table* target_table)
{
	struct linked_sorted_list** array = (struct linked_sorted_list**)calloc(target_table->current_mod, sizeof(struct linked_sorted_list*));

	for (int i = 0; i < target_table->current_mod; ++i)
	{
		array[i] = (struct linked_sorted_list*)(malloc(sizeof(struct linked_sorted_list)));
		array[i]->next = 0;
	}
	return array;
}

void add_element_to_hash_table(struct hash_table** target_table, int element)
{
	int expected_index = get_index_for_element(*target_table, element);
	int current_list_length = (*target_table)->list_lengths_array[expected_index];
	if (current_list_length < HASH_TABLE_SIZE_TO_MOD_RATIO * (*target_table)->current_mod)
	{
		if (current_list_length == 0)
		{
			(*target_table)->array_of_lists[expected_index]->value = element;
			(*target_table)->list_lengths_array[expected_index] = 1;
		}
		else
		{
			add_element_to_lsl(&((*target_table)->array_of_lists[expected_index]), element);
			(*target_table)->list_lengths_array[expected_index] += 1;
		}
		++current_list_length;
	}
	if (current_list_length == HASH_TABLE_SIZE_TO_MOD_RATIO * (*target_table)->current_mod)
	{
		rebalance_hash_table(target_table);
	}
}

//Вернёт elem, если элемент присутствует, и not_found_value, если нет
int find_value_in_hash_table(struct hash_table* target_table, int elem, int not_found_val)
{
	int expected_index = get_index_for_element(target_table, elem);
	int current_list_length = target_table->list_lengths_array[expected_index];
	if (current_list_length == 0)
	{
		return not_found_val;
	}
	else if (current_list_length > 0)
	{
		return find_element_in_lsl(&(target_table->array_of_lists[expected_index]), elem) == -1 ? not_found_val : elem;
	}
}

void remove_element_from_hash_table(struct hash_table* target_table, int element)
{
	int expected_index = get_index_for_element(target_table, element);
	int current_list_length = target_table->list_lengths_array[expected_index];
	if (current_list_length == 0)
	{
		return;
	}
	else if (current_list_length == 1 && target_table->array_of_lists[expected_index]->value == element)
	{
		target_table->list_lengths_array[expected_index] = 0; //Перестаём учитывать текущее значение единственного элемента как существующее, при следующем добавлении оно будет перезаписано
	}
	else if (current_list_length > 1)
	{
		remove_element_from_lsl_safe(&(target_table->array_of_lists[expected_index]), element);
		target_table->list_lengths_array[expected_index] -= 1;
	}
}

void rebalance_hash_table(struct hash_table** target_table)
{
	struct hash_table* table = (struct hash_table*)malloc(sizeof(struct hash_table));

	table->current_mod = (*target_table)->current_mod * HASH_TABLE_SIZE_TO_MOD_RATIO;
	table->array_of_lists = create_array_of_lists(table);
	table->list_lengths_array = (int*)malloc(table->current_mod * sizeof(int));

	for (int i = 0; i < table->current_mod; ++i)
	{
		table->list_lengths_array[i] = 0;
	}

	for (int i = 0; i < (*target_table)->current_mod; ++i)
	{
		for (int j = 0; j < (*target_table)->list_lengths_array[i]; ++j)
		{
			int value = get_first_from_lsl(&((*target_table)->array_of_lists[i]));
			remove_first_from_lsl_safe(&((*target_table)->array_of_lists[i]));
			add_element_to_hash_table(&table, value);
		}
	}

	free_hash_table_contents(*target_table);

	*target_table = table;
}

void print_hash_table(struct hash_table* target_table)
{
	for (int i = 0; i < target_table->current_mod; ++i)
	{
		printf("%d: ", i);
		if (target_table->list_lengths_array[i] > 0)
		{
			print_lsl_elements(target_table->array_of_lists[i]);
		}
		else
		{
			printf("Элементы отсутствуют\n");
		}
	}
	printf("-----------\n");
}

struct hash_table* create_hash_table()
{
	struct hash_table* table = (struct hash_table*)malloc(sizeof(struct hash_table));

	table->current_mod = HASH_TABLE_START_MOD;
	table->array_of_lists = create_array_of_lists(table);
	table->list_lengths_array = (int*)malloc(table->current_mod * sizeof(int));

	for (int i = 0; i < table->current_mod; ++i)
	{
		table->list_lengths_array[i] = 0;
	}
	return table;
}

void free_hash_table_contents(struct hash_table* target_table)
{
	for (int i = 0; i < target_table->current_mod; ++i)
	{
		free(target_table->array_of_lists[i]);
	}
	free(target_table->array_of_lists);
	free(target_table->list_lengths_array);
}
