#pragma once
#include "Tool.h"
class Fill : public Tool {
public:
	Fill()
	{
		SetColor(sf::Color::Black);
	}
	void floodFill(sf::Image& image, int x, int y, sf::Color fillColor);
	void mouseDrag(sf::Image& image, sf::Vector2i pos) override;
	void mouseDown(sf::Image& image, sf::Vector2i pos) override;
	void mouseUp(sf::Image& image, sf::Vector2i pos) override;
};

