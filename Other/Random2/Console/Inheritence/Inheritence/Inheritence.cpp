#include <iostream>
#include "Animal.h"
#include "Bird.h"
#include <vector>

int main()
{
	std::vector<Animal*> animals;

	animals.push_back(new Animal());
	animals.push_back(new Bird());

	for (Animal* animal : animals) {
		animal->attack();
	}
}
