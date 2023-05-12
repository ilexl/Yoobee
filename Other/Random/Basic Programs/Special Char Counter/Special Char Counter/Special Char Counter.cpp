#include <iostream>
#include <cctype>
#include <string>
#include <vector>
#include <sstream>

int main()
{
    std::cout << "Enter a paragraph followed by the enter key...\n";
    std::string input;
    std::getline(std::cin >> std::ws, input);

    int SPECIALCharactersCounter = 0;
    for (int i = 0; i < input.length(); i++) {
        if (isalnum(input[i]) || input[i] == ' ') {

        }
        else {
            SPECIALCharactersCounter++;
        }
    }

    std::cout << "There are " << SPECIALCharactersCounter << " special characters in that string";

    return 0;
}

