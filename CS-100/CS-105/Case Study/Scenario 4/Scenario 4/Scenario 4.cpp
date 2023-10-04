// Alexander Legner - 270168960 - Scenario 4
#include <iostream>
#include <string>
#include <random>
#include <time.h>
#include "Alien.h"

int main(){
    srand(time(NULL)); // Set the random seed to the current time
    int selection = 0; // Set the selection to no selection
    Alien* aliens[6] = { 
        nullptr, nullptr, nullptr, nullptr, nullptr, nullptr // Must be nullptr for each index to allocate memory
    }; // Sets empty array of Aliens to be assigned later

    // Stage keeps track of where the user is up to in the program
    int stage = 0; // Set to start ready for first stage
    while (selection != 4) { // Loop until input for exit
        try {
            // Prompt
            std::cout << "Main Menu\n";
            std::cout << "1. Create Alien Pairs\n";
            std::cout << "2. Create Alien Offsprings\n";
            std::cout << "3. Compare Alien Offspring Prestige\n";
            std::cout << "4. Exit\n";
        
            std::cout << "Please enter your option : ";

            // Get console input
            std::string rawInput = "";
            std::getline(std::cin >> std::ws, rawInput);
        
            // Error checking
            int len = rawInput.find_last_of('.'); // Gets decimal place of input - i.e 1.4 is 1dp
            if (len > 0) { // Stops decimal inputs
                throw std::invalid_argument("Input cannot have decimals...");
            }
            try {
                selection = stoi(rawInput); // Convert input to int
            }
            catch (...) { // Catches exception and reformat it for outer try catch
                throw std::invalid_argument("Invalid input..."); // Throw for outer try catch with custom message
            }
            if (selection < 1 || selection > 4) { // Stops values outside the boundary
                throw std::invalid_argument("Invalid input...");
            }
            if (selection != stage + 1) { // Ensures input is executed in correct order
                throw std::invalid_argument("Invalid order - unable to perform operation...");
            }

            stage = selection; // Sets stage to current (VALID) selection
            selection = 0; // Resets selection

            switch (stage) // Execute code based on stage selected
            {
            case 1: { // Stage 1 - create pairs
                // Create alien 1 -> 4
                aliens[0] = new Alien((rand() % 5) + 1, (rand() % 5) + 1, 2);
                aliens[1] = new Alien((rand() % 5) + 1, (rand() % 5) + 1, 3);
                aliens[2] = new Alien((rand() % 5) + 1, (rand() % 5) + 1, 2);
                aliens[3] = new Alien((rand() % 5) + 1, (rand() % 5) + 1, 3);

                std::cout << "\nAliens 1, 2, 3 and 4 created...\n\n"; // Inform user

                break;
            }
            case 2: { // Stage 2 - create offspring
                // Create offspring aliens 5 and 6
                aliens[4] = (*aliens[0]) + (*aliens[1]);
                aliens[5] = (*aliens[2]) + (*aliens[3]);
                std::cout << "\nAliens 5 and 6 created...\n\n";
                break;
            }
            case 3: { // Stage 3 - compare offspring
                std::cout << "\nOffspring prestige comparison : \n"; // Title
                // Comparrisons in ternary format
                std::cout << "Alien 5 == Alien 6 ? "; ((*aliens[4]) == (*aliens[5])) ? std::cout << "true\n" : std::cout << "false\n";
                std::cout << "Alien 5 != Alien 6 ? "; ((*aliens[4]) != (*aliens[5])) ? std::cout << "true\n" : std::cout << "false\n";
                std::cout << "Alien 5 >  Alien 6 ? "; ((*aliens[4]) >  (*aliens[5])) ? std::cout << "true\n" : std::cout << "false\n";
                std::cout << "Alien 5 >= Alien 6 ? "; ((*aliens[4]) >= (*aliens[5])) ? std::cout << "true\n" : std::cout << "false\n";
                std::cout << "Alien 5 <  Alien 6 ? "; ((*aliens[4]) <  (*aliens[5])) ? std::cout << "true\n" : std::cout << "false\n";
                std::cout << "Alien 5 <= Alien 6 ? "; ((*aliens[4]) <= (*aliens[5])) ? std::cout << "true\n" : std::cout << "false\n";
                std::cout << std::endl; // Console spacing
                break;
            }
            case 4: { // Stage 4 - exit
                return 0; // Returns 0 for no errors
            }
            default: // Stops errors within each stage
                throw std::invalid_argument("Error - something has gone wrong - please try again...");
                break;
            }

        }
        catch (std::exception e) { // Informs users of errors and loops
            std::cout << e.what() << "\n\n";
        }
    }

    return 1; // Returns 1 as it should NOT exit here
}