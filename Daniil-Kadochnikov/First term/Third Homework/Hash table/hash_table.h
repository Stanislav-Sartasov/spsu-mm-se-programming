
struct hashTable
{
    int amountOfElements;
    int amountOfLists;
    int rebalance;
    int maxlength;
    struct list** lists;
};

struct hashTable* createHashTable();
int insert(int key, int count, char* element, struct hashTable* HashTable);
int find(int key, struct hashTable* hashTable);
int delete(int key, struct hashTable* hashTable);
int quit(struct hashTable* hashTable);
void show(struct hashTable* hashTable);