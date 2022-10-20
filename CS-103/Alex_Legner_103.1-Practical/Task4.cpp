/*
Task 4
Play hangman by checking the spelling of the word. If the user guesses correctly, 
the hangman becomes Walkman else an inverted man. You can allow up to 3 guesses.
You must demonstrate your skill as follows for the scenario given.
Use more than one function call. Ideally (5 functions) 
one to draw a line, one to draw the hangman, one to draw a Walkman, one to invert the hangman, and another one to play.

At least one function without parameter(s).
At least one function with parameter(s).
At least one function without any return value.
At least one function with a return value.
Assign a function return value to a variable.
*/
#include <iostream>
#include <string>

using std::cin;
using std::cout;
using std::string;
using std::getline;
using std::ws;
using std::endl;

// Functions prototypes
bool Play();
bool Guess(string prompt, char correctChar);
void DrawLine();
void DrawHangman();
void DrawWalkMan();
void DrawHangman(bool invert);
void EndGame(bool win);

int main()
{
	cout << "-------------------------\nHangman\n\n";
	EndGame(Play()); // Plays the game
	return 0;
}

// Plays hangman and returns win or lose (bool)
bool Play() {
	bool guessCorrect = false;
	DrawHangman(); 
	for (int i = 0; i < 3; i++) {
		guessCorrect = Guess("Guess the missing letter in Yo_bee : ", 'o'); // Make user guess the missing letter
		if (guessCorrect) {
			return true; // win
		}
		else {
			cout << "Sorry, try again\n"; // incorrect guess
		}
	} // loop until out of guesses

	return false; // lose
}

// draws line for hang stand
void DrawLine() {
	cout << "-----\n";
}

// draws hangman
void DrawHangman() {
	cout << "   ____\n";
	cout << "  |    |\n";
	cout << "  |    o\n";
	cout << "  |   \\|/\n";
	cout << "  |    |\n";
	cout << "  |   / \\\n";
	cout << "  |\n";
	cout << "  |\n";
	DrawLine();
}

// draws walk man
void DrawWalkMan() {
	cout << "     o\n";
	cout << "    \\|/\n";
	cout << "     |\n";
	cout << "    / \\\n";
}

// draws hangman inverted if selected
void DrawHangman(bool invert) {
	// calls correct functions for non inverted
	if (invert == false) {
		DrawHangman();
		return; // stop code from below executing
	}

	// if inverted code will execute here
	cout << "   ____\n";
	cout << "  |    |\n";
	cout << "  |   /|\\\n";
	cout << "  |    |\n";
	cout << "  |   /|\\\n";
	cout << "  |    o\n";
	cout << "  |\n";
	cout << "  |\n";
	DrawLine();
}

// promts user for guess and returns if correct
bool Guess(string prompt, char correctChar) {
	cout << prompt;
	string rawInput = "";
	getline(cin >> ws, rawInput); // stops unwanted console behaviour 
	char guess = rawInput[0]; // only return 1st character in console input
	return guess == correctChar;
}

// Displays the result of the game
void EndGame(bool win) {
	cout << endl;
	if (win) {
		cout << " Your guess is correct\n";
		DrawWalkMan();
	}
	else {
		cout << " Your guess is wrong\n";
		DrawHangman(true);
	}
}