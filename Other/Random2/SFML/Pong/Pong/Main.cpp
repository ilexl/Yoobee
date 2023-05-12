#include <iostream>
#include "SFML/Graphics.hpp"
#include "Paddle.h"
#include "Ball.h"
#include "Text.h"
#include <string>
#include <math.h>

int main()
{
    const int WINDOW_WIDTH = 1600;
    const int WINDOW_HEIGHT = 900;
    
    sf::RenderWindow window(sf::VideoMode(WINDOW_WIDTH, WINDOW_HEIGHT), "Pong");
    window.setFramerateLimit(60);

    sf::Font font;
    if (!font.loadFromFile("arial.ttf"))
    {
        std::cerr << "Error while loading font" << std::endl;
        return -1;
    }

    Ball ball;
    Paddle playerOne(sf::Vector2f(0.0, WINDOW_HEIGHT/2 - 50), sf::Keyboard::W, sf::Keyboard::S, sf::Color::Blue);
    Paddle playerTwo(sf::Vector2f((WINDOW_WIDTH - 20), WINDOW_HEIGHT / 2 - 50), sf::Keyboard::Up, sf::Keyboard::Down, sf::Color::Red);

    Text textOne = Text(font, 100, sf::Color::White, sf::Vector2f(725.0f, 415.0f));
    Text textTwo = Text(font, 100, sf::Color::White, sf::Vector2f(875.0f, 415.0f));

    int scoreOffset = 0;;

    int scoreOne = 0;
    int scoreTwo = 0;

    ball.Start(window);
    while (window.isOpen()) {
        sf::Event currentEvent;
        while (window.pollEvent(currentEvent)) {
            if (currentEvent.type == sf::Event::Closed) {
                window.close();
            }

            // EVENTS HERE
        }
        window.clear(); // clears the window

        if (scoreOffset != 0) {
            if (scoreOffset >= 1) {
                scoreOffset = 0;
                scoreTwo++;
            }
            else if (scoreOffset <= -1) {
                scoreOne++;
                scoreOffset = 0;
            }
        }

        //std::cerr << ball.pos.x << ", " << ball.pos.y << "\n";

        ball.Update(window, scoreOffset);
        playerOne.Update(window);
        playerTwo.Update(window);
        ball.CheckBounce(playerOne);
        ball.CheckBounce(playerTwo);
        
        textOne.Update(window, std::to_string(scoreOne));
        textTwo.Update(window, std::to_string(scoreTwo));

        window.display(); // displays the window current frame
    }

    return 0;
}

