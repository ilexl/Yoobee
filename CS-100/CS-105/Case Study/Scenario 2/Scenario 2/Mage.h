#pragma once
#include "Player.h"
class Mage : public Player
{
public:
	/// <summary>
	/// The attack specific to Mage
	/// </summary>
	/// <returns>string Mages attack</returns>
	std::string attack() override;

	/// <summary>
	/// Constructor for Mage
	/// </summary>
	/// <param name="_name">Name of the character</param>
	/// <param name="_race">Race of the character</param>
	Mage(std::string _name, Player::Race _race);
};

