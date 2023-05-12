#pragma once
#include "Animal.h"
class Bird : public Animal
{
public:
	void fly();
	void attack() override;
};

