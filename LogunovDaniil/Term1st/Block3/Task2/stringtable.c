#include <string.h>
#include <stdlib.h>

#define HT_INITIAL_SIZE 25
#define HASH_MULT 67
#define HT_MAX_AVAILABLE_SIZE 2000000
#define HT_MAX_USED_SPACE (double)0.45 // percentage

struct stringHash
{
	int hash;
	int ifDeleted;
	char* original;
};
typedef struct stringHash stringHash;

struct stringTable
{
	int size;
	int allocated;
	stringHash** table;
};
typedef struct stringTable stringTable;

int calcStringHash(const char* str, int modulo)
{
	int hash = 0;
	int strLen = strlen(str);
	for (int i = 0; i < strLen; i++)
	{
		hash = (str[i] + hash * HASH_MULT) % modulo;
	}
	return hash;
}

stringTable* createStringTable()
{
	stringHash** newTable = calloc(HT_INITIAL_SIZE, sizeof(stringHash*));
	if (newTable == NULL)
		return NULL;
	stringTable* newST = malloc(sizeof(stringTable));
	if (newST == NULL)
	{
		free(newTable);
		return NULL;
	}

	newST->size = 0;
	newST->allocated = HT_INITIAL_SIZE;
	newST->table = newTable;

	return newST;
}

void destroyStringTable(stringTable* toDest)
{
	for (int i = 0; i < toDest->allocated; i++)
	{
		if (toDest->table[i] != NULL)
		{
			free(toDest->table[i]->original);
			free(toDest->table[i]);
		}
	}
	free(toDest->table);
	free(toDest);
}

// returns 1 for a success; 0 otherwise. Valid for every other function
int addHash(stringHash** to, int size, stringHash* strHash)
{
	int ind = strHash->hash;
	// checks if ind was on every single position available
	int looped = 0;
	// when the current pointer is NULL, we can stop searching
	while (to[ind] != NULL && !looped)
	{
		ind = (ind + 1) % size;
		if (ind == strHash->hash)
			looped = 1;
	}
	if (looped)
		return 0;
	to[ind] = strHash;
	return 1;
}

int rebalance(stringTable* strTable, int newSize)
{
	if (newSize > HT_MAX_AVAILABLE_SIZE)
		return 0;
	stringHash** newTable = calloc(newSize, sizeof(stringHash*));
	if (newTable == NULL)
		return 0;

	int success = 1;
	for (int i = 0; i < strTable->allocated; i++)
	{
		stringHash* curHash = strTable->table[i];
		if (curHash == NULL)
			continue;
		if (curHash->ifDeleted)
		{
			// here we can safely delete the no longer needed memory
			free(curHash->original);
			free(curHash);
			strTable->size--;
			continue;
		}
		curHash->hash = calcStringHash(curHash->original, newSize);
		success &= addHash(newTable, newSize, curHash);
	}

	if (!success)
	{
		free(newTable);
		return 0;
	}

	free(strTable->table);
	strTable->table = newTable;
	strTable->allocated = newSize;
	return 1;
}

int addStrToTable(stringTable* to, const char* str)
{
	if ((HT_MAX_USED_SPACE * to->allocated) < to->size)
		if (!rebalance(to, to->allocated * 2))
			return 0;

	int strLen = strlen(str) + 1;
	char* strToAdd = malloc(sizeof(char) * strLen);
	if (strToAdd == NULL)
		return 0;
	stringHash* toAdd = malloc(sizeof(stringHash));
	if (toAdd == NULL)
	{
		free(strToAdd);
		return 0;
	}

	strcpy_s(strToAdd, strLen * sizeof(char), str);
	toAdd->hash = calcStringHash(strToAdd, to->allocated);
	toAdd->ifDeleted = 0;
	toAdd->original = strToAdd;

	if (!addHash(to->table, to->allocated, toAdd))
	{
		free(toAdd->original);
		free(toAdd);
		return 0;
	}

	to->size++;
	return 1;
}

void delStrFromTable(stringTable* from, const char* str)
{
	int hash = calcStringHash(str, from->allocated);
	int ind = hash;
	while (from->table[ind] != NULL)
	{
		if (!from->table[ind]->ifDeleted && !strcmp(from->table[ind]->original, str))
		{
			from->table[ind]->ifDeleted = 1;
		}
		ind = (ind + 1) % from->allocated;
		if (ind == hash)
			return; // we went through the whole table
	}
}

// returns 1 if found; 0 otherwise
int searchForStr(stringTable* in, const char* str)
{
	int hash = calcStringHash(str, in->allocated);
	int ind = hash;
	while (in->table[ind] != NULL)
	{
		if (!in->table[ind]->ifDeleted && !strcmp(in->table[ind]->original, str))
			return 1;
		ind = (ind + 1) % in->allocated;
		if (ind == hash)
			return 0;
	}
	return 0;
}
