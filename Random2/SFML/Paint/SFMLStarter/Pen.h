#pragma once
#include "Tool.h"
class Pen : public Tool {
private:
	sf::Vector2i prevPos;
	void drawCheckValid(sf::Image& image, int x, int y);
public:
	Pen()
	{
		SetColor(sf::Color::Black);
	}
	void mouseDrag(sf::Image& image, sf::Vector2i pos) override;
	void mouseDown(sf::Image& image, sf::Vector2i pos) override;
	void mouseUp(sf::Image& image, sf::Vector2i pos) override;
};

