#define _CRT_SECURE_NO_WARNINGS
#include <stdio.h>
#include <stdlib.h>
#include <math.h>
#include "hash_table.h"



struct hashTable
{
    int amountOfElements;
    int amountOfLists;
    int rebalance;
    int maxlength;
    struct list** lists;
} newHashTable;

struct list
{
    int length;
    struct node* next;
};

struct node
{
    int key;
    int sizeOfElement;
    char* element;
    struct node* next;
};

void show()
{
    int coefficient;
    printf("\n");
    for (coefficient = 0; coefficient < newHashTable.amountOfLists; coefficient++)
    {
        struct node* current = newHashTable.lists[coefficient]->next;
        while (current != NULL)
        {
            printf("[%d %s] ", current->key, current->element);
            current = current->next;
        }
        printf("\n");
    }
    printf("\n");
}

int checkAnswer(int answer, int operation)
{
    if (operation == 1 && fgetc(stdin) == '\n' && (answer == 1 || answer == 0)) return 0;
    else
    {
        printf("The entered answer is not 0 or 1, try again please.");
        return 1;
    }
}

int substituteElement(struct node* current, struct node* node)
{
    printf("\nThe key '%d' already exists with the element '%s'.", current->key, current->element);
    int answer, operation;
    do
    {
        fseek(stdin, 0, 0);
        printf("\n\nWould you like to substitute the element of the key '%d'? (1 - yes, 0 - no)\n>>>", current->key);
        operation = scanf("%d", &answer);
    } while (checkAnswer(answer, operation));

    if (answer)
    {
        if (current->sizeOfElement != node->sizeOfElement)
        {
            if ((current->element = (char*)realloc(current->element, node->sizeOfElement * sizeof(char))) == NULL)
            {
                printf("\nERROR: failed to reallocate memory.\n");
                exit(-1);
            }

            for (int coefficient = 0; coefficient < node->sizeOfElement; coefficient++)
            {
                current->element[coefficient] = node->element[coefficient];
            }
        }
        else
        {
            for (int coefficient = 0; coefficient < node->sizeOfElement; coefficient++)
            {
                current->element[coefficient] = node->element[coefficient];
            }
        }
        current->sizeOfElement = node->sizeOfElement;
        free(node->element);
        free(node);
        return 1;
    }
    return 1;
}

int addElement(struct list* head, struct node* node)
{
    if (head->next == NULL)
    {
        head->next = node;
    }
    else
    {
        struct node* current;
        current = head->next;
        while (current->next != NULL)
        {
            if (current->key == node->key)
            {
                return substituteElement(current, node);
            }
            current = current->next;
        }
        if (current->key == node->key)
        {
            return substituteElement(current, node);
        }
        current->next = node;
    }
    head->length++;
    if (head->length > newHashTable.maxlength)
    {
        newHashTable.maxlength = head->length;
    }
    return 0;
}

struct node* createNode(int key, int sizeOfElement, char* element)
{
    struct node* node;
    if((node = (struct node*)malloc(sizeof(struct node))) == NULL)
    {
        printf("\nERROR: failed to allocate memory.\n");
        exit(-1);
    }
    node->key = key;
    node->sizeOfElement = sizeOfElement;
    if ((node->element = (char*)malloc(sizeOfElement * sizeof(char))) == NULL)
    {
        printf("\nERROR: failed to allocate memory.\n");
        exit(-1);
    }
    for (int coefficient = 0; coefficient < sizeOfElement; coefficient++)
    {
        node->element[coefficient] = element[coefficient];
    }
    node->next = NULL;
    return node;
}

int checkNewAmountOfLists()
{
    int number;
    for (number = 2; number <= (int)sqrt(newHashTable.amountOfLists) + 1; number++)
    {
        if (newHashTable.amountOfLists % number == 0 && newHashTable.amountOfLists > 2)
        {
            return 1;
        }
    }
    return 0;
}

struct list* initial()
{

    struct list* head;
    if ((head = (struct list*)malloc(sizeof(struct list))) == NULL)
    {
        printf("\nERROR: failed to allocate memory.\n");
        exit(-1);
    }
    head->length = 0;
    head->next = NULL;
    return head;
}

