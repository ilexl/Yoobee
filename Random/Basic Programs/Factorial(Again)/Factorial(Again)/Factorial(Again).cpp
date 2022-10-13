#include <iostream>

int factorial(int num);

int main()
{
    int input = 0, output = 0;
    std::cout << "Enter a factorial to calculate : ";
    std::cin >> input;

    std::cout << input << "! = ";

    for (int i = 1; i <= input; i++) {
        std::cout << i;
        if (i != input) {
            std::cout << " * ";
        }
    }

    output = factorial(input);

    std::cout << " = " << output;
}

int factorial(int num) {
    if (num > 1) {
        return num * factorial(num - 1);
    }
    else {
        return 1;
    }
}
