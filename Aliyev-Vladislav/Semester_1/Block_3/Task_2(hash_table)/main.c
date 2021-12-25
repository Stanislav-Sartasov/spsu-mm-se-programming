#define _CRT_SECURE_NO_WARNINGS
#include <stdio.h>
#include <string.h>
#include "ht.h"

int main()
{
	printf("This program shows the operation of the implemented hash table.\n\n");
	uintptr_t v;
	table_t  tb;
	void* k;
	iter_t it;
	slist* p;
	char* w, b[32];
	char s[] = "0 2 1 5 8 1 3 4 1 9 6 2 7 0 2 9 5 4 7 1 8 3 6 "\
		"10 16 11 13 12 15 10 14 16 11 16 13 12 14 11";

	//Example: count the number of repetitions of words in a line.
	table_init(&tb, &cmp_str, &hash_str, NULL);
	for (w = strtok(s, " "); w != NULL; w = strtok(NULL, " ")) 
	{
		p = table_insert(&tb, w, 0);
		if (p != NULL)
		++p->val;
	}

	strcpy(b, "3");
	if ((p = table_find(&tb, b)) != NULL) 
	{
		printf("remove: %s (%u)\n", (const char*)p->key, (unsigned int)p->val);
		table_remove(&tb, b); //delete
	}

	//output
	iter_reset(&it);
	while (iter_each(&it, &tb, &k, &v))
		printf("%s (%u)\n", (const char*)k, (unsigned int)v);
	table_clear(&tb);

	//example with dynamic key
	table_init(&tb, &cmp_str, &hash_str, &free_str);
	table_insert(&tb, _strdup("LINUX"), 7777);

	memset(b, '\0', sizeof(b));
	for (v = 0; v < 256; ++v)
	{
		memset(b, rand() % 26 + 'A', 7);
		b[3] = (char)(rand() % 10) + '0';
		table_insert(&tb, _strdup(b), (v + 1));
	}

	//deleting
	for (v = 0; v < 700; ++v)
	{
		memset(b, rand() % 26 + 'A', 7);
		b[3] = (char)(rand() % 10) + '0';
		table_remove(&tb, b);
	}

	if ((p = table_find(&tb, "LINUX")) != NULL)
		p->val = 123456789;

	//output 
	iter_reset(&it);
	while (iter_each(&it, &tb, &k, &v)) 
	{
		printf("key: %s\tvalue: %d\n", (const char*)k, (int)v);
	}

	printf("\nThis program shows the operation of the implemented hash table.\n\n");
	table_clear(&tb);
	return 0;
}