int rebalance()
{
    int oldAmountOfSets = newHashTable.amountOfLists;
    do
    {
        newHashTable.amountOfLists++;
    } while (checkNewAmountOfLists());

    if ((newHashTable.lists = (struct list*)realloc(newHashTable.lists, newHashTable.amountOfLists * sizeof(struct list*))) == NULL)
    {
        printf("\nERROR: failed to reallocate memory.\n");
        exit(-1);
    }
    int coefficient;
    for (coefficient = oldAmountOfSets; coefficient < newHashTable.amountOfLists; coefficient++)
    {
        newHashTable.lists[coefficient] = initial();
    }

    struct list** buffer = (struct list*)malloc(newHashTable.amountOfLists * sizeof(struct list*));
    for (coefficient = 0; coefficient < newHashTable.amountOfLists; coefficient++)
    {
        buffer[coefficient] = initial();
    }

    newHashTable.maxlength = 0;

    int set;
    for (coefficient = 0; coefficient < oldAmountOfSets; coefficient++)
    {
        struct node* current = newHashTable.lists[coefficient]->next;
        struct node* previous;
        while (current != NULL)
        {
            set = current->key % newHashTable.amountOfLists;
            addElement(buffer[set], current);
            previous = current;
            current = current->next;
            previous->next = NULL;
        }
    }

    for (coefficient = 0; coefficient < newHashTable.amountOfLists; coefficient++)
    {
        free(newHashTable.lists[coefficient]);
        newHashTable.lists[coefficient] = buffer[coefficient];
    }
    free(buffer);
}


int insert(int key, int sizeOfElement, char* element)
{
    newHashTable.amountOfElements++;
    if ((int)pow(2, newHashTable.rebalance) <= newHashTable.amountOfElements)
    {
        newHashTable.rebalance++;
    }
    if (newHashTable.amountOfLists * newHashTable.rebalance < newHashTable.amountOfElements)
    {
        rebalance();
    }
    
    int line;
    line = key % newHashTable.amountOfLists;

    struct node* nodePointer;
    nodePointer = createNode(key, sizeOfElement, element);

    int exists;
    exists = addElement(newHashTable.lists[line], nodePointer);
    if (exists)
    {
        newHashTable.amountOfElements--;
        return 0;
    }

    while (newHashTable.maxlength > newHashTable.rebalance)
    {
        rebalance();
    }
    return 0;
}

int find(int key)
{
    int line;
    line = key % newHashTable.amountOfLists;
    struct node* current = newHashTable.lists[line]->next;
    while (current != NULL)
    {
        if (current->key == key)
        {
            printf("\nThe element with the key '%d' has been found.\n", key);
            printf("The informatiion about element:\n-key: %d;\n-element: %s.\n\n", current->key, current->element);
            return 0;
        }
        current = current->next;
    }
    printf("\nThe element with the key '%d' has not been found.\n\n", key);
    return 0;
}

int delete(int key)
{
    int line;
    line = key % newHashTable.amountOfLists;
    struct node* current = newHashTable.lists[line]->next;
    if (current->key == key)
    {
        newHashTable.lists[line]->next = current->next;
        printf("The element '%s' with key number '%d' deleted.", current->element, current->key);
        free(current->element);
        free(current);
        newHashTable.amountOfElements--;
        newHashTable.lists[line]->length--;
        return 0;
    }
    else
    {
        struct node* previous;
        while (current->next != NULL)
        {
            previous = current;
            current = current->next;
            if (current->key == key)
            {
                previous->next = current->next;
                printf("The element '%s' with key number '%d' deleted.", current->element, current->key);
                free(current->element);
                free(current);
                newHashTable.amountOfElements--;
                newHashTable.lists[line]->length--;
                return 0;
            }
        }
    }
    printf("\nThe element with the key '%d' has not been found.\n", key);
    return 0;
}

int quit()
{
    int coefficient;
    for (coefficient = 0; coefficient < newHashTable.amountOfLists; coefficient++)
    {
        struct node* currentNode = newHashTable.lists[coefficient]->next;
        struct node* previous;
        while (currentNode != NULL)
        {
            free(currentNode->element);
            previous = currentNode;
            currentNode = currentNode->next;
            free(previous);
        }
        struct list* currentList = newHashTable.lists[coefficient];
        free(currentList);
    }
    free(newHashTable.lists);
    return 0;
}

void createHashTable()
{
    newHashTable.amountOfElements = 0;
    newHashTable.amountOfLists = 0;
    newHashTable.rebalance = 0;
    newHashTable.maxlength = 0;
    if ((newHashTable.lists = (struct set*)malloc(newHashTable.amountOfLists * sizeof(struct set*))) == NULL)
    {
        printf("\nERROR: failed to allocate memory.\n");
        exit(-1);
    }
}