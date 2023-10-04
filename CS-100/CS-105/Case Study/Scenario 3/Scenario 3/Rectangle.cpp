#include "Rectangle.h"
#include <iostream>

Shape* Rectangle::menu(int& selection)
{
	// Title
	std::cout << "********************\n";
	std::cout << "Rectangle Calculator\n";
	std::cout << "********************\n";

	std::cout << std::endl; // Console spacing

	drawShape(); // Draws this shape into the console

	std::cout << std::endl; // Console spacing

	// Prompt options
	std::cout << "1. Area (area = base * height sq.units)\n";
	std::cout << "2. Perimeter (perimeter = 2 * base + 2 * height units)\n";
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
			return new Rectangle();
		}
		case 2: {
			perimeter(); // Gets user input for area of this shape
			return new Rectangle();
		}
		default: { // Default returns generic shape
			selection = 0; // ****MUST be reset to 0 to prevent unexpected behaviour****
			return new Shape(); // Return generic shape
		}
	}
}

void Rectangle::drawShape()
{
	// Draws a rectangle to the console
	for (int height = 5; height > 0; height--) {
		for (int width = 10; width > 0; width--) {
			std::cout << "* ";
		}
		std::cout << "\n";
	}
}

void Rectangle::area()
{
	float width = getFloat("Enter the width of the rectangle : ", true); // Get valid input
	float height = getFloat("Enter the height of the rectangle : ", true); // Get valid input
	float area = width * height; // Calculation of area
	std::cout << "Area = " << width << " * " << height << " = " << area << " sq.units" << std::endl; // Console output
}

void Rectangle::perimeter()
{
	float width = getFloat("Enter the width of the rectangle : ", true); // Get valid input
	float height = getFloat("Enter the height of the rectangle : ", true); // Get valid input
	float perimeter = (2 * width) + (2 * height); // Calculation of perimeter
	std::cout << "Perimeter = (2 * " << width << ") + (2 * " << height << ") = " << perimeter << " units" << std::endl; // Console output
}
