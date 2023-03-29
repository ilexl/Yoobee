#include "Shape.h"
#include <iostream>
#include <string>
#include "Circle.h"
#include "Square.h"
#include "Rectangle.h"
#include "Triangle.h"

std::string Shape::getRawInput()
{
	// Console input
	std::string rawInput;
	std::getline(std::cin >> std::ws, rawInput);
	return rawInput;
}

int Shape::getInt(std::string prompt)
{
	bool inputValid = false; // Initialize loop condition
	int validInt; // Assign memory for the valid value
	while (!inputValid) { // Loop while input is invalid
		std::cout << prompt; // Prompt
		std::string rawInput = getRawInput(); // Console input
		
		try {
			// Check for decimals
			int len = rawInput.find_last_of('.'); // Gets the amount of decimal places - i.e 1.3 is 1dp 
			if (len > 0) { // Stops any decimals
				throw std::invalid_argument("Input cannot have decimals...");
			}

			validInt = std::stoi(rawInput); // Convert input to int
			inputValid = true; // Stop the loop as the input was valid 
		}
		catch (...) {
			std::cout << "Invalid input...\n\n"; // Inform users of any errors and loop
		}
	}
	return validInt;
}

float Shape::getFloat(std::string prompt)
{
	bool inputValid = false; // Initialize loop condition
	float validFloat = 0; // Assign memory for the valid value
	while (!inputValid) { // Loop while input is invalid
		std::cout << prompt; // Prompt
		std::string rawInput = getRawInput(); // Console input
		try {
			validFloat = std::stof(rawInput); // Convert input to float
			inputValid = true;// Stop the loop as the input was valid 
		}
		catch (...) {
			std::cout << "Invalid input...\n\n"; // Inform users of any errors and loop
		}
	}
	return validFloat;
}

float Shape::getFloat(std::string prompt, bool greaterThanZero)
{
	if (!greaterThanZero) { // Check to see if overload function can be used
		return getFloat(prompt); // Use overload function to return a valid value
	}
	else {
		float checkFloat = getFloat(prompt); // Get a valid value from overload function
		if (!(checkFloat > 0)) { // Check if value is NOT within conditions
			std::cout << "Invalid input...\n\n"; // Inform user
			return getFloat(prompt, greaterThanZero); // Try again using recursion to return a valid input
		}
		else {
			return checkFloat;
		}
	}
}

Shape* Shape::menu(int& selection)
{
	// Title
	std::cout << "********************\n";
	std::cout << "Shapes Calculator\n";
	std::cout << "********************\n";

	std::cout << std::endl; // Console spacing

	// Prompt options
	std::cout << "1. Square\n";
	std::cout << "2. Rectangle\n";
	std::cout << "3. Triangle\n";
	std::cout << "4. Circle\n";
	std::cout << "5. Exit\n";

	std::cout << std::endl; // Console spacing

	selection = 0; // Reset refernce to selection to start loop for input
	while (selection == 0) { // Loop while getting valid input
		selection = getInt("Please choose your option between 1 and 5 : "); // Get valid input
		if (selection < 1 || selection > 5) { // Check input is within bounds
			std::cout << "Invalid input...\n\n";
			selection = 0; // Reset selection to loop if invalid
		}
	}

	switch (selection) // Use valid input to return the correct shape based on selection
	{
		case 1: { // Square selection
			return new Square();
		}
		case 2: { // Square selection
			return new Rectangle();
		}
		case 3: { // Square selection
			return new Triangle();
		}
		case 4: { // Square selection
			return new Circle();
		}
		case 5: // Exit selection -> will exit because (selection == 5)
		default: { // Default returns generic shape
			return new Shape(); // Return generic shape
		}

	}
}

void Shape::drawShape()
{
	std::cout << "\nNo defined shape...\n\n";
}

void Shape::area()
{
	std::cout << "\nNo defined shape...\n\n";
}

void Shape::perimeter()
{
	std::cout << "\nNo defined shape...\n\n";
}
