cmake_minimum_required(VERSION 3.20)
project(Task1 C)

set(CMAKE_C_STANDARD 99)

add_executable(Task1 main.c sort/string_sort.h sort/string_sort.c mmap/strfile_mapping.h mmap/strfile_mapping.c algorithm/algorithm.c algorithm/algorithm.h)
