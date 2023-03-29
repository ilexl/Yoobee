#include <iostream>
#include "Location.h"

void Location::getDegrees()
{
	bool degreesVaild = false; // Loop condition
	while (!degreesVaild) { // Loop while getting input for degrees
		std::cout << "Input the degrees between 0 and 180: "; // Prompt

		// Raw input string from console
		std::string rawInput;
		std::getline(std::cin >> std::ws, rawInput);

		try {
			// Check for decimals
			int len = rawInput.find_last_of('.'); // Get decimal places - i.e. 1.2 is has 1dp
			// Stops decimal input
			if (len > 0) {
				throw std::invalid_argument("Input cannot have decimals...");
			}

			// Check input is in range
			int testValidInput = std::stoi(rawInput); // Convert input to int value
			if (testValidInput >= 0 && testValidInput <= 180) { // Range check
				degrees = testValidInput; // Sets valid input
				degreesVaild = true; // Stop the loop
			}
			else { // Stop values that exceeds range
				throw std::invalid_argument("Input exceeds boundaries...");
			}
		}
		catch (...) { // Catch all exceptions
			std::cout << "Invalid input...\n"; // Inform user and loop
		}
	}
}

void Location::getMinutes()
{
	bool minutesVaild = false; // Loop condition
	while (!minutesVaild) { // Loop while getting input for minutes
		std::cout << "Input the minutes between 0 and 60: "; // Prompt

		// Raw input string from console
		std::string rawInput;
		std::getline(std::cin >> std::ws, rawInput);

		try {
			// Input can have decimals - no check required

			// Check is in range
			int testValidInput = std::stof(rawInput); // Convert input to float value
			if (testValidInput >= 0 && testValidInput <= 60) { // Range check
				minutes = testValidInput; // Sets valid input
				minutesVaild = true; // Stop the loop
			}
			else { // Stop values that exceeds range
				throw std::invalid_argument("Input exceeds boundaries...");
			}
		}
		catch (...) { // Catch all exceptions
			std::cout << "Invalid input...\n"; // Inform user and loop
		}
	}
}

void Location::getDirection()
{
	bool directionVaild = false; // Loop condition
	while (!directionVaild) {
		std::cout << "Input direction (E/W/N/S): "; // Prompt

		// Raw input string from console
		std::string rawInput;
		std::getline(std::cin >> std::ws, rawInput);

		try
		{
			if (rawInput.length() != 1) { // Stop input that ISN'T ONE char long
				throw std::invalid_argument("String should contain 1 character only...");
			}

			char testValidInput = rawInput[0]; // The FIRST char IS VALID at this point
			testValidInput = std::toupper(testValidInput); // Force capital letter for check

			// Check the input is a valid character
			switch (testValidInput)
			{
				// 'E', 'W', 'N', 'S' are all valid inputs with the same behaviour
			case 'E':
			case 'W':
			case 'N':
			case 'S':
				direction = testValidInput; // Set valid input
				directionVaild = true; // Stop the loop
				break;
			default: // Stops invalid characters
				throw std::invalid_argument("Character can only be 'E' 'W' 'N' 'S'...");
			}
		}
		catch (...) { // Catch all exceptions
			std::cout << "Invalid input...\n"; // Inform user and loop
		}
	}
}

void Location::getPos()
{
	getDegrees();
	getMinutes();
	getDirection();
}

void Location::display()
{
	// Outputs to the console in the format (Degrees Minutes Direction)
	std::cout << degrees << '\xF8' << minutes << '\'' << (char)(std::toupper(direction));
}

Location::Location()
{
	// null / empty values to assign memory to members

	degrees = NULL;
	minutes = NULL;
	direction = NULL;
}
