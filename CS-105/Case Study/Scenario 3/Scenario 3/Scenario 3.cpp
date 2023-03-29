// Alexander Legner - 270168960 - Scenario 3
#include <iostream>
#include "Shape.h"

/// <summary>
/// Entry point for the program
/// </summary>
/// <returns>int error code</returns>
int main()
{
    Shape* shape = new Shape(); // Sets current shape to generic shape with main menu

    int selection = 0; // Users current selection 
    while (selection != 5) { // Selection changes within the shapes menu - loop until exit is selected by user
        Shape* shapeNext = shape->menu(selection); // Call the menu for the current shape which returns a new type of shape
        
        delete shape; // Delete the current shape
        shape = nullptr; // Remove pointer to stop accidental access
        shape = shapeNext; // Set the current shape to the one returned from the original shape menu
        shapeNext = nullptr; // Remove pointer to stop accidental access

        std::cout << std::endl; // Console spacing
    }

    return 0; // Exit program with no error code
}