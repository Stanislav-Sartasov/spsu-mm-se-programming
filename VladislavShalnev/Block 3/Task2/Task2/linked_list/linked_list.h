#ifndef LINKED_LIST_H
#define LINKED_LIST_H

// linked list item type
typedef struct linked_list_item
{
	struct linked_list_item* next;
	int key;
	int value;
} lli_t;

// linked list type
typedef struct linked_list
{
	struct linked_list_item* next;
	int length;
} ll_t;

ll_t* llcreate();

void llfree(ll_t* ll);

ll_t* lladd(ll_t* head, int key, int value);

lli_t* llfind(ll_t* head, int key);

int llremove(ll_t* head, int key);

#endif // LINKED_LIST_H
