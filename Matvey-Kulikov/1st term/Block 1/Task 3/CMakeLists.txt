cmake_minimum_required(VERSION 3.20)
project(task2 C)

set(CMAKE_C_STANDARD 11)

add_executable(task2 main.c)
target_link_libraries(task2 m)
