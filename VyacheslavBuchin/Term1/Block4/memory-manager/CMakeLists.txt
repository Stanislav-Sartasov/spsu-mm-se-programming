cmake_minimum_required(VERSION 3.21)
project(memory_manager C)

set(CMAKE_C_STANDARD 99)

add_executable(memory_manager main.c memory/my_memory.c memory/my_memory.h memory/memory_block.c memory/memory_block.h test.c test.h)
