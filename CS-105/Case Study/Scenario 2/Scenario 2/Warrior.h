#pragma once
#include "Player.h"
class Warrior : public Player
{
public:
	/// <summary>
	/// The attack specific to Warrior
	/// </summary>
	/// <returns>string Warriors attack</returns>
	std::string attack() override;

	/// <summary>
	/// Constructor for Warrior
	/// </summary>
	/// <param name="_name">Name of the character</param>
	/// <param name="_race">Race of the character</param>
	Warrior(std::string _name, Player::Race _race);
};

