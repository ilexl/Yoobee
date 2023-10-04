#include "Alien.h"

Alien::Alien(float _height, float _weight, int _gender) {
	height = _height;
	weight = _weight;
	gender = _gender;
}

float Alien::getHeight() const
{
    return height;
}

float Alien::getWeight() const
{
    return weight;
}

int Alien::getGender() const
{
    return gender;
}

float Alien::getPrestige() const
{
	// Calculate prestige
    float prestige = (height * weight);
	if (gender == 2 || gender == 3) { // Multiply by 2 for male or 3 for female
		prestige *= gender; // Gender is 2 for male and 3 for female
	}
    return prestige;
}

bool Alien::operator==(const Alien& other) {
	float otherValue = other.getPrestige();
	float thisValue = getPrestige();

	return thisValue == otherValue;
}

bool Alien::operator!=(const Alien& other) {
	float otherValue = other.getPrestige();
	float thisValue = getPrestige();

	return thisValue != otherValue;
}

bool Alien::operator>(const Alien& other) {
	float otherValue = other.getPrestige();
	float thisValue = getPrestige();

	return thisValue > otherValue;
}

bool Alien::operator>=(const Alien& other) {
	float otherValue = other.getPrestige();
	float thisValue = getPrestige();

	return thisValue >= otherValue;
}

bool Alien::operator<(const Alien& other) {
	float otherValue = other.getPrestige();
	float thisValue = getPrestige();

	return thisValue < otherValue;
}

bool Alien::operator<=(const Alien& other) {
	float otherValue = other.getPrestige();
	float thisValue = getPrestige();

	return thisValue <= otherValue;
}

void Alien::operator=(const Alien& other) {
	height = other.getHeight();
	weight = other.getWeight();
	gender = other.getGender();
	return;
}

Alien* Alien::operator+(const Alien& other) {
	float offSpringWeight = 0.5f * ((float)weight + (float)other.weight); // Average of both parents weight
	float offSpringHeight = 0.5f * ((float)height + (float)other.height); // Average of both parents height
	int offSpringGender = (rand() % 2) + 2; // 50% chance for male or female (2 or 3)
	Alien* offSpring = new Alien(offSpringHeight, offSpringWeight, offSpringGender); // Create the child
	return offSpring; // Return the child
}