#include "Fill.h"
#include <queue>

void Fill::floodFill(sf::Image& image, int x, int y, sf::Color fillColor)
{
	sf::Color targetColor = image.getPixel(x, y);
	if (targetColor == fillColor) {
		return;
	}
	std::queue<sf::Vector2i> pixelsToFill;
	pixelsToFill.push(sf::Vector2i(x, y));

	while (!pixelsToFill.empty()) {
		sf::Vector2i pixel = pixelsToFill.front();
		pixelsToFill.pop();

		if (pixel.x < 0 || pixel.x >= image.getSize().x || pixel.y < 0 || pixel.y >= image.getSize().y) {
			continue;
		}
		if (image.getPixel(pixel.x, pixel.y) == targetColor) {
			image.setPixel(pixel.x, pixel.y, fillColor);
			pixelsToFill.push(sf::Vector2i(pixel.x - 1, pixel.y));
			pixelsToFill.push(sf::Vector2i(pixel.x + 1, pixel.y));
			pixelsToFill.push(sf::Vector2i(pixel.x, pixel.y - 1));
			pixelsToFill.push(sf::Vector2i(pixel.x, pixel.y + 1));
		}
	}
}

void Fill::mouseDrag(sf::Image& image, sf::Vector2i pos)
{
}

void Fill::mouseDown(sf::Image& image, sf::Vector2i pos)
{
	floodFill(image, pos.x, pos.y, color);
}

void Fill::mouseUp(sf::Image& image, sf::Vector2i pos)
{
}
