// Write a program to generate a simple manipulation process for basic arithmetic operations for two values. 
// Note: choose the correct data type to accommodate sufficiently large numbers and handle division by zero case when needed   

// Linkage
#include <iostream>

// Entry point of the program
int main()
{
    bool running = true; // Declare loop variable (starts true)
    do { // Loops while program is running (executes at least once)
        int option = 0; // Declare input block variable (resets to 0 on loop)
        
        // Output options & Get input
        std::cout << "Manipulation\n";
        std::cout << "************\n";
        std::cout << "1.Addition\n";
        std::cout << "2.Subtraction\n";
        std::cout << "3.Multiplication\n";
        std::cout << "4.Division\n";
        std::cout << "5.End\n";
        std::cout << std::endl;
        std::cout << "Please select your option\t:";
        std::cin >> option;

        bool validOption = true; // Assume option is valid, SWITCH will alter if it is not

        // Check if valid option
        switch (option) {
            // Valid options 1->4
            case 1:
            case 2:
            case 3:
            case 4:
                // Do nothing for valid options
                break;
            // Valid option 5 (exit)
            case 5:
                // Exit option 
                validOption = false; // fake invalid option to end loop
                running = false; // set loop variable to false to stop from looping
                break;

            default:
                // All other options are invalid
                validOption = false;
                break;
        }

        if (validOption) { // Code for valid options
            // Declare block variables
            int valueA, valueB, result = 0; 
            
            // Get input for operation
            std::cout << "\n\n"; // Console spacing

            // First value
            std::cout << "Enter first value :";
            std::cin >> valueA;

            // Second value
            std::cout << "Enter second value :";
            std::cin >> valueB;

            std::cout << std::endl; // Console spacing

            // Print relavent operation and calculate accordingly
            bool ZeroError = false; // Assume zero error is false (divide (case 4) will change if need)
            switch (option) {
                case 1: // Addition
                    std::cout << "Addition";
                    result = valueA + valueB;
                    break;
                case 2: // Subtraction
                    std::cout << "Subtraction";
                    result = valueA - valueB;
                    break;
                case 3: // Multiplication
                    std::cout << "Multiplication";
                    result = valueA * valueB;
                    break;
                case 4: // Division
                    
                    if (valueB == 0) {// Check for zero error
                        std::cout << "Cannot divide by ZERO...\n\n\n";
                        ZeroError = true;
                        break;
                    }
                    else { // No Zero Error
                        std::cout << "Division";
                        result = valueA / valueB;
                        break;
                    }
                default:
                    // No default (invalid) values can get here as per
                    // check valid option switch case
                    break;
            }

            // Check if zero error occured
            if (ZeroError) {
                continue; // Starts loop again if zero error occured
            }
            else {
                // If no zero error print result
                std::cout << "\tresult = " << result << "\n\n\n";
            }
        }
        else { // Reset loop for invalid option
            continue; // Start loop again if invalid option
        }


    } while (running); // Loop while running

    return 0; // (return code 0) program completed without errors 
}
