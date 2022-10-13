#include <iostream>
#include <string>
#include <cstring>

int main()
{
    int input, length = 0;
    std::cout << "Enter number for place value analysis (max == billions): ";
    std::cin >> input;

    std::string str = std::to_string(input);
    length = str.length();

    std::reverse(str.begin(), str.end());

    for (int i = 0; i < length; i++) {
        switch (i) {
        case 0:
            std::cout << "Ones : ";
            break;
        case 1:
            std::cout << "Tens : ";
            break;
        case 2:
            std::cout << "Hundreds : ";
            break;
        case 3:
            std::cout << "Thousands : ";
            break;
        case 4:
            std::cout << "Ten Thousands : ";
            break;
        case 5:
            std::cout << "Hundred Thousands : ";
            break;
        case 6:
            std::cout << "Millions : ";
            break;
        case 7:
            std::cout << "Ten Millions : ";
            break;
        case 8:
            std::cout << "Hundred Millions : ";
            break;
        case 9:
            std::cout << "Billions :";
        default:
            std::cout << "Larger than max : ";
            break;
        }

        std::cout << str[i] << "\n";
    }
}