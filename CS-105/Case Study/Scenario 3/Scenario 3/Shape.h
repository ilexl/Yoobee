#pragma once
#include <string>
class Shape
{
protected:
	/// <summary>
	/// Gets console input from the user
	/// </summary>
	/// <returns>string input</returns>
	std::string getRawInput();
	
	/// <summary>
	/// Gets int input after displaying a prompt to the user
	/// </summary>
	/// <param name="prompt">prompt to display</param>
	/// <returns>int input</returns>
	int getInt(std::string prompt);
	
	/// <summary>
	/// Gets float input after displaying a prompt to the user
	/// </summary>
	/// <param name="prompt">prompt to display</param>
	/// <returns>float input</returns>
	float getFloat(std::string prompt);
	
	/// <summary>
	/// Gets float input after displaying a prompt to the user - with an option for only non negative values
	/// </summary>
	/// <param name="prompt">prompt to display</param>
	/// <param name="greaterThanZero">return value is forced to be non negative</param>
	/// <returns>float input</returns>
	float getFloat(std::string prompt, bool greaterThanZero);

	/// <summary>
	/// Draws the current shape
	/// </summary>
	virtual void drawShape();
	
	/// <summary>
	/// Gets users input and calculates the area of the shape
	/// </summary>
	virtual void area();
	
	/// <summary>
	/// Gets users input and calculates the perimeter of the shape
	/// </summary>
	virtual void perimeter();

public:
	
	/// <summary>
	/// Displays a menu for user to interact with
	/// </summary>
	/// <param name="selection">reference to the selection to check for exit</param>
	/// <returns>Shape user selected</returns>
	virtual Shape* menu(int& selection);

};

