#include <iostream>
#include <ctime>
#include <string>

// Declare functions
void SetupRandom();
int RandomNumber(int min, int max);
void RandomiseValues(int max);
void Guess(int guess);
void DisplayCurrent();
void Game(int guessesAmount, int max);

// Public variables
int valA = 1, valB = 1, valC = 1;
bool valACorrect = false, valBCorrect = false, valCCorrect = false;
bool correctlyGuessed;

int main()
{
    // Start calls setup
    SetupRandom();
    
    // Inro
    std::cout << "Hello and Welcome to a guessing game!\n";
    std::cout << "There are 3 random numbers. You have to guess them...\n";

    // First Game
    Game(7, 10);

    // Check if passed
    if (correctlyGuessed) {
        // make harder
        std::cout << "\nWell done!";
        std::cout << "\n----Level 2!----\n";
        Game(5, 15);
    }
    else {
        // Game over
        std::cout << "\nGame Over!!!\n";
        return 0;
    }

    return 0;
}

void SetupRandom() {
    srand(time(NULL));
}

/// <summary>
/// Returns a random value between min(inclusive), max(exclusive)
/// </summary>
/// <returns>int (random)</returns>
int RandomNumber(int min, int max) {
    int value = min;
    int localMax = max - min;

    value += rand() % localMax;
    return value;
}

/// <summary>
/// Randomises 3 input values between 1 and max(exclusive)
/// </summary>
void RandomiseValues(int max) {
    valA = RandomNumber(1, max);
    valB = RandomNumber(1, max);
    valC = RandomNumber(1, max);
}

/// <summary>
/// Guesses and displays
/// </summary>
/// <param name="guess"></param>
/// <returns></returns>
void Guess(int guess) {
    if (valACorrect) {
        std::cout << "Value A : " << valA << "\n";
    }
    else {
        if (valA == guess) {
            std::cout << "Value A : " << valA << " << CORRECT!" << "\n";
            valACorrect = true;
        }
        else {
            std::cout << "Value A : " << "??" << " << WRONG!!!" << "\n";
        }
    }

    if (valBCorrect) {
        std::cout << "Value B : " << valB << "\n";
    }
    else {
        if (valB == guess) {
            std::cout << "Value B : " << valB << " << CORRECT!" << "\n";
            valBCorrect = true;
        }
        else {
            std::cout << "Value B : " << "??" << " << WRONG!!!" << "\n";
        }
    }

    if (valCCorrect) {
        std::cout << "Value C : " << valC << "\n";
    }
    else {
        if (valC == guess) {
            std::cout << "Value C : " << valC << " << CORRECT!" << "\n";
            valCCorrect = true;
        }
        else {
            std::cout << "Value C : " << "??" << " << WRONG!!!" << "\n";
        }
    }

    if (valACorrect && valBCorrect && valCCorrect) {
        correctlyGuessed = true;
    }
}

/// <summary>
/// Displays current guesses
/// </summary>
void DisplayCurrent(){
    std::cout << "SUM = " << (valA + valB + valC) << "\n";
    std::cout << "PRODUCT = " << (valA * valB * valC) << "\n\n";

    if (valACorrect) {
        std::cout << "Value A : " << valA << "\n";
    }
    else {
        std::cout << "Value A : " << "??" << "\n";
    }


    if (valBCorrect) {
        std::cout << "Value B : " << valB << "\n";
    }
    else {
        std::cout << "Value B : " << "??" << "\n";
    }

    if (valCCorrect) {
        std::cout << "Value C : " << valC << "\n";
    }
    else {
        std::cout << "Value C : " << "??" << "\n";
    }
}

void Game(int guessesAmount, int max) {
    valACorrect = false;
    valBCorrect = false;
    valCCorrect = false;
    correctlyGuessed = false;


    std::cout << "Values are between 1 & " << max << "(exclusive)\n\n";

    int guesses = guessesAmount;

    // Initial Randomisation
    RandomiseValues(max);

    // Guessing
    while (guesses > 0) {
        // Allow guesses
        std::cout << "\nGuesses left : " << guesses << "\n\n";
        DisplayCurrent();

        std::string input;
        int parsedInput;
        try {
            std::cout << "\nEnter guess : ";
            std::getline(std::cin >> std::ws, input);
            if (input.find(".") != std::string::npos) {
                throw std::invalid_argument("received float value");;
            } // Check float            std::cout << "\n";
            parsedInput = stoi(input); // final parse int

            // parsedInput is valid guess
            Guess(parsedInput);

            if (correctlyGuessed) {
                break;
            }

            if (guesses == 2) {
                std::cout << "\nYou have 1 more guess...\n\n";
            }
            guesses--;
            continue; // Skip loop to start
        }
        catch (...) {
            std::cout << "Please enter a valid whole number\n\n";
        }
    }
}