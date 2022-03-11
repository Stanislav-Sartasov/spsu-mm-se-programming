#include <stdio.h>
#include "mmanager.h"
#include <stdint.h>

memory_manager mem_mgr;

void init()
{
	mem_mgr.space = malloc(SPACE_BYTES_SIZE);
	mem_mgr.start_block = NULL;
}

void initialize_start_block(size_t size)
{
	mem_mgr.start_block = mem_mgr.space;
	mem_mgr.start_block->previous_block = NULL;
	mem_mgr.start_block->next_block = NULL;
	mem_mgr.start_block->size = size;
}

memory_block *get_last_block()
{
	memory_block *last_block = mem_mgr.start_block;

	while (last_block->next_block != NULL)
	{
		last_block = last_block->next_block;
	}

	return last_block;
}

bool setup_new_block(memory_block *previous_block, size_t size)
{
	//Если создание нового блока с заданными параметрами приведёт к выходу за пределы памяти, выходим
	if ((char *)previous_block + sizeof(memory_block) * 2 + previous_block->size + size >
		(char *)mem_mgr.space + SPACE_BYTES_SIZE)
	{
		return false;
	}

	memory_block *new_block = previous_block + previous_block->size + sizeof(memory_block);
	new_block->size = size;
	new_block->previous_block = previous_block;
	new_block->next_block = NULL;

	previous_block->next_block = new_block;

	return true;
}

void *my_malloc(size_t size)
{

	if (size + sizeof(memory_block) > SPACE_BYTES_SIZE)
	{
		return NULL;
	}

	if (mem_mgr.start_block == NULL)
	{
		initialize_start_block(size);
		return &(mem_mgr.start_block->data);
	}
	memory_block *last_block = get_last_block();
	bool result = setup_new_block(last_block, size);

	if (!result)
	{
		return NULL;
	}
	else
	{
		return &(last_block->next_block->data);
	}

}

memory_block *get_memory_block_from_data_pointer(void *data)
{
	//Если нет ни одного блока или пытаются обратиться по нулю, то освобождать нам нечего, выходим
	if (mem_mgr.start_block == NULL || data == NULL)
	{
		return NULL;
	}
	//Вычисляем расстояние в структуре от её начала до указателя на данные и получаем блок
	int8_t distance_to_block = (int8_t *) &(mem_mgr.start_block->data) - (int8_t *) &(mem_mgr.start_block->size);

	memory_block *result_block = (int8_t *) data - distance_to_block;

	return result_block;
}

void my_free(void *data)
{

	memory_block *current_block = get_memory_block_from_data_pointer(data);

	if (current_block == NULL)
	{
		return;
	}

	// Случай, когда мы освобождаем единственный блок. Просто присваиваем NULL, при следующем malloc стартовый блок переинициализируется
	if (current_block->next_block == NULL && current_block->previous_block == NULL)
	{
		current_block = NULL;
	}
		// Случай, когда блок находится в конце. Мы тоже просто присваиваем NULL ему и говорим предыдущему блоку, что следующего больше нет.
		// Место освобождается, а потом и перезапишется.
	else if (current_block->next_block == NULL && current_block->previous_block != NULL)
	{
		current_block->previous_block->next_block = NULL;
		current_block = NULL;
	}
		//Случай, когда освобождается первый блок, но есть блоки впереди. Просто поменяем start_block на новый, идущий после того, который освободили
	else if (current_block->previous_block == NULL && current_block->next_block != NULL)
	{
		mem_mgr.start_block = current_block->next_block;
		current_block = NULL;
	}
		// Случай, когда наш блок находится между другими блоками. В таком случае сделаем его NULL, а также поменяем указатели
		// так, чтобы цепочка блоков не прервалась. В будущем, когда последующие блоки исчезнут, это пустое место сможет быть переинициализировано
	else
	{
		current_block->previous_block->next_block = current_block->next_block;
		current_block = NULL;
	}

}

void *my_realloc(void *data, size_t size)
{
	memory_block *current_block = get_memory_block_from_data_pointer(data);

	if (current_block == NULL)
	{
		return NULL;
	}

	//Если нужно просто укоротить блок, то записываем новый размер, освобождённое место в будущем перезапишется
	//Если следующего блока нет, то мы тоже просто меняем размер на новый
	//Если следующий блок есть, смотрим, достаточно ли места между текущими блоками, если да, расширяем текущий.
	if (size <= current_block->size || current_block->next_block == NULL ||
		current_block + sizeof(memory_block) + size < current_block->next_block)
	{
		current_block->size = size;
		return &(current_block->data);
	}
		//Если изменить размер блока, модифицировав текущий, не получится, то просто создадим новый, откопировав в него все данные
	else
	{
		memory_block *last_block = get_last_block();
		bool result = setup_new_block(last_block, size);

		if (!result)
		{
			return NULL;
		}

		//Побайтово скопируем данные в новый блок, после чего спокойно освобождаем старый.
		int8_t *current_data = &(last_block->next_block->data);
		int8_t *previous_data = &(current_block->data);

		for (int i = 0; i < current_block->size; ++i)
		{
			current_data[i] = previous_data[i];
		}

		return &(last_block->next_block->data);
	}
}

void free_init_space()
{
	free(mem_mgr.space);
}