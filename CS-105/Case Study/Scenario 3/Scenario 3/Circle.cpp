#include "Circle.h"
#include <iostream>
#define _USE_MATH_DEFINES // USED FOR DEFINITION OF PI
#include <math.h>

Shape* Circle::menu(int& selection)
{
	// Title
	std::cout << "********************\n";
	std::cout << "Circle Calculator\n";
	std::cout << "********************\n";

	std::cout << std::endl; // Console spacing

	drawShape(); // Draws this shape into the console

	std::cout << std::endl; // Console spacing

	// Prompt options
	std::cout << "1. Area = (" << char(227) << " * radius * radius sq.units)\n";
	std::cout << "2. Circumference = (" << char(227) << " * 2 * radius units)\n";
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

	switch (selection) { // Use valid input to perform action and then return the correct shape
		case 1: {
			area(); // Gets user input for area of this shape
			return new Circle();
		}
		case 2: {
			perimeter(); // Gets user input for area of this shape
			return new Circle();
		}
		default: { // Default returns generic shape
			selection = 0; // ****MUST be reset to 0 to prevent unexpected behaviour****
			return new Shape(); // Return generic shape
		}
	}
}

void Circle::drawShape()
{
	// Draws a circle to the console
	int diameter, x, y, radius = 5, draw = 0;
	diameter = 2 * radius;
	for (int height = 0; height <= diameter; height++) {
		for (int width = 0; width <= diameter; width++) {
			x = radius - height;
			y = radius - width;
			draw = x * x + y * y;
			if (draw <= radius * radius + 1) {
				std::cout << "* ";
			}
			else {
				std::cout << "  ";
			}
		}
		std::cout << "\n";
	}
}

void Circle::area()
{
	float radius = getFloat("Enter the radius of the circle : ", true); // Get valid input
	float area = radius * radius * M_PI; // Calculation of area
	std::cout << "Area = " << char(227) << " * " << radius << " * " << radius << " = " << area << " sq.units" << std::endl; // Console output
}

void Circle::perimeter()
{
	float radius = getFloat("Enter the radius of the circle : ", true); // Get valid input
	float circumference = radius * 2 * M_PI; // Calculation of circumference
	std::cout << "Circumference = " << char(227) << " * " << radius << " * 2 = " << circumference << " units" << std::endl; // Console output
}
