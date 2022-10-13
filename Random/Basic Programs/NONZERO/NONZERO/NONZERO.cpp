#include <iostream>

int main()
{
    double input;
    std::cout << "This program validates a NON zero value!\n";

    std::cout << "Enter value to validate as NON ZERO : ";
    std::cin >> input;

    if (input != 0) {
        std::cout << "Value is NOT zero (:";
    }
    else {
        std::cout << "Value IS zero ):";
    }

    return 0;
}

