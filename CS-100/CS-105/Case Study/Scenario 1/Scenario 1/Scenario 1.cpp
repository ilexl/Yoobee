// Alexander Legner - 270168960 - Scenario 1
#include <iostream>
#include <string>
#include <vector>
#include "Yacht.h"

/// <summary>
/// entry point to the program
/// </summary>
/// <returns>program error code</returns>
int main()
{
    Yacht::resetCounter(1); // Resets the static int counter
    int yachtsAmount = 3;
    std::vector<Yacht*> yachts;
                    
    std::cout << "***********Ocean Race 2021-22************\n\n";

    // Create the yachts
    for (int i = 0; i < yachtsAmount; i++) {
        std::cout << "*****************************************\n";
        Yacht* yacht = new Yacht();
        yachts.push_back(yacht); // Add instance to list
        yacht->getPos(); // Gets user input for Lat/Long position
    }
                      
    std::cout << "\n*******Welcome to Ocean Race 2021-22*******\n\n";

    // Loop through list and displays the yachts position
    for (Yacht* yacht : yachts) {
        yacht->display(); // Display the position
        std::cout << std::endl; // Spacing in console
    }

    // Delete the no longer required memory before exiting
    for (Yacht* yacht : yachts) {
        delete(yacht); 
        yacht = nullptr; 
    }

    // Stop the console from exiting straight away
    std::cout << "Press any key to continue...";
    char ingored = std::getchar(); // character is ignored / not used
    return 0;
}