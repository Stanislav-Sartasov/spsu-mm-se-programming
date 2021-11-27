#include <stdio.h>
#include <stdlib.h>

struct Element
{
	struct Element* next;
	int value;
};
typedef struct Element element;

int add(element* head, int value)
{
	int k = 0;
	while (head->next != NULL)
	{
		head = head->next;
		k++;
		if (head->value == value)
			return 0;
	}

	if (k >= 2)
		return -1;

	element* last = (element*)malloc(sizeof(element));
	head->next = last;
	last->value = value;
	last->next = NULL;
	
	return 1;
}

int search(element* head, int value)
{
	while (head != NULL)
	{
		if (head->value == value)
			return 1;

		head = head->next;
	}

	return 0;
}

int del(element* head, int value)
{
	int k = 1;
	while (head != NULL)
	{
		if (head->value == value)
			return k;

		head = head->next;
		k++;
	}

	return 0;
}

element* resize(element* chains, int m)
{
	m *= 2;
	element* new = (element*)calloc(m, sizeof(element));
	element* head = NULL;

	for (int i = 0; i < (int)(m / 2); i++)
	{
		head = chains[i].next;
		while (head != NULL)
		{
			add(&new[head->value % m], head->value);
			head = head->next;
		}
	}

	free(head);
	return new;
}

int main()
{
	printf("Program fills an N-size array with random integers.\nPlease, enter N: ");
	int n;
	scanf_s("%d", &n);

	int div = n;
	int* data = (int*)malloc(n * sizeof(int));
	element* chains = (element*)calloc(div, sizeof(element));
	srand(time(NULL));
	for (int i = 0; i < n; i++)
	{
		data[i] = rand() % 1001;
		while (add(&chains[data[i] % div], data[i]) == -1)
		{
			chains = resize(chains, div);
			div *= 2;
		}
	}

	printf("\nEnter an element you want to search: ");
	int valueSearch;
	scanf_s("%d", &valueSearch);

	if (search(chains[valueSearch % div].next, valueSearch))
		printf("There is such element.");
	else
		printf("There is no such element.");

	int response;
	printf("\n\nEnter 1 if you want to DELETE some element.\nEnter 2 if you want to ADD some element.\nEnter 0 if you want to SKIP this step.\n");
	printf("You want to: ");
	scanf_s("%d", &response);

	if (response == 1)
	{
		printf("\nEnter an element you want to delete: ");
		int valueDelete;
		scanf_s("%d", &valueDelete);

		int numDelete = del(chains[valueDelete % div].next, valueDelete);
		
		if (numDelete)
		{
			int k = 1;
			element* head = chains[valueDelete % div].next;
			element* last = &chains[valueDelete % div];
			while (k != numDelete)
			{
				last = head;
				head = head->next;
				k++;
			}
			last->next = head->next;
			free(head);
			printf("Done. There is no such element now.\n\n");
		}
		else
			printf("There is no such element.\n\n");
	}
	else if (response == 2)
	{
		printf("\nEnter an element you want to add: ");
		int valueAdd;
		scanf_s("%d", &valueAdd);

		while (add(&chains[valueAdd % div], valueAdd) == -1)
		{
			chains = resize(chains, div);
			div *= 2;
		}
		printf("Done.\n\n");
	}

	element* head;
	for (int i = 0; i < div; i++)
	{
		head = chains[i].next;
		while (head != NULL)
		{
			element* curr = head;
			head = head->next;
			free(curr);
		}
		free(head);
	}

	free(chains);
	free(data);

	return 0;
}