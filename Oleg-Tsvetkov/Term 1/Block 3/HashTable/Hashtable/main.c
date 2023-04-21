#include <stdio.h>
#include <malloc.h>
#include "hash_table.h"
#include <locale.h>

//Произвольно наполняем хеш-таблицу так, чтобы пока что не вызывать ребалансировку и выводим результат.
//В самом начале у нас всего 3 списка, соответственно макс. кол-во элементов в каждом - 6.
void hash_table_demonstration_1(struct hash_table** table)
{
	add_element_to_hash_table(table, 10);
	add_element_to_hash_table(table, 11);
	add_element_to_hash_table(table, 0);
	add_element_to_hash_table(table, 9);
	add_element_to_hash_table(table, 1);
	add_element_to_hash_table(table, 3);
	add_element_to_hash_table(table, 21);
	add_element_to_hash_table(table, -10);
	add_element_to_hash_table(table, 15);
	add_element_to_hash_table(table, 20);
	print_hash_table(*table);
}

//Добавляем элемент так, чтобы вызывать необходимость ребалансировки, количество списков и макс.
//кол-во элементов удваивается впоследствии
void hash_table_demonstration_2(struct hash_table** table)
{
	add_element_to_hash_table(table, 24);
	print_hash_table(*table);
}

//Удаляем несколько произвольных элементов для демонстрации работы
void hash_table_demonstration_3(struct hash_table** table)
{
	remove_element_from_hash_table(*table, 1);
	remove_element_from_hash_table(*table, 20);
	remove_element_from_hash_table(*table, 3);
	remove_element_from_hash_table(*table, 15);
	print_hash_table(*table);
}

//Демонстрация работы поиска элементов. Выведем элемент, если найден, -1 если нет.
void hash_table_demonstration_4(struct hash_table* table)
{
	printf("%d ", find_value_in_hash_table(table, -10, -1));
	printf("%d ", find_value_in_hash_table(table, 0, -1));
	printf("%d ", find_value_in_hash_table(table, 1, -1));
	printf("%d ", find_value_in_hash_table(table, 20, -1));
	printf("%d ", find_value_in_hash_table(table, 21, -1));
	printf("\n");
}

int main()
{
	setlocale(LC_ALL, "Russian");

	struct hash_table* table = create_hash_table();

	hash_table_demonstration_1(&table);
	hash_table_demonstration_2(&table);
	hash_table_demonstration_3(&table);
	hash_table_demonstration_4(table);

	free_hash_table_contents(table);
	free(table);

	return 0;
}
