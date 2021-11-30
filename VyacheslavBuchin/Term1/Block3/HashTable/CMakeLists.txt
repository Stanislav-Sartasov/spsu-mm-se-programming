cmake_minimum_required(VERSION 3.20)
project(HashTable C)

set(CMAKE_C_STANDARD 99)

add_executable(HashTable main.c linked_list/pair_int_list.c linked_list/pair_int_list.h hash_table/int_hash_map.c hash_table/int_hash_map.h)
