#include"Header.h"

int main(int argc, char* argv[])
{
	system("chcp 1251");
	system("cls");

	printf("Функциональное меню: \n");
	printf(" 1 - Усредняющий фильтр 3x3 \n");
	printf(" 2 - Усредняющий фильтр Гаусса 3x3 \n");
	printf(" 3 - Усредняющий фильтр Гаусса 5x5 \n");
	printf(" 4 - Фильтр Собеля по X \n");
	printf(" 5 - Фильтр Собеля по Y \n");
	printf(" 6 - Перевод изображения из цветного в оттенки серого \n\n");

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

	if (strcmp(numberOfFilter, "1") && strcmp(numberOfFilter, "2") && strcmp(numberOfFilter, "3") && strcmp(numberOfFilter, "4") && strcmp(numberOfFilter, "5") && strcmp(numberOfFilter, "6"))
	{
		printf("\nУказанного вами фильтра нет в меню\n");
		return 0;
	}

	FILE* fileImport = fopen(partToTheFile, "rb");

	struct infoHeaderOfFile* headerFile = malloc(sizeof(struct infoHeaderOfFile));

	int result = fileValidation(headerFile, fileImport);

	if (!result)
	{
		fclose(fileImport);
		printf("Ошибка чтения файла импорта");
		return 0;
	}

	int p;
	struct RGBTRIPLE** rgbTriple = readArray(&p, headerFile, fileImport);
	struct RGBTRIPLE** rgbNew = cpyArray(rgbTriple, headerFile);

	if (!strcmp(numberOfFilter, "1"))
	{
		filterMedian(headerFile->biWidth, headerFile->biHeight, rgbTriple, rgbNew);
	}
	if (!strcmp(numberOfFilter, "2"))
	{
		filterGauss(headerFile->biWidth, headerFile->biHeight, rgbTriple, rgbNew, 3);
	}
	if (!strcmp(numberOfFilter, "3"))
	{
		filterGauss(headerFile->biWidth, headerFile->biHeight, rgbTriple, rgbNew, 5);
	}
	if (!strcmp(numberOfFilter, "4"))
	{
		filterSobelXY(headerFile->biWidth, headerFile->biHeight, rgbTriple, rgbNew, 1);
	}
	if (!strcmp(numberOfFilter, "5"))
	{
		filterSobelXY(headerFile->biWidth, headerFile->biHeight, rgbTriple, rgbNew, 1);
	}
	if (!strcmp(numberOfFilter, "6"))
	{
		filterBlackandWhite(headerFile->biWidth, headerFile->biHeight, rgbTriple, rgbNew);
	}

	FILE* fileExport = fopen(partToTheFileExport, "wb");

	fwrite(headerFile, sizeof(struct infoHeaderOfFile), 1, fileExport);

	for (int i = 0; i < headerFile->biHeight; i++)
	{
		for (int j = 0; j < headerFile->biWidth; j++)
			fwrite(&rgbNew[i][j], sizeof(struct RGBTRIPLE), 1, fileExport);
		if (headerFile->biBitCount == 32)
		{
			putc(0, fileExport);
		}
		for (int k = 0; k < p; k++)
			fputc(0, fileExport);
	}

	printf("Результат работы программы:\n");
	printf(" - Параметр 1 - %s\n", argv[1]);
	printf(" - Параметр 2 - %s\n", argv[2]);
	printf(" - Параметр 3 - %s\n\n", argv[3]);
	printf("Программа успешно завершила программу!\n\n");

	fclose(fileExport);
	free(headerFile);
	free(rgbTriple);
	free(rgbNew);

	return 0;
}