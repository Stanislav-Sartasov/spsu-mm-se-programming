#include <stdio.h>
#include <locale.h>

void swap(long long* first, long long* second) {
	long long temp = *first;
	*first = *second;
	*second = temp;
}

void clear_excess_chars() {
	char temp;
	do {
		temp = getchar();
	} while (temp != '\n' && temp != EOF);
}

long long get_number(counter) {
	short success = 0;
	long long result;
	char after_integer;
	do {
		printf("Введите %d-е число: ", counter);
		if (scanf_s("%10lld%c", &result, &after_integer, 6) != 2 || after_integer != '\n') {
			printf("Ошибка! К вводу принимаются только натуральные числа, которые не вызовут переполнение\n");
			clear_excess_chars(); //Если не удалось сосканировать число, пропускаем лишние символы до след. строки
		}
		else if (result <= 0) {
			printf("Ошибка! Введённое число должно быть натуральным\n");
		}
		else {
			success = 1;
		}

	} while (!success);
	return result;
}

int main() {
	setlocale(LC_ALL, "Russian");
	printf("Описание: Программа получает три натуральных числа от пользователя и говорит,\n");
	printf("являются ли они Пифагоровой тройкой. Если да, то программа также скажет, является ли она простой.\n");
	long long numbers[3];
	for (int i = 1; i <= 3; ++i) {
		numbers[i - 1] = get_number(i);
	}
	if (numbers[0] >= numbers[1] && numbers[0] >= numbers[2]) {
		swap(&numbers[0], &numbers[2]);
	}
	else if (numbers[1] >= numbers[0] && numbers[1] >= numbers[2]) {
		swap(&numbers[1], &numbers[2]);
	}
	if (numbers[0] * numbers[0] + numbers[1] * numbers[1] == numbers[2] * numbers[2]) {
		printf("Представленные числа являются Пифагоровой тройкой.\n");

		long long minimal;
		if (numbers[0] > numbers[1]) {
			minimal = numbers[1];
		}
		else {
			minimal = numbers[0];
		}

		short is_coprime = 1;

		for (long long i = 2; i <= minimal; ++i) {
			if (numbers[0] % i == 0 && numbers[1] % i == 0 && numbers[2] % i == 0) {
				is_coprime = 0;
				break;
			}
		}

		if (is_coprime) {
			printf("Представленные числа являются примитивной Пифагоровой тройкой (взаимно простые).\n");
		}
		else {
			printf("Представленные числа не являются примитивной Пифагоровой тройкой (не взаимно простые).\n");
		}


	}
	else {
		printf("Представленные числа не являются Пифагоровой тройкой.\n");
	}
	return 0;
}
