#include <iostream>
#include <string>
#include <vector>
#include <string>

int main()
{
    std::cout << "Hello and welcome to the word calculator...\n";
    std::cout << "Points work as the following : \n";
    std::cout << "\n";
    std::cout << "Lowercase characters: 1 point\n";
    std::cout << "Capital characters:   2 points\n";
    std::cout << "Lowercase Vowels:     2 points\n";
    std::cout << "Capital vowels:       3 points\n";
    std::cout << "Invalid:             -1 points\n";
    std::cout << "\n";
    std::cout << "Enter your word/s to score (space as seperator) : ";
    
    std::string input = "", output = "";
    std::vector<std::string> words;
    std::getline(std::cin, input);

    int multiWord = input.find_first_of(' ');

    if (multiWord != -1) {
        size_t pos = 0;
        std::string subWord;
        while ((pos = input.find(' ')) != std::string::npos) {
            //std::cout << "test";
            subWord = input.substr(0, pos);
            words.push_back(subWord);
            input.erase(0, pos + 1);
        }
        output = input;
        words.push_back(output);
    }
    else {
        output = input;
        words.push_back(output);
    }

    std::vector<int> lc;
    std::vector<int> cc;
    std::vector<int> lv;
    std::vector<int> cv;
    std::vector<int> in;

    for (std::string word : words) {
        int lowerCases = 0, capitalCases = 0, lowerVowels = 0, capitalVowels = 0, invalid = 0;
        for (int i = 0; i < word.length(); i++) {
            switch (word[i]) {
                case 'b':
                case 'c':
                case 'd':
                case 'f':
                case 'g':
                case 'h':
                case 'j':
                case 'k':
                case 'l':
                case 'm':
                case 'n':
                case 'p':
                case 'q':
                case 'r':
                case 's':
                case 't':
                case 'v':
                case 'w':
                case 'x':
                case 'y':
                case 'z':
                    lowerCases++;
                    break;

                case 'a':
                case 'e':
                case 'i':
                case 'o':
                case 'u':
                    lowerVowels++;
                    break;

                case 'B':
                case 'C':
                case 'D':
                case 'F':
                case 'G':
                case 'H':
                case 'J':
                case 'K':
                case 'L':
                case 'M':
                case 'N':
                case 'P':
                case 'Q':
                case 'R':
                case 'S':
                case 'T':
                case 'V':
                case 'W':
                case 'X':
                case 'Y':
                case 'Z':
                    capitalCases++;
                    break;

                case 'A':
                case 'E':
                case 'I':
                case 'O':
                case 'U':
                    capitalVowels++;
                    break;

                default:
                    invalid++;
                    break;
            }
        }

        lc.push_back(lowerCases);
        cc.push_back(capitalCases);
        lv.push_back(lowerVowels);
        cv.push_back(capitalVowels);
        in.push_back(invalid);
    }

    std::cout << "\n";

    int i = 0;
    for (std::string word : words) {
        int score = 0;
        score += (lc[i] * 1);
        score += (cc[i] * 2);
        score += (lv[i] * 2);
        score += (cv[i] * 3);
        score += (in[i] * -1);

        std::cout << "\"" << words[i] << "\" Scored : " << score << " points!\n";
        std::cout << "Lowercases chars : " << lc[i] << "\n";
        std::cout << "Uppercases chars : " << cc[i] << "\n";
        std::cout << "Lowercases vowels : " << lv[i] << "\n";
        std::cout << "Uppercases chars: " << cv[i] << "\n";
        std::cout << "Invalid chars: " << in[i] << "\n";

        std::cout << "\n";
        i++;
    }
}