cmake_minimum_required(VERSION 3.20)
project(Task_2__Memory_manager_ C)

set(CMAKE_C_STANDARD 11)

include_directories(.)

add_executable(Task_2__Memory_manager_
        memory_manager.c
        memory_manager.h
        main.c)
