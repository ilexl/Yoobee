#pragma once
#include <iostream>
#include "Location.h"
/// <summary>
/// A yacht (boat) with a latitude and longitude position
/// </summary>
class Yacht
{
private:
	static int counter; // Count is used for unique serial numbers
	int serialNumber;   // Unique serial number of instance

	Location latitude;  // Degrees, Minutes, Direction
	Location longitude; // Degrees, Minutes, Direction
public:
	/// <summary>
	/// Default constructor for a yacht
	/// </summary>
	Yacht();

	/// <summary>
	/// Resets the counter to the new value
	/// </summary>
	/// <param name="resetVal">the new value to reset to</param>
	static void resetCounter(int resetVal);

	/// <summary>
	/// Gets latitude and longitude for the yacht
	/// </summary>
	void getPos();

	/// <summary>
	/// Displays the final positition of the yacht
	/// </summary>
	void display();
};

