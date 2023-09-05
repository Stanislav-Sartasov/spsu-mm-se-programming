cmake_minimum_required(VERSION 3.20)
project(task_1 C)

set(CMAKE_C_STANDARD 11)
set(sources sys/mman.c)
set(headers sys/mman.h main.h)

add_library(mman ${sources})

SET(GCC_COVERAGE_COMPILE_FLAGS "-Wall -Wextra -Wshadow")

install (TARGETS mman RUNTIME DESTINATION bin
        LIBRARY DESTINATION lib${LIB_SUFFIX}
        ARCHIVE DESTINATION lib${LIB_SUFFIX})

install (FILES ${headers} DESTINATION include/sys)

add_executable(task_1 main.c)
target_link_libraries(task_1 mman)