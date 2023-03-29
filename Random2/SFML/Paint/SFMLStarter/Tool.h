#pragma once
#include "SFML/Graphics.hpp"
class Tool {
public:
	sf::Color color;
	void SetColor(sf::Color c) {
		color = c;
	}
	virtual void mouseDown(sf::Image& image, sf::Vector2i) abstract;
	virtual void mouseDrag(sf::Image& image, sf::Vector2i) abstract;
	virtual void mouseUp(sf::Image& image, sf::Vector2i) abstract;
};