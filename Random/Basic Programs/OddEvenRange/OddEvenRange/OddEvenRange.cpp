#include <iostream>

int main()
{
    int min, max, selection;
    std::cout << "Enter selection 0 -> odd, 1 -> even (0 or 1) : ";
    std::cin >> selection;

    std::cout << "Enter min value : ";
    std::cin >> min;

    std::cout << "Enter max value : ";
    std::cin >> max;

    for (int i = min; i <= max; i++) {
        if ((bool)selection) {
            // even
            if(i % 2 == 0) std::cout << i << "\n";
        }
        else {
            // odd
            if (i % 2 == 1) std::cout << i << "\n";
        }
    }

    return 0;
}
