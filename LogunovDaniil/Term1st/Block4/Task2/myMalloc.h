#pragma once

void init();

void initFree();

void* myMalloc(size_t);

void myFree(void*);

void* myRealloc(void*, size_t);