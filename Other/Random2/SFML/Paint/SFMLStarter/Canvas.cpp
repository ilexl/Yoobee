#include "canvas.h"
#include "SFML/Graphics.hpp"

void Canvas::Start(sf::RenderWindow& window)
{
    image.create(800, 600, sf::Color::White);
    tool = &pen; // set current tool to pen

    const int colorsAvaliable = 5;
    sf::Color colors[colorsAvaliable] = { sf::Color::Black, sf::Color::White , sf::Color::Red , sf::Color::Green , sf::Color::Blue };
    int colorSelect = 0;

    while (window.isOpen()) {
        sf::Vector2i mousePos = sf::Mouse::getPosition(window);

        sf::Event currentEvent;
        while (window.pollEvent(currentEvent)) {
            if (currentEvent.type == sf::Event::Closed) {
                window.close();
            }
            if (currentEvent.type == sf::Event::MouseButtonPressed) {
                tool->mouseDown(image, mousePos);
            }
            if (currentEvent.type == sf::Event::KeyPressed) {
                if (sf::Keyboard::isKeyPressed(sf::Keyboard::Tab)) {
                    colorSelect++;
                    if (colorSelect >= colorsAvaliable) {
                        colorSelect = 0;
                    }
                    tool->SetColor(colors[colorSelect]);
                }
            }
        }

        if (sf::Mouse::isButtonPressed(sf::Mouse::Left)) {
            tool->mouseDrag(image, mousePos);
        }
        if (sf::Keyboard::isKeyPressed(sf::Keyboard::F)) {
            tool = &fill;
        }
        if (sf::Keyboard::isKeyPressed(sf::Keyboard::D)) {
            tool = &pen;
        }
        

        window.clear(sf::Color::White); // clears the window
        texture.loadFromImage(image);
        sf::Sprite sprite = sf::Sprite(texture) ;
        window.draw(sprite);
        window.display(); // displays the window current frame
    }
}
