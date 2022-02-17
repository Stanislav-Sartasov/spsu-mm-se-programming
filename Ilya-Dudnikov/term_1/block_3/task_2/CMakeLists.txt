cmake_minimum_required(VERSION 3.20)
project(task_2 C)

set(CMAKE_C_STANDARD 11)

add_executable(task_2 launcher.c hash_table.h hash_table.c tests.c tests.h)