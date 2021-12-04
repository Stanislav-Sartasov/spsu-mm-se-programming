#include"Header.h"


bool inputValidation(int *value)
{
	size_t length = 0;
	char* end = NULL;
	char buf[bufer_size] = "";

	fflush(stdout);

	if (!fgets(buf, sizeof(buf), stdin))
	{
		return false;
	}


	length = strlen(buf);

	if (buf[length - 1] == '\n')
	{
		buf[--length] = '\0';


		errno = 0;
		*value = strtod(buf, &end);


		if (length == 0)
		{
			system("cls");
			return false;
		}

		if (errno != 0 || *end != '\0')
		{
			printf("Ошибка: некорректный символ.\n");
			printf("\t%s\n", buf);
			printf("\t%*c\n", (int)(end - buf) + 1, '^');

			return false;
		}

		if (*value <=0 || *value > 6)
		{
			printf("Ошибка: данного пукта в меню - нет!\n");
			return false;
		}

	}
	else
	{
		scanf_s("%*[^\n]");
		scanf_s("%*c");
		printf("Ошибка: не вводите больше чем %d символа(ов).\n", bufer_size - 2);

		return false;
	}

	return true;
}