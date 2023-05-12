#include <iostream>
#include <cctype>
#include <string>

int main()
{
    std::string str;
    std::cout << "Please enter a string : ";
    std::getline(std::cin >> std::ws, str);


    for (int i = 0; i < size(str); i++) {
        std::cout << "Character " << i << " : " << str[i] << "\n";
    }


    return 0;
}
