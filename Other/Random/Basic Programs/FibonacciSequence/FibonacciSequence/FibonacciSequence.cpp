#include <iostream>

int main()
{
    int n = 0;;
    std::cout << "Enter the fibonacci sequence value n : ";
    std::cin >> n;

    long firstPrevous = 0;
    long secondPrevious = 0;
    int counter = 0;
    while (counter <= n) {
        long current = 0;
        if (firstPrevous == 0) {
            current = 1;
        }
        else {
            current = firstPrevous + secondPrevious;
        }
        secondPrevious = firstPrevous;
        firstPrevous = current;
        std::cout << current;
        if (counter != n) {
            std::cout << ", ";
        }
        counter++;
    }
}
