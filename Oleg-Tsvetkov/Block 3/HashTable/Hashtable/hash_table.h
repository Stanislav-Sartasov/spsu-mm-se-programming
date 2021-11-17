#include "linked_sorted_list.h"

#ifndef HASHTABLE_HASH_TABLE_H
#define HASHTABLE_HASH_TABLE_H

#define HASH_TABLE_START_MOD 3
#define HASH_TABLE_SIZE_TO_MOD_RATIO 2

/*
 max_list_length - содержит максимальную длину списка
 array_of_lists - массив списков, каждый из которых отвечает за свой остаток. Элементы в списке храним по возрастанию,
 чтобы оптимизировать операции поиска, вставки и удаления элементов.
 list_lengths_array - массив для хранения длин списков, храним значения в нём, чтобы каждый раз не приходилось заново
 вычислять длину конкретного списка элементов.
 */
struct hash_table
{
	int current_mod;
	struct linked_sorted_list** array_of_lists;
	int* list_lengths_array;
};

int get_index_for_element(struct hash_table* table, int element);

struct linked_sorted_list** create_array_of_lists(struct hash_table* target_table);

void add_element_to_hash_table(struct hash_table** target_table, int element);

//Вернёт elem, если элемент присутствует, и not_found_value, если нет
int find_value_in_hash_table(struct hash_table* target_table, int elem, int not_found_val);

void remove_element_from_hash_table(struct hash_table* target_table, int element);

void rebalance_hash_table(struct hash_table** target_table);

void print_hash_table(struct hash_table* target_table);

struct hash_table* create_hash_table();

void free_hash_table_contents(struct hash_table* target_table);

#endif //HASHTABLE_HASH_TABLE_H
