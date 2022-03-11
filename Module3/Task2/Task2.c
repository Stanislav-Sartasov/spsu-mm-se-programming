#include "header.h"

int main()
{
	system("chcp 1251");
	system("cls");

	int sz;

	printf("%s", "Задайте размер хэш-таблицу, которую хотите создать: ");
	scanf_s("%d", &sz);

	struct hash_table* table = malloc(sizeof(struct hash_table));

	initialize(sz, table);
	bool work_with_list = true, status_table = false;
	int enter = -1;

	while (work_with_list)
	{

		printf("-------- Функциональное меню --------\n");
		printf("* 1. Добавить элемент в таблицу     *\n");
		printf("* 2. Удалить элемент из таблицы     *\n");
		printf("* 3. Найти элемнент                 *\n");
		printf("* 4. Распечатать таблицу            *\n");
		printf("* 5. Удалить таблицу                *\n");
		printf("* 6. Выйти из программы             *\n");
		printf("-------------------------------------\n");

		scanf_s("%d", &enter);

		switch (enter)
		{
			case 1:
			{
				status_table = true;
				int key, value;
				printf("Введите ключ и значение: \n");
				scanf_s("%d %d", &key, &value);
				h_add(table, key, value);
				system("cls");
				printf("Сообщение: Новый элемент добавлен.\n\n");
				break;
			}

			case 2:
			{
				if (status_table)
				{
					int key;
					printf( "Введите ключ значения, который хотите удалить: \n\n");
					scanf_s("%d", &key);
					h_remove(table, key);
					system("cls");
				}
				else
				{
					system("cls");
					printf("Предупреждение: Ошибка!!! Хэш - таблица еще не создана! \n\n");
				}
				break;
			}
			case 3:
			{
				if (status_table)
				{
					int key, value;
					printf("Введите ключь для поиска элеменнта: \n");
					scanf_s("%d", &key);
					if (h_search(table, key, &value))
					{
						system("cls");
						printf("%s%d\n", "Сообшение: Надено значение:  ", value);
					}
					else
					{
						system("cls");
						printf("%s", "Сообшение: По данному ключу, элеенты не найдены\n\n");
					}
				}
				else
				{
					system("cls");
					printf("Предупреждение: Ошибка!!! Хэш - таблица еще не создана!\n\n ");
				}
				break;
			}
			case 4:
			{
				if (status_table)
				{
					system("cls");
					printf("%s", "Сообшение: Таблица\n");
					h_print(table);
				}
				else
				{
					system("cls");
					printf("Предупреждение: Ошибка!!! Хэш - таблица еще не создана! \n\n");
				}
				break;
			}
			case 5:
			{
				if (status_table)
				{
					h_delete(table);
					system("cls");
					printf( "Сообшение: Таблица была удалена\n\n");
				}
				else
				{
					system("cls");
					printf("Предупреждение: Ошибка!!! Хэш - таблица еще не создана!\n\n ");
				}
				status_table = false;
				break;
			}
			case 6:
			{
				work_with_list = false;
				system("cls");
				printf("Сообщение: Осуществляю выход из программы, пока!\n\n");
				break;
			}
			default:
			{
				
				system("cls");
				printf("Сообщение: Введено значение, которое не входит в указанный список!\n\n");
				break;
			}

		}

	}

	free(table);

	return 0;
}


