#include <stdbool.h>

typedef struct element
{
	int value;
	struct element *next;
} elem;

void pop(elem** head);

bool delVal(elem** head, int value);

bool findVal(elem* head, int value);

void push(elem** head, int value);
