#include <malloc.h>
#include "linked_sorted_list.h"

void add_first(struct linked_sorted_list** head_list, int element)
{
	struct linked_sorted_list* new_element = (struct linked_sorted_list*)malloc(sizeof(struct linked_sorted_list));
	new_element->next = *head_list;
	new_element->value = element;
	*head_list = new_element;
}

void add_element_to_lsl(struct linked_sorted_list** head_list, int element)
{
	struct linked_sorted_list* list = *head_list;
	//Рассматриваем случай, если вставляемый элемент - наименьший
	if (element <= list->value)
	{
		add_first(head_list, element);
		return;
	}
	while (list->next != 0 && list->next->value <= element)
	{
		list = list->next;
	}
	//Рассматриваем случай, когда вставляемый элемент наибольший, помещаем в конец списка
	if (list->next == 0)
	{
		struct linked_sorted_list* new_element = (struct linked_sorted_list*)malloc(
			sizeof(struct linked_sorted_list));
		new_element->next = NULL;
		new_element->value = element;
		list->next = new_element;
	}
	//Рассматриваем случай, когда элемент не наибольший и не наименьший, вставляем в нужное место
	else if (list->next->value > element)
	{
		struct linked_sorted_list* new_element = (struct linked_sorted_list*)malloc(
			sizeof(struct linked_sorted_list));
		new_element->next = list->next;
		new_element->value = element;
		list->next = new_element;
	}
}

int get_first_from_lsl(struct linked_sorted_list** head_list)
{
	return (*head_list)->value;
}

int remove_first_from_lsl_safe(struct linked_sorted_list** head_list)
{
	if ((*head_list)->next != 0)
	{
		*head_list = (*head_list)->next;
		return 1;
	}
	return -1;
}

int remove_element_from_lsl_safe(struct linked_sorted_list** head_list, int element)
{
	//Случай, когда 1-ый элемент - искомый
	if ((*head_list)->value == element)
	{
		int result = remove_first_from_lsl_safe(head_list);
		if (result == -1)
		{
			return -1;
		}
		else
		{
			return 1;
		}
	}
	struct linked_sorted_list* list = *head_list;
	while (list->next != 0 && list->next->value < element)
	{
		list = list->next;
	}
	//Случай, когда мы дошли до конца списка и элемент не был найден
	if (list->next == 0)
	{
		return 0;
	}
	//Случай, когда мы нашли первый элемент не меньше искомого, но он оказался больше искомого.
	//Тогда мы возвращаем 0, так как список отсортирован и дальше мы его не найдём
	if (list->next->value > element)
	{
		return 0;
	}
	//Случай, когда мы нашли первый элемент не меньше искомого, и он оказался равным искомому.
	//Тогда мы возвращаем 1 и удаляем элемент.
	if (list->next->value == element)
	{
		struct linked_sorted_list* next_element = list->next->next;
		if (element == 0)
		{
			free(list->next);
			list->next = 0;
		}
		else
		{
			free(list->next);
			list->next = next_element;
		}
		return 1;
	}
}

int find_element_in_lsl(struct linked_sorted_list** head_list, int element)
{
	//Случай, когда 1-ый элемент - искомый
	if ((*head_list)->value == element)
	{
		return 1;
	}
	struct linked_sorted_list* list = *head_list;
	while (list->next != 0 && list->next->value < element)
	{
		list = list->next;
	}
	//Случай, когда мы дошли до конца списка и элемент не был найден
	if (list->next == 0)
	{
		return -1;
	}
	//Случай, когда мы нашли первый элемент не меньше искомого, но он оказался больше искомого.
	//Тогда мы возвращаем -1, так как список отсортирован и дальше мы его не найдём
	if (list->next->value > element)
	{
		return -1;
	}
	//Случай, когда мы нашли первый элемент не меньше искомого, и он оказался равным искомому.
	//Тогда мы возвращаем 1
	if (list->next->value == element)
	{
		return 1;
	}
}

void print_lsl_elements(struct linked_sorted_list* list)
{
	struct linked_sorted_list* current = list;

	printf("%d ", current->value);
	while (current->next != 0)
	{
		printf("%d ", current->next->value);
		current = current->next;
	}
	printf("\n");
}