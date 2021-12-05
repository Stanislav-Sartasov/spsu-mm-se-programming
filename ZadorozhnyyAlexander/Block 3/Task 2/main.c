#include "hash_table_function.h"

void sample_test_add_del_student(hash_table* table)
{
	print_all_student(table);
	add_new_student(table, "Jacob", 15);
	add_new_student(table, "Sue", 20);
	add_new_student(table, "Mark", 99);
	add_new_student(table, "Semen", 5);
	add_new_student(table, "Margo", 13);
	add_new_student(table, "Anna", 11);
	add_new_student(table, "Marog", 40);
	print_all_student(table);
	delete_student(table, "Jacob", 15);
	printf("\n");
	print_all_student(table);
	delete_student(table, "Semen", 8);
	printf("\n");
	print_all_student(table);
	delete_student(table, "Semen", 5);
	delete_student(table, "Anna", 27);
	printf("\n");
	print_all_student(table);

	delete_student(table, "Jacob", 15);
	delete_student(table, "Sue", 20);
	delete_student(table, "Mark", 99);
	delete_student(table, "Semen", 5);
	delete_student(table, "Margo", 13);
	delete_student(table, "Anna", 11);
	delete_student(table, "Marog", 40);
}

void test_overflow(hash_table* table)
{
	add_new_student(table, "a", 13);
	add_new_student(table, "k", 11);
	add_new_student(table, "u", 11);
	add_new_student(table, "C", 11);
	printf("\n");
	print_all_student(table);
	add_new_student(table, "M", 11);
	printf("\n");
	print_all_student(table);
}

void test_search(hash_table* table)
{
	add_new_student(table, "Jacob", 15);
	add_new_student(table, "Sue", 20);
	add_new_student(table, "Mark", 99);
	add_new_student(table, "Semen", 5);
	add_new_student(table, "Margo", 13);

	print_serching_result(table, "Mark", 99);
	print_serching_result(table, "Margo", 10);
}

int main()
{
	printf("\nThis programm working with a hash-table of student\n");
	printf("Function add_new_student that have 3 parametrs {hash-table, name, age} used for add new student in table.\n");
	printf("Function delete_student that have 3 parametrs {hash-table, name, age} deleting student from table if somebody found else doing nothing.\n");
	printf("Function print_serching_result that have 3 parametrs {hash-table, name, age} shows if there is a person with the same name and age in the table\n");
	printf("Function print_all_student that have only one parametr {hash-table} write all student from hash-table if table clear it write 'Hash-table is empty!'\n");
	printf("\n");

	hash_table* hash_table = create_hash_table(10);

	sample_test_add_del_student(hash_table);
	printf("\n");
	printf("\n");

	test_overflow(hash_table);
	printf("\n");
	printf("\n");

	test_search(hash_table);
	printf("\n");
	printf("\n");

	delete_hash_table(hash_table);
	free(hash_table);
}