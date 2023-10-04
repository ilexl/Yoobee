/*
Task 1
Create a structure called soccer that contains two members: a player number (type int) and the player's top speed (in mph, type float). 
Add a variable of type enum position with values that indicate a player's position type: goalkeeper, midfielder, striker, winger and defender.

Write a program so that the user enters three items of information for each of two players: a player number, the player's top speed and the player's position type.

For player’s position type, allow user to specify a type by entering its first letter (‘g’,’m’,’s’,’w’, and so on) then stores the type chosen as a value of a 
variable of type enum position, and finally displays the complete word for this type.

The program should store this information in two variables of type players, and then display their contents.
*/

#include <iostream>
#include <string>

using std::cin;
using std::cout;
using std::string;
using std::getline;
using std::ws;
using std::endl;

// all the player positions
enum position{
    goalkeeper = 'g',
    midfielder = 'm',
    striker = 's',
    winger = 'w',
    defender = 'd'
};

// soccer player data type
struct player {
    int playerNumber;
    float topSpeed;
    position pos;
};

// function prototypes
player GetPlayer(int playerNum);
void DisplayPlayerStats(player player);

int main()
{   
    // two variables of custom data type as per brief 
    player playerOne = GetPlayer(1);
    player playerTwo = GetPlayer(2);

    // display all players, no need for loop as only two
    cout << "All players : \n";
    DisplayPlayerStats(playerOne);
    cout << endl;
    DisplayPlayerStats(playerTwo);

    return 0;
}

// Gets player information
player GetPlayer(int playerNum) {
    bool invalidInput;
    cout << "Player " << playerNum << "\n\n";
    player player;

    // player number
    // loop for valid input
    invalidInput = true;
    while (invalidInput) {
        // Get raw input
        cout << "Enter " << "player number : ";
        string rawInput;
        getline(cin >> ws, rawInput);

        // check if input is a valid int (Without crashing)
        int playerNumber = 0;
        try {
            playerNumber = stoi(rawInput);
            invalidInput = false;
        }
        catch (...) {
            invalidInput = true;
        }

        // Inform user of invalid input or store valid input
        if (invalidInput) {
            cout << "\nInvalid Input...\n\n";
        }
        else {
            player.playerNumber = playerNumber;
        }
    }

    // player top speed
    // loop for valid input
    invalidInput = true;
    while (invalidInput) {
        // Get raw input
        cout << "Enter " << "player's top speed (mph) : ";
        string rawInput;
        getline(cin >> ws, rawInput);

        // check if input is a number (Without crashing)
        float playerTopSpeed = 0;
        try {
            playerTopSpeed = stof(rawInput);
            invalidInput = false;
        }
        catch (...) {
            invalidInput = true;
        }

        // Inform user of invalid input or store valid input
        if (invalidInput) {
            cout << "\nInvalid Input...\n\n";
        }
        else {
            player.topSpeed = playerTopSpeed;
        }
    }

    // player position
    // loop for valid input
    invalidInput = true;
    while (invalidInput) {
        // Get raw input
        cout << "Positions goalkeeper = 'g', midfielder = 'm', striker = 's', winger = 'w', defender = 'd'\n";
        cout << "Enter " << "player position (first letter only): ";
        string rawInput;
        getline(cin >> ws, rawInput);

        // check if input is a valid position (Without crashing)
        char playerPos = rawInput[0];
        position pos;
        try {
            pos = (position)playerPos;
            switch (pos)
            {
            case goalkeeper:
            case midfielder:
            case striker:
            case winger:
            case defender:
                invalidInput = false;
                break;
            default:
                invalidInput = true;
                break;
            }
        }
        catch (...) {
            invalidInput = true;
        }

        // Inform user of invalid input or store valid input
        if (invalidInput) {
            cout << "\nInvalid Input...\n\n";
        }
        else {
            player.pos = pos;
        }
    }

    cout << endl; // console spacing

    return player; // give player object back
}

// Displays player stats with a player instance
void DisplayPlayerStats(player player) {
    // Simple console out
    cout << "Player number          : " << player.playerNumber << endl;
    cout << "Player top speed (mph) : " << player.topSpeed << endl;
    cout << "Player position type   : ";
    
    // Console out based on position
    switch (player.pos)
    {
    case goalkeeper:
        cout << "goal keeper";
        break;
    case midfielder:
        cout << "mid fielder";
        break;
    case striker:
        cout << "striker";
        break;
    case winger:
        cout << "winger";
        break;
    case defender:
        cout << "defender";
        break;
    default:
        break;
    }

    cout << endl; // Console spacing
}