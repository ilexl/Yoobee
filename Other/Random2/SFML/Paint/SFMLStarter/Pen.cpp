#include "Pen.h"

void Pen::drawCheckValid(sf::Image& image, int x, int y)
{
	bool onScreenVert = y < image.getSize().y && y > 0;
	bool onScreenHori = x < image.getSize().x && x > 0;
	if (onScreenHori && onScreenVert) {
		image.setPixel(x, y, color);
	}
}

void Pen::mouseDrag(sf::Image& image, sf::Vector2i pos)
{
	sf::Vector2i delta = pos - prevPos;
	float distance = std::sqrt((delta.x * delta.x) + (delta.y * delta.y)); // Pythag A^2 + B^2 = C^2
	float step = 1 / distance;
	for (float t = 0.0; t < 1.0; t += step) {
		int x = (prevPos.x + (t * delta.x));
		int y = (prevPos.y + (t * delta.y));
		drawCheckValid(image, x, y);
	}
	prevPos = pos;
}

void Pen::mouseDown(sf::Image& image, sf::Vector2i pos)
{
	prevPos = pos;
}

void Pen::mouseUp(sf::Image& image, sf::Vector2i pos)
{
	prevPos = pos;
}
