#include <iostream>
#include <ctime>
#include <cstdlib>

int main()
{
    srand((unsigned)time(NULL));

    int maxValue, number = 0, amount = 5;

    std::cout << "Enter a maximum value (inclusive) for 5 random numbers : ";
    std::cin >> maxValue;

    maxValue++;

    for (int i = 0; i < amount; i++) {
        number = (rand() % maxValue);
        if (i != (amount-1)) {
            std::cout << number << ", ";
        }
        else {
            std::cout << number;
        }
    }
}
