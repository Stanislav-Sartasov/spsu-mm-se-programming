#include <stdio.h>
#include <stdlib.h>
#include <string.h>

#define HEAP_SIZE 10000
#define BLOCK_MIN_SIZE 50

#define LEFT_SON(X) ((X) * 2 + 1)
#define RIGHT_SON(X) ((X) * 2 + 2)

enum blockStatus
{
	empty,
	partial,
	allocated
};
typedef enum blockStatus blockStatus;

struct block
{
	size_t size;
	blockStatus stat;
	void* ptr;
};
typedef struct block block;

struct heap
{
	void* mem;
	block* memTree;
};
typedef struct heap heap;

heap* mainChunk;

int memoryTreeSize()
{
	int res = 0;
	int size = HEAP_SIZE;
	while (size >= BLOCK_MIN_SIZE)
	{
		res++;
		size /= 2;
	}
	return 1 << res; // 2^res
}

int isSplit(int size)
{
	return size >= BLOCK_MIN_SIZE * 2;
}

void treeBlockInit(block* tree, void* mem, int id, size_t size)
{
	tree[id].stat = empty;
	tree[id].ptr = mem;
	tree[id].size = size;
	if (isSplit(tree[id].size))
	{
		int half = size / 2;
		treeBlockInit(tree, mem, LEFT_SON(id), half);
		treeBlockInit(tree, (char*)mem + half, RIGHT_SON(id), size - half);
	}
}

void blockUpdate(block* tree, int id)
{
	if (!isSplit(tree[id].size))
		return;
	if (tree[LEFT_SON(id)].stat == empty && tree[RIGHT_SON(id)].stat == empty)
		tree[id].stat = empty;
	else
		tree[id].stat = partial;
}

void* treeGetBlock(block* tree, int id, size_t size)
{
	if (tree[id].stat == allocated)
		return NULL;
	if (size * 2 > tree[id].size || !isSplit(tree[id].size))
	{
		if (tree[id].stat != empty)
			return NULL;
		tree[id].stat = allocated;
		return tree[id].ptr;
	}
	void* freeBlock = treeGetBlock(tree, LEFT_SON(id), size);
	if (freeBlock == NULL)
		freeBlock = treeGetBlock(tree, RIGHT_SON(id), size);
	blockUpdate(tree, id);
	return freeBlock;
}

void treeFreeBlock(block* tree, int id, void* ptr)
{
	if (tree[id].ptr > ptr || (char*)tree[id].ptr + tree[id].size <= ptr)
		return;
	if (tree[id].stat == allocated)
	{
		if (tree[id].ptr == ptr)
			tree[id].stat = empty;
		return;
	}
	if (!isSplit(tree[id].size))
		return;
	treeFreeBlock(tree, LEFT_SON(id), ptr);
	treeFreeBlock(tree, RIGHT_SON(id), ptr);
	blockUpdate(tree, id);
}

size_t treeGetPtrSize(block* tree, int id, void* ptr)
{
	if (tree[id].ptr > ptr || (char*)tree[id].ptr + tree[id].size <= ptr)
		return 0;
	if (tree[id].stat == allocated)
	{
		if (tree[id].ptr == ptr)
			return tree[id].size;
		return 0;
	}
	if (!isSplit(tree[id].size))
		return 0;
	return max(treeGetPtrSize(tree, LEFT_SON(id), ptr),
		treeGetPtrSize(tree, RIGHT_SON(id), ptr));
}

heap* heapCreate()
{
	heap* new = malloc(sizeof(heap));
	if (new == NULL)
		return NULL;
	int treeSize = memoryTreeSize();
	new->mem = malloc(HEAP_SIZE * sizeof(char));
	new->memTree = malloc(treeSize * sizeof(block));
	if (new->mem == NULL || new->memTree == NULL)
	{
		free(new->mem);
		free(new->memTree);
		free(new);
		return NULL;
	}
	treeBlockInit(new->memTree, new->mem, 0, HEAP_SIZE);
	return new;
}

void heapDestroy(heap* toDest)
{
	free(toDest->mem);
	free(toDest->memTree);
	free(toDest);
}

void* myMalloc(size_t size)
{
	if (size <= 0)
		return NULL;
	return treeGetBlock(mainChunk->memTree, 0, size);
}

void myFree(void* ptr)
{
	treeFreeBlock(mainChunk->memTree, 0, ptr);
}

void* myRealloc(void* ptr, size_t newSize)
{
	void* newPtr = myMalloc(newSize);
	if (newPtr == NULL && newSize > 0)
		return NULL;
	if (newPtr != NULL)
	{
		size_t sz = treeGetPtrSize(mainChunk->memTree, 0, ptr);
		memcpy(newPtr, ptr, min(newSize, sz));
	}
	myFree(ptr);
	return newPtr;
}

void init()
{
	mainChunk = heapCreate();
}

void initFree()
{
	heapDestroy(mainChunk);
}