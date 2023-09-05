#include "header.h"

int listAdd(int value, int key, struct list* list)
{
	int counter = 0;

	struct list* list_add = list;

	while (list_add->next != NULL)
	{
		if (list_add->key == key)
		{
			list_add->value = value;
			return 0;
		}
		counter++;
		list_add = list->next;
	}

	list_add->key = key;
	list_add->value = value;
	list_add->next = malloc(sizeof(struct list));

	list_add->next->value = 0;
	list_add->next->key = 0;
	list_add->next->next = NULL;

	return counter + 1;
}

void listValueRemove(int remove_key, struct list* l_remove)
{
	if (l_remove->key == remove_key)
	{
		l_remove->key = l_remove->next->key;
		l_remove->value = l_remove->next->value;
		struct list* temp = l_remove->next;
		l_remove->next = l_remove->next->next;
		free(temp);
		return;
	}

	while ((l_remove->next != NULL) && (l_remove->next->key != remove_key))
	{
		l_remove = l_remove->next;
	}

	if (l_remove->next->key == remove_key)
	{
		struct list* temp;
		temp = l_remove->next->next;
		free(l_remove->next);
		l_remove->next = temp;
	}
}

bool listSearch(int search_key, struct list* l_search, int* num)
{
	while (l_search->next != NULL)
	{
		if (l_search->key == search_key)
		{
			*num = l_search->value;
			return true;
		}
		l_search = l_search->next;
	}
	return false;
}

void listPrint(struct list* l_print)
{

	while (l_print->next != NULL)
	{
		printf("(%d , %d) ", l_print->key, l_print->value);
		l_print = l_print->next;
	}
}

void listDelete(struct list* list)
{
	if (list == NULL) return;

	listDelete(list->next);
	free(list);

}
