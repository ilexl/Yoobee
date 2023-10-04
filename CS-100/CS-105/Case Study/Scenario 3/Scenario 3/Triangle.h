#pragma once
#include "Shape.h"
class Triangle : public Shape
{
public:
	/// <summary>
	/// Displays a menu for user to interact with
	/// </summary>
	/// <param name="selection">reference to the selection to ensure program doesnt exit unintendedly</param>
	/// <returns>Shape user selected</returns>
	Shape* menu(int& selection) override;

	/// <summary>
	/// Draws the current shape using console art
	/// </summary>
	void drawShape() override;

	/// <summary>
	/// Gets user input and calculates the area of the current shape
	/// </summary>
	void area() override;

	/// <summary>
	/// Gets user input and calculates the perimeter of the current shape
	/// </summary>
	void perimeter() override;
};

