#include <stdbool.h>

#ifdef IS_MSVC
typedef __int64 INT64;
#else
typedef __int64_t INT64;
#endif

void swap(INT64 *a, INT64 *b);

INT64 gcd(INT64 a, INT64 b);

bool is_prime(INT64 n);
