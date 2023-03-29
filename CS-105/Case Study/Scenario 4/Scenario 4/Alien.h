#pragma once
#include <random>
#include <time.h>
class Alien
{
private:
	float height;
	float weight;
	int gender; // 2 for male, 3 for female
public:
	/// <summary>
	/// Constructor for Alien
	/// </summary>
	/// <param name="height">float height in meters</param>
	/// <param name="weight">float weight in kilograms</param>
	/// <param name="gender">int gender (2 == male, 3 == female)</param>
	Alien(float _height, float _weight, int _gender);
	
	/// <summary>
	/// Gets this instances height
	/// </summary>
	/// <returns>float height</returns>
	float getHeight() const;

	/// <summary>
	/// Gets this instances weight
	/// </summary>
	/// <returns>float weight</returns>
	float getWeight() const;

	/// <summary>
	/// Gets this instances gender as an int
	/// </summary>
	/// <returns>int gender (2 == male, 3 == female)</returns>
	int getGender() const;

	/// <summary>
	/// Calculates and returns this instances prestiege based on its values
	/// </summary>
	/// <returns>float prestiege</returns>
	float getPrestige() const;
	
	/// <summary>
	/// Checks if this instances prestiege does equal to other instances prestiege
	/// </summary>
	/// <param name="other">The other instance</param>
	/// <returns>bool true or false</returns>
	bool operator==(const Alien& other);

	/// <summary>
	/// Checks if this instances prestiege does NOT equal to other instances prestiege
	/// </summary>
	/// <param name="other">The other instance</param>
	/// <returns>bool true or false</returns>
	bool operator!=(const Alien& other);

	/// <summary>
	/// Checks if this instances prestiege is greater than other instances prestiege
	/// </summary>
	/// <param name="other">The other instance</param>
	/// <returns>bool true or false</returns>
	bool operator>(const Alien& other);

	/// <summary>
	/// Checks if this instances prestiege is greater OR equal to other instances prestiege
	/// </summary>
	/// <param name="other">The other instance</param>
	/// <returns>bool true or false</returns>
	bool operator>=(const Alien& other);

	/// <summary>
	/// Checks if this instances prestiege is less than other instances prestiege
	/// </summary>
	/// <param name="other">The other instance</param>
	/// <returns>bool true or false</returns>
	bool operator<(const Alien& other);

	/// <summary>
	/// Checks if this instances prestiege is less than OR equal to other instances prestiege
	/// </summary>
	/// <param name="other">The other instance</param>
	/// <returns>bool true or false</returns>
	bool operator<=(const Alien& other);

	/// <summary>
	/// Sets the values of this instance to the values of the other instance
	/// </summary>
	/// <param name="other">The other instance</param>
	void operator=(const Alien& other);

	/// <summary>
	/// Adds together this and other instances values in a biological way
	/// </summary>
	/// <param name="other">The other instance</param>
	/// <returns>new Alien the biological child of the two parents</returns>
	Alien* operator+(const Alien& other);

};

