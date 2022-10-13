#include <iostream>

int main()
{
    int rows;
    std::cout << "Enter the amount of rows for the multiples of 1->5 : ";
    std::cin >> rows;

    for (int i = 0; i < rows; i++) {
        for (int j = 1; j <= 5; j++) {
            std::cout << j + (i * 5) << " ";
        }
        std::cout << std::endl;
    }
}
