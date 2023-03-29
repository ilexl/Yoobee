#include "SFML/Graphics.hpp"
#include "canvas.h"

int main()
{
    sf::RenderWindow window(sf::VideoMode(800, 600), "Paint2.0");
    Canvas canvas;
    canvas.Start(window);
    return 0;
}