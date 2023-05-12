#include <iostream>

int main()
{
    int myArray[5] = {};

    for (int i = 0; i < (sizeof(myArray) / sizeof(*myArray)); i++) {
        int input = 0;
        std::cout << "Enter value " << (i + 1) << " : ";
        std::cin >> input;
        myArray[i] = input;
    }

    std::cout << "Array = {";
    int sum = 0;
    for (int i = 0; i < (sizeof(myArray) / sizeof(*myArray)); i++) {
        std::cout << myArray[i];
        if (i == ((sizeof(myArray) / sizeof(*myArray)) - 1)) {
            std::cout << "}\n";
        }
        else {
            std::cout << ", ";
        }
        sum += myArray[i];
    }

    std::cout << "The sum of the array = " << sum << "\n";
    return 0;
}
