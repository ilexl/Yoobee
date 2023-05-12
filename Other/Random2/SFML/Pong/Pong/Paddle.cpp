#include "Paddle.h"
#include "SFML/Graphics.hpp"

Paddle::Paddle(sf::Vector2f _pos, sf::Keyboard::Key _keyUp, sf::Keyboard::Key _keyDown, sf::Color color)
{
	this->position = _pos;
	this->up = _keyUp;
	this->down = _keyDown;
	this->color = color;
}

void Paddle::Update(sf::RenderWindow& window)
{
	if (sf::Keyboard::isKeyPressed(up)) {
		position -= sf::Vector2f(0.0, 1.0 * speed);
	}
	if (sf::Keyboard::isKeyPressed(down)) {
		position += sf::Vector2f(0.0, 1.0 * speed);
	}

	if (position.y < 0) {
		position.y = 0;
	}
	if (position.y > window.getDefaultView().getSize().y - 100) {
		position.y = window.getDefaultView().getSize().y - 100;
	}
	

	sf::RectangleShape rect(size);
	if (flash) {
		rect.setFillColor(sf::Color::White);
		if (frames == 0) {
			frames = 5;
			flash = false;
		}
		else {
			frames--;
		}
	}
	else {
		rect.setFillColor(color);
	}
	rect.setPosition(position);
	window.draw(rect);
}
