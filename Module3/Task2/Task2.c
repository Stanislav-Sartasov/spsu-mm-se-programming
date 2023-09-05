#include "header.h"

int main()
{
	system("chcp 1251");
	system("cls");

	int sz;

	printf("%s", "������� ������ ���-�������, ������� ������ �������: ");
	scanf_s("%d", &sz);

	struct hash_table* table = malloc(sizeof(struct hash_table));

	initialize(sz, table);
	bool work_with_list = true, status_table = false;
	int enter = -1;

	while (work_with_list)
	{

		printf("-------- �������������� ���� --------\n");
		printf("* 1. �������� ������� � �������     *\n");
		printf("* 2. ������� ������� �� �������     *\n");
		printf("* 3. ����� ��������                 *\n");
		printf("* 4. ����������� �������            *\n");
		printf("* 5. ������� �������                *\n");
		printf("* 6. ����� �� ���������             *\n");
		printf("-------------------------------------\n");

		scanf_s("%d", &enter);

		switch (enter)
		{
			case 1:
			{
				status_table = true;
				int key, value;
				printf("������� ���� � ��������: \n");
				scanf_s("%d %d", &key, &value);
				h_add(table, key, value);
				system("cls");
				printf("���������: ����� ������� ��������.\n\n");
				break;
			}

			case 2:
			{
				if (status_table)
				{
					int key;
					printf( "������� ���� ��������, ������� ������ �������: \n\n");
					scanf_s("%d", &key);
					h_remove(table, key);
					system("cls");
				}
				else
				{
					system("cls");
					printf("��������������: ������!!! ��� - ������� ��� �� �������! \n\n");
				}
				break;
			}
			case 3:
			{
				if (status_table)
				{
					int key, value;
					printf("������� ����� ��� ������ ���������: \n");
					scanf_s("%d", &key);
					if (h_search(table, key, &value))
					{
						system("cls");
						printf("%s%d\n", "���������: ������ ��������:  ", value);
					}
					else
					{
						system("cls");
						printf("%s", "���������: �� ������� �����, ������� �� �������\n\n");
					}
				}
				else
				{
					system("cls");
					printf("��������������: ������!!! ��� - ������� ��� �� �������!\n\n ");
				}
				break;
			}
			case 4:
			{
				if (status_table)
				{
					system("cls");
					printf("%s", "���������: �������\n");
					h_print(table);
				}
				else
				{
					system("cls");
					printf("��������������: ������!!! ��� - ������� ��� �� �������! \n\n");
				}
				break;
			}
			case 5:
			{
				if (status_table)
				{
					h_delete(table);
					system("cls");
					printf( "���������: ������� ���� �������\n\n");
				}
				else
				{
					system("cls");
					printf("��������������: ������!!! ��� - ������� ��� �� �������!\n\n ");
				}
				status_table = false;
				break;
			}
			case 6:
			{
				work_with_list = false;
				system("cls");
				printf("���������: ����������� ����� �� ���������, ����!\n\n");
				break;
			}
			default:
			{
				
				system("cls");
				printf("���������: ������� ��������, ������� �� ������ � ��������� ������!\n\n");
				break;
			}

		}

	}

	free(table);

	return 0;
}


