#include <iostream>

int main()
{
    std::cout << "Check value type!\n";

    char input = ' ';

    std::cout << "Enter value to check type : ";
    std::cin >> input;

    bool digit = isdigit(input);
    if (digit) {
        std::cout << "Value is a number\n";
    }
    else {
        std::cout << "Value is a char\n";
    }

    return 0;
}
