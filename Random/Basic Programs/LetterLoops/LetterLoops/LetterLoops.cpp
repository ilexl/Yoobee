#include <iostream>

using std::cout;
using std::cin;

int main()
{
    int loopLimit;
    char finalChar, startingChar = 'a';
    cout << "Enter final letter to display in loop thing : ";
    cin >> finalChar;
    finalChar = std::tolower(finalChar);

    for (int i = (int)startingChar; i < (int)finalChar; i++) {
        char nextChar = (char)(i + 1);
        cout << "(" << (char)i << "-" << nextChar << ")";
        if (i < (int)finalChar-1) {
            cout << " + ";
        }
    }
}