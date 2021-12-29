#pragma warning (disable:4786)
#include <stdio.h>


typedef union header 
{
	struct 
	{
		unsigned int size;
		union header* ptr;
	} info;
	float dummy;
} header;


static int nb_alloc = 0;
static int nb_sbrk = 0;
static header sentinelle;

int size = 10; 
char* array = NULL; 

void* my_malloc(int size) 
{

	printf("\n\n----------	calling myMalloc	--------------\n\n");
	int sbrk_usage = 0;
	static header* bloc;
	static header* next_bloc;
	nb_alloc += 1;
	if (bloc == NULL) 
	{
		bloc = &sentinelle;
		sentinelle.info.size = 0;
		sentinelle.info.ptr = bloc;
	}

	header* ptr = &sentinelle;
	header* ptr_prec = &sentinelle;
	while ((ptr = ptr->info.ptr) != &sentinelle) 
	{
		ptr_prec = ptr;
		if (ptr->info.size >= (size + sizeof(header)))
			break;
	}

	if (ptr == &sentinelle) 
	{

		nb_sbrk++;
		sbrk_usage = 1;
		bloc->info.size = 800;
		bloc->info.ptr = &sentinelle;
		ptr_prec->info.ptr = bloc;
	}


	if (sbrk_usage == 0) 
	{
		bloc->info.size = size + sizeof(header);
		next_bloc = bloc + size + sizeof(header);
		next_bloc->info.size = ((ptr->info.size) - (size + sizeof(header)));
	}
	else 
	{
		next_bloc = bloc + size + sizeof(header);
		next_bloc->info.size = ((800) - (size + sizeof(header)));
		printf("PASSED: %u\n", 800 - (size + sizeof(header)));
	}
	printf("Memory is successfuly allocated\n");
	printf("size: %d\n", ptr->info.size);
	next_bloc->info.ptr = &sentinelle;
	ptr_prec->info.ptr = next_bloc;
	bloc->info.ptr = next_bloc;
	sentinelle.info.ptr = next_bloc;

	
	return bloc;
}


void my_free(char* buffer) 
{

	printf("\n\n----------	calling myFree	--------------\n\n");

	static unsigned char my_space[1024 * 1024];
	int j;
	int flag = 0;
	int allocated = 0;
	int x = *buffer;
	for (j = 0; j < size; j++) 
	{
		printf("deleted: %c [%p]\n", buffer[j], &buffer[j]);
		if (my_space[j] == *buffer) 
		{
			my_space[j] = 0x80 | *buffer;
			flag = 1;
		}
	}

	if (flag == 1) 
	{
		allocated = allocated - x;
	}
}

void* my_memcpy(void* to, const void* from, size_t n)
{
	const char* f_pointer = (const char*)from;
	char* t_pointer = (char*)to;
	for (size_t i = 0; i < n; ++i)
	{
		*(t_pointer++) = *(f_pointer++);
	}
	return to;
}

void* my_realloc(void* ptr, size_t original_length, size_t new_length) 
{

	printf("\n\n----------	calling MyRealloc	--------------\n\n");
	printf("Memory is successfuly reallocated\n");

	if (new_length == 0)
	{
		my_free((char*)ptr);
		return NULL;
	}
	else if (!ptr)
	{
		return my_malloc(new_length);
	}
	else if (new_length <= original_length)
	{
		return ptr;
	}
	else
	{
		void* ptr_new = my_malloc(new_length);
		if (ptr_new)
		{
			my_memcpy(ptr_new, ptr, original_length);
			my_free((char*)ptr);
		}
		return ptr_new;
	}
}

void show_elements(char* array, const int size) 
{

	printf("\nElemets of array (size: %d)\n", size);

	for (int i = 0; i < size; i++)
		printf("%c ", array[i]);

} 

void init() 
{

	array = (char*)my_malloc(size * sizeof(char)); 

	for (int i = 0; i < size; i++)
		array[i] = 'o'; 

	printf("\nInitial array:");
	show_elements(array, size);

	int old_size = size;
	size *= 2;

	my_realloc(array, size, old_size); 

	for (int i = old_size; i < size; i++)
		array[i] = 'f'; 

	printf("\nUpdate realloced array:");
	show_elements(array, size);

}

int main() 
{

	printf("The program testing function: myMalloc, myRealloc, myFree\n");

	init(); 

	my_free(array); 
	printf("Free successful\n");

	getchar();
	return 0; 

}