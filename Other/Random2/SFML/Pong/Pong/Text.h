#pragma once
#include <iostream>
#include "SFML/Graphics.hpp"
class Text
{
    sf::Text text;
    sf::Font font;
    std::string str = "";
    int size;
    sf::Color color;
    sf::Vector2f pos;
public:
	Text(sf::Font font, int size, sf::Color color, sf::Vector2f pos);
    void Update(sf::RenderWindow& window, std::string text);
};

