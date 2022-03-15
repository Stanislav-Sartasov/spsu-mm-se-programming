#pragma once

#define _CRT_SECURE_NO_WARNINGS
#define MAX_NAME 256

#include <stdio.h>
#include <stdbool.h>

typedef struct st
{
	struct st* next;
	char name[MAX_NAME];
	int age;
} student;

typedef struct list
{
	student* person;
	int student_in_basket;
} all_students_list;

typedef struct table
{
	struct all_students_list** list_hash_table;
	int count_elements;
	int size;

} hash_table;

hash_table* create_hash_table(int size);

void add_new_student(hash_table* table, char* name, int age);

int search_student_key(hash_table* table, char* name, int age);

void print_serching_result(hash_table* table, char* name, int age);

void delete_student(hash_table* table, char* name, int age);

void delete_hash_table(hash_table* table);

void print_all_student(hash_table* table);

