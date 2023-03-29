#include <iostream>
#include "Yacht.h"
#include "Location.h"

int Yacht::counter = NULL; // Assign the counter memory

void Yacht::resetCounter(int resetVal)
{
	Yacht::counter = resetVal;
}

Yacht::Yacht()
{
	serialNumber = counter++; // Set serial number THEN increment counter
	latitude = Location();  // Set blank location in memory
	longitude = Location(); // Set blank location in memory
}

void Yacht::getPos()
{
	std::cout << "Enter the location of ship #" << serialNumber << ":\n"; // Prompt
	latitude.getPos();  // Gets location values for degrees minutes direction for latitude
	longitude.getPos(); // Gets location values for degrees minutes direction for longitude
}

void Yacht::display()
{
	// Displays the formatted yachts position 
	std::cout << "The ship serial number is: " << serialNumber << std::endl;
	std::cout << "The position of ship #" << serialNumber << " is ";
	latitude.display(); // Latitude in correct format
	std::cout << " Latitude\t";
	longitude.display(); // Longitude in correct format
	std::cout << " Longitude\n";
}
