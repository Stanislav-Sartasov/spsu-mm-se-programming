#include <stdbool.h>

#ifndef HASHTABLE_LINKED_SORTED_LIST_H
#define HASHTABLE_LINKED_SORTED_LIST_H

struct linked_sorted_list
{
	int value;
	struct linked_sorted_list* next;
};

void add_element_to_lsl(struct linked_sorted_list** list, int element);

int get_first_from_lsl(struct linked_sorted_list** head_list);

//Возвращает 1, если удалось удалить первый элемент, и -1, если элемент единственный(что сигнализирует о том, что следует удалить сам список)
int remove_first_from_lsl_safe(struct linked_sorted_list** head_list);

//Возвращает 1, если удалось удалить элемент, -1, если элемент единственный
//(что сигнализирует о том, что следует удалить сам список)
//0, если элемента не было
int remove_element_from_lsl_safe(struct linked_sorted_list** head_list, int element);

// Вернёт 1, если значение было найдено и -1, если нет.
int find_element_in_lsl(struct linked_sorted_list** head_list, int element);

void print_lsl_elements(struct linked_sorted_list* list);

#endif //HASHTABLE_LINKED_SORTED_LIST_H
