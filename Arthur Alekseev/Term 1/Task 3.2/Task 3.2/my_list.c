#include "my_list.h"
#include <stdlib.h>

void lst_holder_add(struct my_list_holder* lst, struct my_int_list* lst_ptr) 
{
	// If list was empty
	if (lst->value == NULL) 
	{
		lst->value = lst_ptr;
		return;
	}

	// Create new holder
	struct my_list_holder* new = (struct my_list_holder*)malloc(sizeof(struct my_list_holder));
	new->prev = NULL;
	new->value = lst_ptr;

	// Find "last" element of the list chain
	while (lst->prev != NULL)
		lst = lst->prev;
	// Insert new elem
	lst->prev = new;

}
struct my_int_list *lst_holder_get(struct my_list_holder* lst, int index) 
{

	int c = 0;
	do
	{
		if (c == index)
			return lst->value;
		lst = lst->prev;
		c++;
	} 
	while (lst->prev != NULL);
	if (c == index)
		return lst->value;

	return NULL;
}

void  lst_add(struct my_int_list *lst, int key, int value) 
{
	// If list was empty
	if (lst->key == NULL) 
	{
		lst->key = key;
		lst->value = value;
		return;
	}

	// Create new holder
	struct my_int_list* new = (struct my_int_list*)malloc(sizeof(struct my_int_list));
	new->prev = NULL;
	new->key = key;
	new->value = value;
	// Find "last" element of the list chain
	while (lst->prev != NULL)
		lst = lst->prev;
	// Insert new elem
	lst->prev = new;
}

struct my_int_list *init_list() 
{
	struct my_int_list* lst = (struct my_int_list*)malloc(sizeof(struct my_int_list));
	lst->prev = NULL;
	lst->key = NULL;
	lst->value = NULL;
	return lst;
}

struct my_list_holder* init_list_holder() 
{
	struct my_list_holder* lst = (struct my_list_holder*)malloc(sizeof(struct my_list_holder));
	lst->value = NULL;
	lst->prev = NULL;
	return lst;
}
int lst_get(struct my_int_list* lst, int key) 
{
	do 
	{
		if (lst->key == key)
			return lst->value;
		lst = lst->prev;
	} 
	while (lst->prev != NULL);
	if (lst->key == key)
		return lst->value;

	return NULL;
}

int lst_remove(struct my_int_list *lst, int key)
{
	// if head is element
	if (lst->key == key) 
	{
		struct my_int_list* to_remove = lst->prev;
		lst->prev = lst->prev->prev;
		lst->key = lst->prev->key;
		lst->value = lst->prev->value;
		
		free(to_remove);
		return 1;
	}

	// if element between head and last elem
	struct my_int_list* lst_prev = lst;
	while (lst->prev != NULL) 
	{
		if (lst->key == key) 
		{
			struct my_int_list* to_remove = lst;
			lst_prev->prev = lst->prev;
			free(to_remove);
			return 1;
		}
		lst_prev = lst;
		lst = lst->prev;
	}

	// if element is last
	if (lst->key == key) 
	{
		struct my_int_list* to_remove = lst;
		lst_prev->prev = NULL;
		free(to_remove);
		return 1;
	}
	return 0;
};

void lst_dispose(struct my_int_list* lst)
{
	do
	{
		struct my_int_list* to_remove = lst;
		lst = lst->prev;
		free(to_remove);
	}
	while (lst->prev != NULL);
}

void lst_holder_dispose(struct my_list_holder* lst)
{
	do
	{
		struct my_list_holder* to_remove = lst;
		lst = lst->prev;
		free(to_remove);
	} 
	while (lst->prev != NULL);
}
