
#ifndef TASK_1_MAIN_H
#define TASK_1_MAIN_H

char *get_input(char *filename, int oflag, int mode, int *needed_size);

void parse(char *mem_file, int size, char **result);

int get_file_size(char *path, int oflag, int mode, int *file_desc);

// finds necessary information about the file: how many words there are and length of the longest one
void get_file_stat(char *mem_file, int size, int *longest, int *word_count);

void free_memory(char **array, int dimension_1);

#endif //TASK_1_MAIN_H
