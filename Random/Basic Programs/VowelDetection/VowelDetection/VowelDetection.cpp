#include <iostream>

void Output(bool vowel, char input);

int main()
{
    char input;
    std::cout << "Enter a character : \n";
    std::cin >> input;

    switch (input) {
        case 'a':
            Output(true, input);
            break;
        case 'A':
            Output(true, input);
            break;
        case 'e':
            Output(true, input);
            break;
        case 'E':
            Output(true, input);
            break;
        case 'i':
            Output(true, input);
            break;
        case 'I':
            Output(true, input);
            break;
        case 'o':
            Output(true, input);
            break;
        case 'O':
            Output(true, input);
            break;
        case 'u':
            Output(true, input);
            break;
        case 'U':
            Output(true, input);
            break;
        default:
            Output(false, input);
            break;
    }
}

void Output(bool vowel, char input) {
    if (vowel) {
        std::cout << input << " is a Vowel!";
    }
    else {
        std::cout << input << " is NOT a Vowel!";
    }
}
