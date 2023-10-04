#include "Priest.h"

std::string Priest::attack()
{
    return "I will assault you with Holy Wrath!"; // Specific attack for this character type
}

Priest::Priest(std::string _name, Player::Race _race) : Player(_name, _race, 200, 0) {}
