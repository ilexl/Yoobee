/*
Task 2
Write a program to sum the non-negative values in the list. 
Pass an array to the function using a pointer and return the sum. 
Allow the user to feed list elements.
(Hint: Function arguments could be the array pointer and the size of the array)
*/

#include <iostream>
#include <string>

using std::cin;
using std::cout;
using std::string;
using std::getline;
using std::ws;
using std::endl;

// function prototypes
float GetValidInput(string question, bool newLine);
float GetSumPositive(int* list, int listSize);

int main()
{
	// get list size > 0
	int listSize;
	do {
		listSize = (int)GetValidInput("Enter the size of the list (floats): ", false);

		if (listSize < 1) {
			cout << "\nList is too small...\n\n";
		}
	} while (listSize < 1);

	// set dynamic list to correct size
	int* list = new int[listSize]; // created in heap

	// get inputs for the list
	for (int i = 0; i < listSize; i++) {
		float input = GetValidInput(("Enter list value " + std::to_string(i + 1) + " : "), false);
		list[i] = input;
	}

	// get sum of all positive numbers as per brief
	float sum = GetSumPositive(list, listSize);

	cout << endl; // spacing

	// outut sum
	cout << "The sum of the positive values in the list is : " << sum;

	// delete unused memory from heap
	delete[] list;
	list = nullptr;

	return 0;
}

// returns float with forced correct input from user
float GetValidInput(string question, bool newLine) {
	bool invalidInput = true;
	
	// loop while getting input
	while (invalidInput) {
		cout << question; // output promt for input
		if (newLine) {
			cout << endl;
		} // newline printed for input if selected
		string rawInput;
		getline(cin >> ws, rawInput);  // getline prevents unwanted console behaviour
		invalidInput = false; // assume input is valid

		try {
			float output = stof(rawInput);
			return output; // if no error, return number
		}
		catch (...) {
			cout << "\nInvalid Input...\n\n";
			invalidInput = true; // only invalid if an exception occurs
								 // errors if not a number
		}
	} // stop looping when input is valid
	return 0;
}

// adds all positive numbers and returns the sum
float GetSumPositive(int* list, int listSize) {
	float total = 0; // set sum to 0

	for (int i = 0; i < listSize; i++) {
		if (list[i] > 0) {
			total += list[i]; // add each positive number to sum
		}
	}

	return total; // return sum
}