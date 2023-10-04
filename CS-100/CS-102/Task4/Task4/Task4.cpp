// Write a program to display the following series using loops and conditional statements.

// Linkage
#include <iostream>

// Entry point of the program
int main()
{
    // Variable Declaration
    int start = 1, end = 10; 

    // Loop to display
    for (int i = start; i <= end; i++) { // i loops start -> end (inclusive) i.e i = 1->10
        if (i == start) { // checks for start value
            std::cout << i; // display only start with no '/'
        }
        else { // if not start value then output contains '/'
            std::cout << (i - 1) << "/" << i; // display 'previous/current'
        }

        if (i < end) { // checks if the value displayed is not the last to follow
            std::cout << " + "; // displays spacer ' + '
        }
    }

    return 0; // (return code 0) program completed without errors 
}

