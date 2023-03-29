#include "Mage.h"
#include "Player.h"

std::string Mage::attack()
{
	return "I will crush you with the power of my arcane missiles!"; // Specific attack for this character type
}

Mage::Mage(std::string _name, Player::Race _race) : Player(_name, _race, 200, 0) {}
