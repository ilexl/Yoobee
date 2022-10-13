// Make a lottery ticket choosing program

// Linkage
#include <iostream>
#include <string>
#include <ctime>

// Entry point of the program
int main()
{
	// Variable declarion
	const int lottoBuyers = 5; // const for array declarion
	
	// Linked arrays declaration
	std::string names[lottoBuyers];
	int tickets[lottoBuyers];

	// Initialise random seed as current time
	srand(time(NULL));

	// Title
	std::cout << "\t\t\tLucky NZ Lottery entry !!!!\n";
	std::cout << "\t\t\t~~~~~~~~~~~~~~~~~~~~~~~~~~~\n";

	// Loop to get all inputs for lottoBuyers
	for (int i = 0; i < lottoBuyers; i++) { // loops 0 -> lottoBuyers (exclusive)
		// Block input variables declaration
		std::string nameInput = "";
		int ticketInput = 0;

		// Get inputs and store in array at correct index

		// Name input
		std::cout << "Enter name: ";
		std::getline(std::cin >> std::ws, nameInput); // Get name and ignore whitespace

		std::cout << std::endl; // Spacing

		bool ticketInvalid = true; // Assume ticket number will not be between 1000 - 10000
		do { // Get ticket number (at least once)
			// TicketNumber input
			std::cout << "Your ticket number is : ";
			std::cin >> ticketInput;

			// If ticket number is valid (between 1000 & 11000)
			if (ticketInput >= 100 && ticketInput <= 11000) {
				ticketInvalid = false; // stops from looping as input is valid
				std::cout << std::endl; // Spacing
			}
			else {
				// If invalid ticket, alert user, ticketInvalid remains true causing loop
				std::cout << "Please enter valid ticket number (between 1000 & 11000)\n\n";
			}
		} while (ticketInvalid); // Loop while ticket is invalid
		
		// Correct inputs stored in arrays at loop index (i)
		names[i] = nameInput;
		tickets[i] = ticketInput;
	}

	// After loop Draw tickets

	std::cout << "\t\t\tDraw day !!!!\n";
	std::cout << "\t\t\t~~~~~~~~~~~~~\n";
	
	std::cout << std::endl; // Spacing

	std::cout << "Buyers List :\n";
	std::cout << "~~~~~~~~~~~~~\n";
	
	std::cout << std::endl; // Spacing

	for (int i = 0; i < lottoBuyers; i++) {
		std::cout << "\t" << (i + 1) << ". " << names[i] << "----" << tickets[i] << "\n";
		std::cout << std::endl; // Spacing
	}

	// Generate random winner
	int randomWinnerIndex = rand() % lottoBuyers;

	// Display random winner
	std::cout << "\t\t****************************\n";
	std::cout << std::endl; // Spacing
	std::cout << "\t\t\tCongrats... !!!!!!!";
	std::cout << std::endl; // Spacing
	std::cout << std::endl; // Spacing
	std::cout << "\t\tThe winner is  : " << names[randomWinnerIndex] << "\n";    // Shows the random winner with random winner index (linked array)
	std::cout << std::endl; // Spacing
	std::cout << "\t\tWinning ticket : " << tickets[randomWinnerIndex] << "\n";  // Shows the random winner with random winner index (linked array)
	std::cout << std::endl; // Spacing
	std::cout << "\t\t****************************\n";


	return 0; // (return code 0) program completed without errors 
}