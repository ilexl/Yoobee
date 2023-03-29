#pragma once
#include <string>
/// <summary>
/// The player class for character creation
/// </summary>
class Player
{
protected:
	/// <summary>
	/// Race of the player
	/// </summary>
	enum Race
	{
		NONE,
		HUMAN,
		ELF,
		DWARF,
		ORC,
		TROLL
	};

	/// <summary>
	/// Sets the name of the character
	/// </summary>
	/// <param name="_name">character name</param>
	void setName(std::string _name);

	/// <summary>
	/// Sets the race of the character
	/// </summary>
	/// <param name="_race">Characters race</param>
	void setRace(Race _race);

	/// <summary>
	/// Sets the hit points of the character
	/// </summary>
	/// <param name="_hitPoints">Characters hit points</param>
	void setHitPoints(int _hitPoints);

	/// <summary>
	/// Sets the magic points of the character
	/// </summary>
	/// <param name="_magicPoints">Characters magic points</param>
	void setMagicPoints(int _magicPoints);

private:
	std::string name; // Characters name
	Race race; // Characters race
	int hitPoints; // Characters hit points
	int magicPoints; // Characters magic points
public:
	/// <summary>
	/// Gets user input for character name
	/// </summary>
	/// <returns>string Characters name</returns>
	static std::string getName();

	/// <summary>
	/// Gets user input for character race
	/// </summary>
	/// <returns>Race Characters race</returns>
	static Race getRace();
	
	/// <summary>
	/// Constructor for 'Player' character 
	/// </summary>
	/// <param name="_name">Character name</param>
	/// <param name="_race">Character race</param>
	/// <param name="_hitPoints">Character hit points</param>
	/// <param name="_magicPoints">Character magic points</param>
	Player(std::string _name, Race _race, int _hitPoints, int _magicPoints);
	
	/// <summary>
	/// Gets the race of character as a string
	/// </summary>
	/// <returns>string Character race</returns>
	std::string whatRace();

	/// <summary>
	/// Gets the name of character as a string
	/// </summary>
	/// <returns>string Character name</returns>
	std::string whatName();

	/// <summary>
	/// Gets the characters hit points
	/// </summary>
	/// <returns>int Character hit points</returns>
	int getHitPoints();

	/// <summary>
	/// Gets the characters magic points
	/// </summary>
	/// <returns>int Character magic points</returns>
	int getMagicPoints();

	/// <summary>
	/// Gets the attack of a character
	/// </summary>
	/// <returns>string Characters attack</returns>
	virtual std::string attack() = 0;
};

