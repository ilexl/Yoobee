#include <iostream>
#include "Player.h"

Player::Player(std::string _name, Race _race, int _hitPoints, int _magicPoints) {
	// Set all instance values to parameter values
	name = _name;
	race = _race;
	hitPoints = _hitPoints;
	magicPoints = _magicPoints;
}

std::string Player::getName() {
	std::cout << "What would you like to name your character? : "; // Prompt
	
	// Get console input
	std::string rawInput = ""; 
	std::getline(std::cin >> std::ws, rawInput);
	
	return rawInput; // Return input
}

Player::Race Player::getRace() {
	bool inputValid = false; // Loop control variable
	while (!inputValid) { // Loop while getting input for characters race
		// Prompt
		std::cout << "\nWhich race do you want?\n";
		std::cout << "\t1. Human!\n";
		std::cout << "\t2. Elf!\n";
		std::cout << "\t3. Dwarf!\n";
		std::cout << "\t4. Orc!\n";
		std::cout << "\t5. Troll!\n";
		
		// Get console input
		std::string rawInput = "";
		std::getline(std::cin >> std::ws, rawInput);
		
		try {
			// Check for decimals
			int len = rawInput.find_last_of('.'); // Gets decimal places of input - i.e. 1.2 is 1dp
			if (len > 0) { // Stops all decimals
				throw std::invalid_argument("Input cannot have decimals...");
			}

			int checkInput = stoi(rawInput); // Convert input to int
			inputValid = true; // Assume the input is valid to stop from looping

			switch (checkInput) { // Use input
				case 1: // Selection HUMAN from prompt - HUMAN = Race(1)
				case 2: // Selection ELF from prompt   - HUMAN = Race(2)
				case 3: // Selection DWARF from prompt - HUMAN = Race(3)
				case 4: // Selection ORC from prompt   - HUMAN = Race(4)
				case 5: // Selection TROLL from prompt - HUMAN = Race(5)
					return Race(checkInput); // Return converted race from input
					break;

				default: // Stops all values outside prompt range
					throw std::invalid_argument("Input is outside valid range...");
					break;
			}
		}
		catch (...) { // catch all exceptions
			inputValid = false; // Turn loop back if any input was invalid
			std::cout << "Invalid input...\n";
		}
	}
	return Race(NULL); // Return NULL if unexpected behaviour - SHOULD NOT REACH HERE
}

std::string Player::whatRace()
{
	// return the string equivalent of the enum for 
	switch (race)
	{
		case Player::Race::HUMAN:
			return "HUMAN";
		case Player::Race::ELF:
			return "ELF";
		case Player::Race::DWARF:
			return "DWARF";
		case Player::Race::ORC:
			return "ORC";
		case Player::Race::TROLL:
			return "TROLL";
		default:
			return ""; // Return blank value if not a valid Race
	}
}

std::string Player::whatName() {
	return name; // Name of character
}

int Player::getHitPoints() {
	return hitPoints; // Hit points of character
}

int Player::getMagicPoints() {
	return magicPoints; // Magic points of character
}

void Player::setName(std::string _name)
{
	name = _name;
}

void Player::setRace(Race _race)
{
	race = _race;
}

void Player::setHitPoints(int _hitPoints)
{
	hitPoints = _hitPoints;
}

void Player::setMagicPoints(int _magicPoints)
{
	magicPoints = _magicPoints;
}