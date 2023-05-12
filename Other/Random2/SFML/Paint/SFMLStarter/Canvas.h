#pragma once
#include "SFML/Graphics.hpp"
#include "Tool.h"
#include "Pen.h"
#include "Fill.h"

class Canvas
{
private:
	sf::Image image;
	sf::Texture texture;
	Tool* tool;
	Pen pen;
	Fill fill;

public:
	void Start(sf::RenderWindow& window);
};

