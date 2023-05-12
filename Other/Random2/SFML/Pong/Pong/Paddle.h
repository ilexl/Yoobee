#pragma once

#include "SFML/Graphics.hpp"

class Paddle
{
private:
	float speed = 20.0;
	sf::Keyboard::Key up, down;
	sf::Color color;
	int frames = 5;
public:
	bool flash = false;
	sf::Vector2f position;
	sf::Vector2f size = sf::Vector2f(20, 100);
	Paddle(sf::Vector2f _pos, sf::Keyboard::Key _keyUp, sf::Keyboard::Key _keyDown, sf::Color color);
	void Update(sf::RenderWindow& window);
};

