#include <iostream>
#include <cctype>
#include <string>
#include <vector>
#include <sstream>

int main()
{
    const bool DEBUG = false;
    if (DEBUG) { std::cout << "DEGUG Enabled \n"; }

    const char vowels[] = {'a', 'e', 'i', 'o', 'u'};

    std::cout << "Enter sentence to count the vowels in it...\n";
    std::string input;
    std::getline(std::cin, input);

    int vowelCount = 0;
    for (int i = 0; i < input.length(); i++) {
        char check = input[i];
        check = tolower(check);
        for (char vowel : vowels) {
            if (check == vowel) {
                vowelCount++;
            }
        }
    }

    std::cout << "There are " << vowelCount << " vowels in the sentence (:\n";

    return 0;
}
