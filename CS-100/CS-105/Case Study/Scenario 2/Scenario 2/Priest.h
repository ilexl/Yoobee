#pragma once
#include "Player.h"
class Priest : public Player
{
public:
	/// <summary>
	/// The attack specific to Priest
	/// </summary>
	/// <returns>string Priests attack</returns>
	std::string attack() override;

	/// <summary>
	/// Constructor for Priest
	/// </summary>
	/// <param name="_name">Name of the character</param>
	/// <param name="_race">Race of the character</param>
	Priest(std::string _name, Player::Race _race);
};

