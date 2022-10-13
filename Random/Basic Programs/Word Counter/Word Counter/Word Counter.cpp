#include <iostream>
#include <cctype>
#include <string>
#include <vector>
#include <sstream>

int main()
{
    std::cout << "Enter a paragraph followed by the enter key...\n";
    std::string input;
    std::getline(std::cin, input);

    const char separator = ' ';
    std::vector<std::string> outputArray;
    std::stringstream streamData(input);

    std::string val;

    while (std::getline(streamData, val, separator)) {
        outputArray.push_back(val);
    }

    std::cout << "There are " << outputArray.size() << " words in the sentence (:";

    return 0;
}
