#include <iostream>
#include <string>

int main()
{
    int rows;
    std::cout << "Enter the amount of rows for the multiples of 1->10 : ";
    std::cin >> rows;

    for (int i = 0; i < rows; i++) {
        for (int j = 1; j <= 10; j++) {
            std::string str = std::to_string(j);
            if (str.length() == 1 && i == 0) {
                str.append(1, '0');
                std::reverse(str.begin(), str.end());
                std::cout << str << " ";
            }
            else {
                std::cout << j + (i * 10) << " ";
            }
        }
        std::cout << std::endl;
    }
}

