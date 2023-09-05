cmake_minimum_required(VERSION 3.20)
project(Task3 C)

set(CMAKE_C_STANDARD 11)

add_executable(Task3 main.c filter.c)

if (CMAKE_C_COMPILER_ID MATCHES "MSVC")
    add_compile_definitions(IS_MSVC=1)
endif ()
