#include <stdio.h>
#include <string.h>
#include "stringtable.h"

void greetingsMessage()
{
	printf("This program is designed to test the basic capabilities of\n");
	printf(" stringtable.h library.\n\n");
}

void testFailedMessage(const char* msg)
{
	printf("test failed: %s\n\n", msg);
}

int testAddFunctionality(stringTable* strTable, const char* str)
{
	if (!addStrToTable(strTable, str))
	{
		testFailedMessage("could not add %s to the table");
		return 0;
	}
	return 1;
}

int testSearchFunctionality(stringTable* strTable, const char* str, int expected)
{
	if (searchForStr(strTable, str) != expected)
	{
		printf("hello?");
		testFailedMessage("failed search function");
		return 0;
	}
	return 1;
}

void test()
{
	stringTable* strTable = createStringTable();
	if (strTable == NULL)
	{
		testFailedMessage("could not allocate stringTable");
		return;
	}
	printf("Successfully allocated stringTable instance!\n\n");

	char* hello = "Hello world!";
	if (!testAddFunctionality(strTable, hello))
	{
		return;
	}
	printf("Successfully added \"Hello world!\" to the table!\n\n");

	if (!testSearchFunctionality(strTable, hello, 1))
	{
		return;
	}
	printf("Successfully found \"Hello world!\" in the table!\n\n");

	// to check the rebalance function
	char num[5];
	for (int i = 0; i < 20; i++)
	{
		sprintf_s(num, 5 * sizeof(char), "%d", i);
		if (!testAddFunctionality(strTable, num))
		{
			return;
		}
	}
	printf("Successfully added numbers from 0 to 19 to the table!\n\n");

	if (!testSearchFunctionality(strTable, num, 1))
	{
		return;
	}

	sprintf_s(num, 5 * sizeof(char), "%d", 15);
	delStrFromTable(strTable, num);
	if (!testSearchFunctionality(strTable, num, 0))
	{
		return;
	}
	printf("Successfully deleted 15 from the table!\n\n");

	destroyStringTable(strTable);
	printf("Successfully destroyed stringTable instance!\n\n");

	printf("for more thorough testing available please contact the author\n\n");
}

int main()
{
	greetingsMessage();
	test();

	return 0;
}