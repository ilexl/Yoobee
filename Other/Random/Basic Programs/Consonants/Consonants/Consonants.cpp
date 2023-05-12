#include <iostream>
#include <string>

int main()
{
    std::cout << "Enter sentence to count the consonants in it...\n";
    std::string input;
    std::getline(std::cin, input);

    int ConsonantCount = 0;
    for (char vowel : input) {
        vowel = tolower(vowel);

        // switch here
        switch (vowel) {
        case 'a':
            break;
        case 'e':
            break;
        case 'i':
            break;
        case 'o':
            break;
        case 'u':
            break;
        default:
            if (isalpha(vowel)) {
                ConsonantCount++;
            }
            break;
        }
    }

    std::cout << "There are "<< ConsonantCount <<" consonants in the sentence!";
}

