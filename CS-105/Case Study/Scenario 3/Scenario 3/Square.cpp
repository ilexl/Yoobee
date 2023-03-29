#include "Square.h"
#include <iostream>

Shape* Square::menu(int& selection)
{
	// Title
	std::cout << "********************\n";
	std::cout << "Square Calculator\n";
	std::cout << "********************\n";

	std::cout << std::endl; // Console spacing

	drawShape(); // Draws this shape into the console

	std::cout << std::endl; // Console spacing

	// Prompt options
	std::cout << "1. Area (area = base * base sq.units)\n";
	std::cout << "2. Perimeter (perimeter = 4 * base units)\n";
	std::cout << "3. Go back to main menu\n";

	std::cout << std::endl; // Console spacing

	selection = 0; // Reset refernce to selection to start loop for input
	while (selection == 0) { // Loop while getting valid input
		selection = getInt("Please choose your option between 1 and 3 : "); // Get valid input
		if (selection < 1 || selection > 3) { // Check input is within bounds
			std::cout << "Invalid input...\n\n";
			selection = 0; // Reset selection to loop if invalid
		}
	}

		switch (selection) // Use valid input to perform action and then return the correct shape
		{
		case 1: {
			area(); // Gets user input for area of this shape
			return new Square();
		}
		case 2: {
			perimeter(); // Gets user input for area of this shape
			return new Square();
		}
		default: { // Default returns generic shape
			selection = 0; // ****MUST be reset to 0 to prevent unexpected behaviour****
			return new Shape(); // Return generic shape
		}
	}
}

void Square::drawShape()
{
	// Draws a square to the console
	for (int height = 10; height > 0; height--) {
		for (int width = 10; width > 0; width--) {
			std::cout << "* ";
		}
		std::cout << "\n";
	}
}

void Square::area()
{
	float base = getFloat("Enter the base of the square : ", true); // Get valid input
	float area = base * base; // Calculation of area
	std::cout << "Area = " << base << " * " << base << " = " << area << " sq.units" << std::endl; // Console output
}

void Square::perimeter()
{
	float base = getFloat("Enter the base of the square : ", true); // Get valid input
	float perimeter = 4 * base; // Calculation of perimeter
	std::cout << "Perimeter = 4 * " << base << " = " << perimeter << " units" << std::endl; // Console output
}
