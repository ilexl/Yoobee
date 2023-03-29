#include "Animal.h"
#include <iostream>

void Animal::eat()
{
	std::cout << "I'm eating!\n";
}

void Animal::sleep()
{
	std::cout << "I'm sleeping!\n";
}

void Animal::attack()
{
	std::cout << "Animal attacks for 10 damage!\n";
}