#include "Warrior.h"

std::string Warrior::attack()
{
    return "I will destroy you with my sword, foul demon!"; // Specific attack for this character type
}

Warrior::Warrior(std::string _name, Player::Race _race) : Player(_name, _race, 200, 0) {}