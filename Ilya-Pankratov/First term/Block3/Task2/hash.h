#pragma once

struct hash_table
{
	int mode;
	int number_elements;
	struct line* lines;
};

void create_hash_table(struct hash_table* my_hast_table, int mode);

int hash_function(struct hash_table* hash_table, int value);

void add_element(struct hash_table* my_hash_table, int value);

void find_element(struct hash_table* my_hash_table, int value);

void delete_element(struct hash_table* my_hash_table, int value);

void show(struct hash_table* my_hash_table);

void delete_hash_table(struct hash_table* my_hast_table);
