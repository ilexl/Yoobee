#include "Text.h"

Text::Text(sf::Font font, int size, sf::Color color, sf::Vector2f pos)
{
	this->font = font;
	this->size = size;
	this->color = color;
	this->pos = pos;
}

void Text::Update(sf::RenderWindow& window, std::string txt)
{
	text = sf::Text();
	text.setFont(font);
	text.setCharacterSize(size);
	text.setFillColor(color);
	text.setString(txt);
	text.setOrigin(text.getLocalBounds().width / 2.0f, text.getLocalBounds().height / 2.0f);
	text.setPosition(pos);
	window.draw(text);
}
