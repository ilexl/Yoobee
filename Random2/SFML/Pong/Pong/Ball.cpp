#include <iostream>
#include "Ball.h"
#include "Paddle.h"

void Ball::Update(sf::RenderWindow& window, int& score)
{
	pos += direction * speed;
	speed += 0.01f;
	sf::Vector2f windowSize = window.getDefaultView().getSize();
	if (pos.y < 0 || pos.y > (windowSize.y - 5)) {
		direction.y *= -1;
	}

	if (pos.x < 0 || pos.x >(windowSize.x - 5)) {
		if (pos.x < 0) {
			score++;
		}
		else {
			score--;
		}
		pos = window.getDefaultView().getCenter();
		speed = 5;
	}

	sf::RectangleShape rect(size);
	rect.setPosition(pos);
	window.draw(rect);
}



void Ball::Start(sf::RenderWindow& window) {
	pos = window.getDefaultView().getCenter();
}

void Ball::CheckBounce(Paddle& paddle)
{
	sf::Vector2f padPos = paddle.position;
	sf::Vector2f padSize = paddle.size;

	bool left = pos.x + size.x < padPos.x + padSize.x;
	bool right = pos.x > padPos.x;
	bool top = pos.y + size.y > padPos.y;
	bool bottom = pos.y < padPos.y + padSize.y;

	if (left && right && top && bottom) {
		direction.x *= -1;
		paddle.flash = true;
	}
}
