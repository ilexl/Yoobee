#include <iostream>

int main()
{
    double f1=0, f2=0;
    std::cout << "The programs determines the greater of two values!\n";

    std::cout << "Value 1 : ";
    std::cin >> f1;

    std::cout << "Value 1 : ";
    std::cin >> f2;

    if (f1 > f2) {
        std::cout << "Value 1 (" << f1 << ") is GREATER than Value 2 (" << f2 << ")!";
    }
    else if (f2 > f1) {
        std::cout << "Value 2 (" << f2 << ") is GREATER than Value 1 (" << f1 << ")!";
    }
    else {
        std::cout << "Value 1 (" << f1 << ") is EQUAL to Value 2 (" << f2 << ")!";
    }

    return 0;
}

