#pragma once

struct stringTable;
typedef struct stringTable stringTable;

stringTable* createStringTable();

void destroyStringTable(stringTable* stringTable);

int addStrToTable(stringTable* strTable, const char* str);

void delStrFromTable(stringTable* strTable, const char* str);

int searchForStr(stringTable* strTable, const char* str);
