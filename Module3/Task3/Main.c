#include"Header.h"

int main(int argc, char* argv[])
{
	system("chcp 1251");
	system("cls");

	printf("Функциональное меню: \n");
	printf(" 1 - Усредняющий фильтр 3x3 \n");
	printf(" 2 - Усредняющий фильтр Гаусса 3x3 \n");
	printf(" 3 - Фильтр Собеля по X \n");
	printf(" 4 - Фильтр Собеля по Y \n");
	printf(" 5 - Перевод изображения из цветного в оттенки серого \n\n");

	printf(" Пример ввода: Параметр1 Параметр2  Параметр3, где   \n");
	printf(" - Параметр 1 - это путь к файлу исходнику; Пример ввода: С:\\file.bpm\n");
	printf(" - Параметр 2 - Номер фильтра; Пример ввода: 2\n");
	printf(" - Параметр 3 - это путь к файлу выгрузки;  С:\\file_test.bpm\n\n");

	char* partToTheFile = argv[1];
	char* partToTheFileExport = argv[3];
	char* numberOfFilter = argv[2];

	if (readArgument(argc))
	{
		printf("\n Ошибка ввода параметров аргументов!\n");
		return 0;
	}

	if (strcmp(numberOfFilter, "1") && strcmp(numberOfFilter, "2") && strcmp(numberOfFilter, "3") && strcmp(numberOfFilter, "4") && strcmp(numberOfFilter, "5"))
	{
		printf("\nУказанного вами фильтра нет в меню\n");
		return 0;
	}

	FILE* fileImport = fopen(partToTheFile, "rb");

	struct infoHeaderOfFile headerFile;


	if (!fileValidation(&headerFile, fileImport))
	{
		fclose(fileImport);
		printf("Ошибка чтения файла импорта");
		return 0;
	}

	int p;
	struct RGBTRIPLE** rgbTriple = readArray(&p, headerFile, fileImport);

	FILE* fileExport = fopen(partToTheFileExport, "wb");

	if (!strcmp(numberOfFilter, "1"))
	{
		filterMedian(headerFile, rgbTriple, fileExport);
	}
	if (!strcmp(numberOfFilter, "2"))
	{
		filterGauss(headerFile, rgbTriple, fileExport);
	}
	if (!strcmp(numberOfFilter, "3"))
	{
		filterSobelXY(headerFile, rgbTriple, fileExport, 1);
	}
	if (!strcmp(numberOfFilter, "4"))
	{
		filterSobelXY(headerFile, rgbTriple, fileExport, 2);
	}
	if (!strcmp(numberOfFilter, "5"))
	{
		filterBlackandWhite(headerFile, rgbTriple, fileExport);
	}

	printf("Результат работы программы:\n");
	printf(" - Параметр 1 - %s\n", argv[1]);
	printf(" - Параметр 2 - %s\n", argv[2]);
	printf(" - Параметр 3 - %s\n\n", argv[3]);
	printf("Программа успешно завершила программу!\n\n");

	fclose(fileExport);
	free(rgbTriple);

	return 0;
}