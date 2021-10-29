cmake_minimum_required(VERSION 3.20)
project(task3 C)

set(CMAKE_C_STANDARD 11)

add_executable(task3 main.c)
target_link_libraries(task3 m)
