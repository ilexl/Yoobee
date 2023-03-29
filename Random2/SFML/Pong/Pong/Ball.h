#pragma once

#include "SFML/Graphics.hpp"
#include "Paddle.h"

class Ball
{
	float speed = 5;
	sf::Vector2f direction = sf::Vector2f(1, 1);
	sf::Vector2f size = sf::Vector2f(5, 5);
public:
	sf::Vector2f pos = sf::Vector2f(0, 0);
	void Update(sf::RenderWindow& window, int& score);
	void CheckBounce(Paddle& paddle);
	void Start(sf::RenderWindow& window);
};

