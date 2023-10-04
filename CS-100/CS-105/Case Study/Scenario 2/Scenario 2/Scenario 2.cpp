// Alexander Legner - 270168960 - Scenario 2
#include <iostream>
#include <string>
#include <vector>
#include "Player.h"
#include "Warrior.h"
#include "Priest.h"
#include "Mage.h"

/// <summary>
/// entry point to the program
/// </summary>
/// <returns>program error code</returns>
int main()
{
	// Vectors for sorting all Player instances
	std::vector<Player*> warriors;
	std::vector<Player*> priests;
	std::vector<Player*> mages;
	std::vector<Player*> allPlayers;

    int seletion = 0; // Input value
    while (seletion != 4) { // Loop while input is NOT exit input
		// Prompt
        std::cout << "\tCHARACTER CREATION\n";
        std::cout << "Which of the following would you like?\n";
        std::cout << "\t1. Create a Warrior!\n";
        std::cout << "\t2. Create a Priest!\n";
        std::cout << "\t3. Create a Mage!\n";
        std::cout << "\t4. Finish creating player characters!\n";
		
		// Raw input from console
		std::string rawInput = "";
		std::getline(std::cin >> std::ws, rawInput);
		
		try {
			// Check for decimals
			int len = rawInput.find_last_of('.'); // Gets decimal place - i.e. 1.3 is 1dp
			if (len > 0) { // Stops any decimals
				throw std::invalid_argument("Input cannot have decimals...");
			}

			seletion = stoi(rawInput); // trys to convert raw input to int

			switch (seletion) { // Menu selection
				case 1: { // Create 'WARRIOR' selection 
					// Create warrior with user input for name and race
					Warrior* warrior = new Warrior(Player::getName(), Player::getRace()); 
				
					// Add warrior to appropiate lists
					warriors.push_back(warrior);
					allPlayers.push_back(warrior);

					break;
				}
				case 2: { // Create 'PRIEST' selection
					// Create priest with user input for name and race
					Priest* priest = new Priest(Player::getName(), Player::getRace());
				
					// Add priest to appropiate lists
					priests.push_back(priest);
					allPlayers.push_back(priest);

					break;
				}
				case 3: { // Create 'MAGE' selection
					// Create mage with user input for name and race
					Mage* mage = new Mage(Player::getName(), Player::getRace());
				
					// Add mage to appropiate lists
					mages.push_back(mage);
					allPlayers.push_back(mage);

					break;
				}
				case 4: { // End 'CHARACTER CREATION' selection
					// Display WARRIORS
					std::cout << "---------------\n";
					std::cout << "WARRIORS LIST: \n";
					std::cout << "---------------\n";

					// Loop through each warrior
					for (Player* player : warriors) {
						// Display player type, name, race and unique attack
						std::cout << "I am a warrior with name " << player->whatName() << " with race " << player->whatRace() << " and my attack is : " << player->attack() << "\n";
					}

					std::cout << "\n"; // Console Spacing

					// Display PRIESTS
					std::cout << "---------------\n";
					std::cout << "PRIESTS LIST: \n";
					std::cout << "---------------\n";

					// Loop through each priest
					for (Player* player : priests) {
						// Display player type, name, race and unique attack
						std::cout << "I am a priest with name " << player->whatName() << " with race " << player->whatRace() << " and my attack is : " << player->attack() << "\n";
					}

					std::cout << "\n"; // Console Spacing

					// Display MAGES
					std::cout << "---------------\n";
					std::cout << "MAGES LIST: \n";
					std::cout << "---------------\n";

					// Loop through each mage
					for (Player* player : mages) {
						// Display player type, name, race and unique attack
						std::cout << "I am a mage with name " << player->whatName() << " with race " << player->whatRace() << " and my attack is : " << player->attack() << "\n";
					}
				
					std::cout << "\n"; // Console Spacing

					break;
				}
				default: { // Stops invalid input out of range 
					throw std::invalid_argument("Input is outside valid range...");
					break;
				}
			}
		}
		catch (...) { // Catch all exceptions
			seletion = 0; // Reset selection to stop unwanted behaviour
			std::cout << "Invalid input...\n"; // Inform user and loop
		}

		std::cout << "\n"; // Console spacing
    }

	std::cout << "Character Creation Done!.....\n"; // Exit message

	// Remove unused memory
	for (Player* player : allPlayers){
		delete(player);
		player = nullptr;
	}

	// Clear all vectors
	allPlayers.clear(); 
	warriors.clear();
	priests.clear();
	mages.clear();

	return 0;
}