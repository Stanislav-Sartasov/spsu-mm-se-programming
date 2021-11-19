#pragma once

struct StringTable;
typedef struct StringTable stringTable;

stringTable* createStringTable();

void destroyStringTable(stringTable* stringTable);

int addStrToTable(stringTable* strTable, const char* str);

void delStrFromTable(stringTable* strTable, const char* str);

int searchForStr(stringTable* strTable, const char* str);
