#include <iostream>
#include <conio.h>

void Square();
void Triangle();
void Rectangle();
void Circle();

int main()
{
	std::string input = "";
	int size = 0;

	std::cout << "Enter shape (Square, Triangle, Rectangle, Circle) : ";
	std::cin >> input;
	
	if (input == "Square" || input == "square" || input == "s") {
		Square();
	}
	else if (input == "Rectangle" || input == "rectangle" || input == "r") {
		Rectangle();
	}
	else if (input == "Triangle" || input == "triangle" || input == "t") {
		Triangle();
	}
	else if (input == "Circle" || input == "circle" || input == "c") {
		Circle();
	}
	else {
		std::cout << "Not a valid option...";
	}

	return 0;
}

void Square() {
	int length = 0;
	std::cout << "Enter side length of square : ";
	std::cin >> length;

	for (int i = 0; i < length; i++) {
		if (i == 0 || i == length - 1) {
			for (int i = 0; i < length; i++) {
				std::cout << "* ";
			}
			std::cout << "\n";
		}
		else {
			std::cout << "*";
			for (int i = 0; i < length-2; i++) {
				std::cout << "  ";
			}
			std::cout << " *";
			std::cout << "\n";
		}
	}
}

void Triangle() {
	int width = 1, height = 0;
	std::cout << "Enter height : ";
	std::cin >> height;

	for (int i = 1, k = 0; i <= height; ++i, k = 0)
	{
		for (width = 1; width <= height - i; ++width)
		{
			std::cout << "  ";
		}

		while (k != 2 * i - 1)
		{
			if (i == 1 || i == height) {
				std::cout << "* ";
			}
			else {
				if (k == 0 || k == (2 * i - 2) ) {
					std::cout << "* ";
				}
				else {
					std::cout << "  ";
				}
			}
			++k;
		}
		std::cout << std::endl;
	}
}

void Rectangle() {
	int width = 0, height = 0;
	std::cout << "Enter width : ";
	std::cin >> width;
	std::cout << "Enter height : ";
	std::cin >> height;

	for (int i = 0; i < height; i++) { // for height
		if (i == 0 || i == height - 1) {
			for (int i = 0; i < width; i++) { // output width
				std::cout << "* ";
			}
			std::cout << "\n";
		}
		else {
			std::cout << "*";
			for (int i = 0; i < width - 2; i++) {
				std::cout << "  ";
			}
			std::cout << " *";
			std::cout << "\n";
		}
	}
}

void Circle() {
	float y, k;
	std::cout << "Enter the Radius of the desired circle size";
	std::cin >> y;
	float m = 2;

	for (int i = -y; i <= y; i++)
	{
		for (int j = -y; j <= y; j++)
		{
			k = ((i * m) / y) * ((i * m) / y) + (j / y) * (j / y);

			if (k > 0.95 && k < 1.08)
			{
				std::cout << " * ";
			}
			else
			{
				std::cout << " ";
			}
		}
		std::cout << "\n";
	}
	_getch();
}
