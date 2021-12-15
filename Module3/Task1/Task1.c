#include <stdio.h>
#include <string.h>
#include <stdlib.h>

#include <fcntl.h>
#include <sys/mman.h>
#include <sys/stat.h>



typedef struct string
{
    int size;
    char* str;
} string;


int comparator(const void* str1, const void* str2)
{
    string* a = (string*)str1;
    string* b = (string*)str2;

    if (a->size != b->size)
    {
        return (a->size - b->size);
    }

    int k = a->size;
    char* ptr_a = a->str, * ptr_b = b->str;

    for (; k >= 4; k -= 4, ptr_a += 4, ptr_b += 4)
    {
        int temp = memcmp(ptr_a, ptr_b, 4);

        if (temp)
        {
            return temp;
        }
    }

    int temp = memcmp(ptr_a, ptr_b, (size_t)k);

    if (temp)
    {
        return temp;
    }

    return 0;
}

void sort(char* file, char* file_out, int str_num, string* str_arr, size_t file_size)
{
    qsort((void*)str_arr, (size_t)str_num, sizeof(string), comparator);

    remove(file_out);
    int out_res = open(file_out, O_RDWR | O_CREAT | O_TRUNC);
    char* out = mmap(0, (size_t)file_size, PROT_READ | PROT_WRITE, MAP_SHARED, out_res, 0);
    ftruncate(out_res, file_size);

    for (int i = 0, k = 0; i < str_num; k += str_arr[i].size, i++)
    {
        char* c = str_arr[i].str;

        for (int j = 0; j < str_arr[i].size; j++)
        {
            out[k + j] = c[j];
        }
    }

    memcpy(file, out, (size_t)file_size);

    munmap(out, file_size);
    close(out_res);
    remove(file_out);

}

struct stat* getStatBuff(char* name)
{
    struct stat* buff = malloc(sizeof(struct stat));

    if (stat(name, buff) == -1)
    {
        printf("Фатальная ошибка! Не могу открыть файл\n");
        return NULL;
    }

    return buff;
}

char* openFile(char* name, size_t size)
{
    int fdin = open(name, O_RDWR);

    if (fdin == -1)
    {
        printf("Фатальная ошибка! Не могу открыть файл\n");
        return NULL;
    }

    char* file = mmap(NULL, size + 1, PROT_READ | PROT_WRITE, MAP_SHARED, fdin, 0);

    if (file == MAP_FAILED)
    {
        printf("Фатальная ошибка! Не создать файл\\n");
        return NULL;
    }

    return file;
}

string* getString(int str_num, char* file)
{
    string* str_arr = malloc(sizeof(string) * str_num);
    char* seek = file;

    for (int i = 0; i < str_num; i++)
    {
        str_arr[i].str = seek;
        int k = 0;

        while (*seek != '\n' && *seek != '\r')
        {
            k++;
            seek++;
        }

        k++;
        str_arr[i].size = k;

        seek++;
    }

    return str_arr;
}

int strCount(char* file, int size)
{
    if (file[size - 1] != '\n')
    {
        file[size - 1] = '\n';
    }

    char* seek = file;
    int count = 0;

    for (int i = 0; i < size; i++, seek++)
    {
        if (*seek == '\n')
            count++;
    }

    return count;
}

int main(int argc, char* argv[])
{
    system("chcp 1251");
    system("cls");

    printf("Пример запуска программы: \n");
    printf("Параметр1 Параметр2, где \n");
    printf(" - Параметр 1 - это путь к файлу исходнику(файл, которых хотите отсортировать); Пример ввода: С:\\file.txt\n");
    printf(" - Параметр 2 - это путь к файлу выгрузки (файл, результат работы рограммы);    Пример ввода: С:\\file_test.txt\n\n");

    if (argc != 3)
    {
        printf("Фатальная ошибка! Указано не верное количество параметров.");
        return 0;
    }

    char* part_to_the_file = argv[1];
    char* part_to_the_file_out = argv[2];

    struct stat* buff = getStatBuff(part_to_the_file);

    if (buff == NULL)
    {
        return 0;
    }

    char* file_in = openFile(part_to_the_file, (size_t)buff->st_size);

    if (file_in == NULL)
    {
        printf("Фатальная ошибка! Программа не смогла прочитать, указанный Вами файд.");
        return 0;
    }

    int str_num = strCount(file_in, (int)buff->st_size);

    string* str_arr = getString(str_num, file_in);

    sort(file_in, part_to_the_file_out, str_num, str_arr, (size_t)buff->st_size);

    munmap(file_in, (size_t)buff->st_size);

    return 0;
}
