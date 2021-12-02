#ifndef MY_LIST
#define MY_LIST

struct my_int_list 
{
	int value;
	int key;
	struct my_int_list* prev;
};

struct my_list_holder 
{
	struct my_int_list *value;
	struct my_list_holder* prev;
};

void lst_add(struct my_int_list* lst, int key, int value);
int lst_get(struct my_int_list* lst, int key);
int lst_remove(struct my_int_list *lst, int key);
void lst_dispose(struct my_int_list* lst);
struct my_int_list *init_list();

void lst_holder_add(struct my_list_holder* lst, struct my_int_list *lst_ptr);
void lst_holder_dispose(struct my_list_holder* lst);
struct my_int_list *lst_holder_get(struct my_list_holder* lst, int index);
struct my_list_holder* init_list_holder();

#endif