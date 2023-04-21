#include"Header.h"

int main(int argc, char* argv[])
{
	system("chcp 1251");
	system("cls");

	printf("�������������� ����: \n");
	printf(" 1 - ����������� ������ 3x3 \n");
	printf(" 2 - ����������� ������ ������ 3x3 \n");
	printf(" 3 - ������ ������ �� X \n");
	printf(" 4 - ������ ������ �� Y \n");
	printf(" 5 - ������� ����������� �� �������� � ������� ������ \n\n");

	printf(" ������ �����: ��������1 ��������2  ��������3, ���   \n");
	printf(" - �������� 1 - ��� ���� � ����� ���������; ������ �����: �:\\file.bpm\n");
	printf(" - �������� 2 - ����� �������; ������ �����: 2\n");
	printf(" - �������� 3 - ��� ���� � ����� ��������;  �:\\file_test.bpm\n\n");

	char* partToTheFile = argv[1];
	char* partToTheFileExport = argv[3];
	char* numberOfFilter = argv[2];

	if (readArgument(argc))
	{
		printf("\n ������ ����� ���������� ����������!\n");
		return 0;
	}

	if (strcmp(numberOfFilter, "1") && strcmp(numberOfFilter, "2") && strcmp(numberOfFilter, "3") && strcmp(numberOfFilter, "4") && strcmp(numberOfFilter, "5"))
	{
		printf("\n���������� ���� ������� ��� � ����\n");
		return 0;
	}

	FILE* fileImport = fopen(partToTheFile, "rb");

	struct infoHeaderOfFile headerFile;


	if (!fileValidation(&headerFile, fileImport))
	{
		fclose(fileImport);
		printf("������ ������ ����� �������");
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

	printf("��������� ������ ���������:\n");
	printf(" - �������� 1 - %s\n", argv[1]);
	printf(" - �������� 2 - %s\n", argv[2]);
	printf(" - �������� 3 - %s\n\n", argv[3]);
	printf("��������� ������� ��������� ���������!\n\n");

	fclose(fileExport);
	free(rgbTriple);

	return 0;
}