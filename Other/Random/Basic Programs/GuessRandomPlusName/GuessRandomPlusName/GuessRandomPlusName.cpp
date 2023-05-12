#include <iostream>
#include <vector>
#include <ctime>
#include <string>
#include <cmath>

using std::vector;
using std::string;
using std::cout;
using std::cin;
using std::getline;
using std::abs;

int main()
{
    srand(time(NULL));
    int randomVal = 0, x = 0, range=100;
    vector<int> guessesVal;
    vector<string> guessesNames;

    cout << "Generating RANDOM Number between 0 - " << (range - 1) << "\n";
    randomVal = rand() % range;
    cout << std::endl;

    cout << "Please enter the amount of people guessing : ";
    cin >> x;

    cout << std::endl;
    for (int i = 0; i < x; i++) {
        string temp = "", name="";
        int guess = 0;

        cout << "Enter your name : ";
        getline(cin >> std::ws, temp);
        name = temp;
        guessesNames.push_back(name);

        cout << "Enter your guess "<< temp <<" : ";
        getline(cin >> std::ws, temp);
        guess = stoi(temp);
        guessesVal.push_back(guess);
        
        cout << "Your guess has been recorded as \"" << guess << "\" - " << name << "\n\n";
    }

    cout << "The correct value was : " << randomVal << "\n";
    cout << "Your guesses were as follows...\n";

    bool atLeastOneCorrect = false;
    for (int i = 0; i < guessesNames.size(); i++) {
        cout << guessesNames[i] << " : " << guessesVal[i];

        if (guessesVal[i] == randomVal) {
            cout << " CORRECT!!!!";
            atLeastOneCorrect = true;
        }

        cout << "\n";
    }

    if (atLeastOneCorrect) {
        cout << "Congrats to the winner/s!\n";
    }
    else {
        int smallestDifference = INT_MAX;
        int smallestIndex = 0;

        for (int i = 0; i < guessesVal.size(); i++) {
            int difference = abs(randomVal - guessesVal[i]);
            if (difference < smallestDifference) {
                smallestDifference = difference;
                smallestIndex = i;
            }
        }

        std::cout << "The closest guess was " << guessesNames[smallestIndex] << " : " << guessesVal[smallestIndex] << " and therefore WINS!!! \n";
    }

    return 0;
}