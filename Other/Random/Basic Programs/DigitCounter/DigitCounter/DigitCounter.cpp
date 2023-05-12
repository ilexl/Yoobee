#include <iostream>
#include <string>

int main()
{
    std::string input;
    std::cout << "Enter a (whole) number to count the amount of digits : ";
    std::getline(std::cin >> std::ws, input);

    int temp = stoi(input);
    input = std::to_string(temp);

    int length = input.length();
    std::cout << input << " has " << length << " digits!";
}
