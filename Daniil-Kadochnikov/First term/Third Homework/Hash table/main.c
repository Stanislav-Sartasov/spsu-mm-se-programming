#define _CRT_SECURE_NO_WARNINGS

#include <stdio.h>
#include <string.h>
#include <stdlib.h>
#include "hash_table.h"



int checkErrors1(char *action)
{
	if ((strcmp(action, "INSERT\n") == 0) || (strcmp(action, "FIND\n") == 0) || (strcmp(action, "DELETE\n") == 0) || (strcmp(action, "QUIT\n") == 0))
	{
		return 0;
	}
	else
	{
		printf("You didn't enter INSERT, FIND, DELETE or QUIT, try again please.\n");
		memset(action, '\0', 7);
		return 1;
	}
}

int checkErrors2(long long int* pnumber, int *operation)
{
	if (*operation == 1 && fgetc(stdin) == '\n')
	{
		if (*pnumber > 0)
		{
			return 0;
		}
		else
		{
			printf("You didn't enter a positive integer or \"QUIT\", try again, please.\n\n");
			return 1;
		}
	}
	else if (*operation == 0)
	{
		char action[6];
		fgets(action, 6, stdin);

		if (!(strcmp(action, "QUIT\n")))
		{
			*operation = -1;
			return 0;
		}
		printf("You didn't enter a positive integer or \"QUIT\", try again, please.\n\n");
		return 1;
	}
	else
	{
		printf("You didn't enter a positive integer or \"QUIT\", try again, please.\n\n");
		return 1;
	}
}

int getKey(int* key)
{
	int operation;
	do
	{
		fseek(stdin, 0, 0);
		printf("Enter a positive integer - key value. Enter \"QUIT\" to exit to the main menu.\n>>>");
		operation = scanf("%lld", key);
	} while (checkErrors2(key, &operation));
	return operation;
}

int main()
{
	printf("The programm shows the power of hash tables.\nThere are 3 possible actions allowed\n-INSERT a key and an element;\n-FIND an element via a key;\n-DELETE a key and an element;\n-QUIT the program.\n");
	printf("Also, to check the efficiency of functions and actions, hash table will be printed out every time it subjects to changes.\n");

	createHashTable();

	int operation;
	char action[8];
	long long int key;

	while (1)
	{
		do
		{
			fseek(stdin, 0, 0);
			printf("\nEnter the action you would like to do (INSERT or FIND or DELETE or QUIT).\n>>>");
			fgets(action, 8, stdin);
		} while (checkErrors1(&action));

		printf("\n");

		if (!(strcmp(action, "INSERT\n")))
		{
			printf("You entered the INSERT mode. Enter a key, then an element to insert that to hash table or write 'QUIT' to exit the INSERT mode.\n\n");
			char symbol;
			int countChar;
			char* buffer;
			int buffSize;   //n = 1 => n * 100 = buffSize
			char* element;
			while (1)
			{
				countChar = 0;
				buffSize = 1;   //n = 1 => n * 100 = buffSize
				if ((buffer = (char*)malloc(buffSize * 100 * sizeof(char))) == NULL)
				{
					printf("\nERROR: failed to allocate memory.\n");
					return -1;
				}

				operation = getKey(&key);

				if (operation == -1)
				{
					break;
				}

				printf("\nEnter an element for the key value %lld.\nElement >>>", key);
				while ((symbol = getc(stdin)) != '\n')
				{
					countChar++;
					if (countChar < buffSize * 100)
					{
						buffer[countChar - 1] = symbol;
					}
					else
					{
						buffSize++;
						if ((buffer = (char*)realloc(buffer, buffSize * 100 * sizeof(char))) == NULL)
						{
							printf("\nERROR: failed to reallocate memory.\n");
							return -1;
						}
						buffer[countChar - 1] = symbol;
					}
				}
				if ((element = (char*)malloc((countChar + 1) * sizeof(char))) == NULL)
				{
					printf("\nERROR: failed to reallocate memory.\n");
					return -1;
				}
				for (int coefficient = 0; coefficient < countChar; coefficient++)
				{
					element[coefficient] = buffer[coefficient];
				}
				element[countChar] = '\0';

				free(buffer);
				insert(key, countChar + 1, element);
				show();
				free(element);
			}
		}
		else if (!(strcmp(action, "FIND\n")))
		{
			printf("You entered the FIND mode. Enter a key to find an element or write 'QUIT' to exit the FIND mode.\n\n");
			while (1)
			{
				operation = getKey(&key);

				if (operation == -1)
				{
					break;
				}
				find(key);
			}
		}
		else if (!(strcmp(action, "DELETE\n")))
		{
			printf("You entered the DELETE mode. Enter a key to delete an element or write 'QUIT' to exit the DELETE mode.\n\n");
			while (1)
			{

				operation = getKey(&key);

				if (operation == -1)
				{
					break;
				}
				delete(key);
				show();
			}
		}
		else if (!(strcmp(action, "QUIT\n")))
		{
			quit();
			printf("Completing the program...\n");
			exit(0);
		}
	}
}