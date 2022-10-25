/*
Task 5
Write a game for children. The game is called “Identify the Shapes”.  Use functions to generate shapes.
Define functions for a square & triangle without any argument and without return type
Define another function called rectangle with arguments and without return type. Pass height and width as arguments to the function.
Define one more function called calculate score with argument and with the return value. For each right answer, increment the score by 10.  
Return the score to the calling place.
Display Main Menu with Play, Final Score and Exit
Play option should call any one of shapes randomly and display it.  Provide options to the user as follows, (Hint: use random generator to choose the shape)
a. Rectangle
b. Triangle
c. Square
d. None of the above
*/

#include <iostream>
#include <string>
#include <ctime>

using std::cin;
using std::cout;
using std::string;
using std::getline;
using std::ws;
using std::endl;

// Menu selection options
enum class OPTIONS {
	None = 'n',
	Play = 'p',
	FinalScore = 'f',
	Exit = 'e'
};

// Function prototypes
void Triangle();
void Square();
void Rectangle(int height, int width);
int CalculateScore(int correctAnswers);

int main()
{
	srand((unsigned)time(NULL)); // Set random seed
	int correct = 0; // correct guesses

	cout << "Identify the Shapes\n"; // title

	OPTIONS option = OPTIONS::None; // selection for menu
	do {
		// Options
		cout << "Options : \n";
		cout << "p. Play\n";
		cout << "f. Final Score\n";
		cout << "e. Exit\n";
		cout << "Selection : ";

		// Input
		string rawInput = "";
		getline(cin >> ws, rawInput);
		char input = rawInput[0];
		if (input < 'a') {
			input += 32; // makes capitals lowercase
		}

		// Parsing
		option = (OPTIONS)input; // 
		switch (option) // Options
		{
		case OPTIONS::Play: {
			cout << endl;
			int shape = rand() % 3; // 0 or 1 or 2 (random)
			
			// Display random shape
			switch (shape)
			{
			case 0:
				Square();
				break;
			case 1:
				Triangle();
				break;
			case 2:
				Rectangle(5, 10);
				break;
			default:
				return 1; // Error if return 1 no shape or invalid shape number
			}

			// Prompt possible shapes
			cout << "a. Rectangle\n";
			cout << "b. Triangle\n";
			cout << "c. Square\n";
			cout << "d. None of the above\n";
			cout << "Your selection : ";

			// Force valid input of a, b, c, or d as chars
			bool invalidGuess = true;
			int guess = -1;
			while (invalidGuess) {
				guess = -1;
				rawInput = "";
				getline(cin >> ws, rawInput);
				char choice = rawInput[0];
				if (choice < 'a') {
					choice += 32; // Double check this
				}
				invalidGuess = false;
				switch (choice) {
				case 'a':
					guess = 2;
					break;
				case 'b':
					guess = 1;
					break;
				case 'c':
					guess = 0;
					break;
				case 'd':
					guess = 10;
					break;
				default:
					cout << "Not a valid option...\n";
					invalidGuess = true;
					break;
				}
			}

			// Check if guess is correct
			if (guess == shape) {
				cout << "\nCorrect, well done!!\n\n";
				correct++; // adds 1 correct answer to total
			}
			else {
				cout << "\nThat's not correct, better luck next time!\n\n";
			}

			break;
		}
		case OPTIONS::FinalScore: {
			int fScore = CalculateScore(correct); // Calculates final score
			cout << "\nYou have " << fScore << " points!\n\n"; // Displays final score
			break;
		}
		case OPTIONS::Exit:
			// Does nothing as it exits the loop when selected
			break;
		default:
			option = OPTIONS::None; // Ensure option is a valid type in case of invalid parse
			cout << "\nInvalid Input...\n\n"; // Warn user
			break;
		}

	} while (option != OPTIONS::Exit); // Loop if no exit

	return 0;
}

// Draws a square
void Square() {
	for (int i = 0; i < 5; i++) {
		if (i == 0 || i == 4) {
			for (int j = 0; j < 5; j++) {
				cout << "* ";
			}
			cout << endl;
		}
		else {
			cout << "*       *\n";
		}
	}
}

// Draws a triangle
void Triangle() {
	cout << "     *      \n";
	cout << "   * * *    \n";
	cout << "  * * * *   \n";
	cout << " * * * * *  \n";
	cout << "* * * * * * \n";
}

// Draws a rectangle
void Rectangle(int height, int width) {
	for (int i = 0; i < height; i++) {
		if (i == 0 || i == height-1) {
			for (int j = 0; j < width; j++) {
				cout << "* ";
			}
		}
		else {
			for (int j = 0; j < width; j++) {
				if (j == 0 || j == width - 1) {
					cout << "* ";
				}
				else {
					cout << "  ";
				}
			}
		}
		cout << endl;
	}
}

// Calculate score, correct answers * 10
int CalculateScore(int correctAnswers) {
	return correctAnswers * 10;
}