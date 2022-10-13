#include <iostream>

int main()
{
    int i = 0;
    std::cout << "Enter your number of iterations: ";
    std::cin >> i;

    char c = 'A';

    while (i != 0) {
        std::cout << c;
        if (i != 1) {
            std::cout << ", ";
        }
        else {
            std::cout << "\n";
        }
        c++;
        i--;
    }

    std::cout << "Int value = " << int(c) <<"\n";

    return 0;
}
