// Write a program to find the factorial of a given integer. (E.g. 5! = 1 x 2 x 3 x 4 x5 = 120)

// Linkage
#include <iostream>

// Function Declaration
// Returns the factorial of the given value
int factorial(int num);

// Entry point of the program
int main()
{
    // Variable declaration
    int input, answer = 0;

    std::cout << "Factorial Calculation\n\n\n"; // Title of program

    // User Input
    std::cout << "Enter a number : ";
    std::cin >> input;
    std::cout << "\n\n"; // Spacing

    // Loop to display factorial calculation
    for (int i = input; i >= 1; i--) {
        std::cout << i; // outputs numbers 1 -> input
       
        // Checks if number isnt the input for the factorial
        if (i != 1) { 
            std::cout << " x "; // Outputs ' x ' inbetween each number (NOT after last)
        }
    }

    answer = factorial(input); // Calculate factorial

    std::cout << " = " << answer; // Display answer

    return 0; // (return code 0) program completed without errors 
}

// Returns the factorial of the given value
int factorial(int num) {
    // Factorial calulation using recurrsion
    if (num > 1) {
        return num * factorial(num - 1);
    }
    else {
        return 1;
    }
}
