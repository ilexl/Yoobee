#include "Triangle.h"
#include <iostream>

Shape* Triangle::menu(int& selection)
{
	// Title
	std::cout << "********************\n";
	std::cout << "Triangle Calculator\n";
	std::cout << "********************\n";

	std::cout << std::endl; // Console spacing

	drawShape(); // Draws this shape into the console

	std::cout << std::endl; // Console spacing

	// Prompt options
	std::cout << "1. Area (area = 0.5 * base * base sq.units)\n";
	std::cout << "2. Perimeter (Side 1 + Side 2 + Side 3 units)\n";
	std::cout << "3. Go back to main menu (Shapes Calculator)\n";

	std::cout << std::endl; // Console spacing

	selection = 0; // Reset refernce to selection to start loop for input
	while (selection == 0) { // Loop while getting valid input
		selection = getInt("Please choose your option between 1 and 3 : "); // Get valid input
		if (selection < 1 || selection > 3) { // Check input is within bounds
			std::cout << "Invalid input...\n\n";
			selection = 0; // Reset selection to loop if invalid
		}
	}

	switch (selection) {  // Use valid input to perform action and then return the correct shape
		case 1: {
			area(); // Gets user input for area of this shape
			return new Triangle();
		}
		case 2: {
			perimeter(); // Gets user input for area of this shape
			return new Triangle();
		}
		case 3: // Main menu selection
		default:{ // Default returns generic shape
			selection = 0; // ****MUST be reset to 0 to prevent unexpected behaviour****
			return new Shape(); // Return generic shape
		}
	}
}

void Triangle::drawShape()
{
	// Draws a triangle to the console
	for (int height = 1; height <= 10; height++) {
		for (int width = 1; width <= height; width++) {
			std::cout << "* ";
		}
		std::cout << "\n";
	}
}

void Triangle::area()
{
	float width = getFloat("Enter the width of the triangle : ", true); // Get valid input
	float height = getFloat("Enter the height of the triangle : ", true); // Get valid input
	float area = width * height * 0.5f; // Calculation of area
	std::cout << "Area = 0.5 * " << width << " * " << height << " = " << area << " sq.units" << std::endl; // Console output
}

void Triangle::perimeter() {
	float sideOne = getFloat("Enter the Side 1 of the triangle : ", true); // Get valid input
	float sideTwo = getFloat("Enter the Side 2 of the triangle : ", true); // Get valid input
	float sideThree = getFloat("Enter the Side 3 of the triangle : ", true); // Get valid input
	float perimeter = sideOne + sideTwo + sideThree; // Calculation of perimeter
	std::cout << "Perimeter = " << sideOne << " + " << sideTwo << " + " << sideThree << " = " << perimeter << " units" << std::endl; // Console output
}
