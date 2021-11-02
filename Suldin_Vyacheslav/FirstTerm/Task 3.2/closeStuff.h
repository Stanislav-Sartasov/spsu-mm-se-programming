#pragma once

struct Element
{
	int key;
	int value;
	struct Element* next;
};
typedef struct Element element;

void check(element** keys, int hashKey, int* balance);

int hashFunction(int key, int balance);

void createLists(int start, int end, element** keys);

void rebalancing(element** keys, int* balance);

void add(element** head, int key, int value);

int deleteAndSearch(element** head, int key, int type);

int input(int min, int max);