// Linkage
#include <iostream>

// Entry point of the program
int main()
{
    // Write a program to generate half pyramid pattern as per the size and a symbol of user choice.

    // Variable Declaration
    int size = 0;
    char symbol = ' ';

    // Get User Input
    std::cout << "Enter the pyramid size: ";
    std::cin >> size;

    std::cout << "Enter a symbol: ";
    std::cin >> symbol;


    // Display output
    std::cout << "Output structure:\n";

    // Outer loop 1 -> size(inclusive)
    for (int i = 1; i <= size; i++) {
        // Inner loop 0 -> i(exclusive)
        for (int j = 0; j < i; j++) {
            std::cout << symbol << " "; // Outputs the symbol as per loops
        }
        std::cout << std::endl; // Spacing (new line for each outer loop)
    }

    return 0; // (return code 0) program completed without errors 
}
