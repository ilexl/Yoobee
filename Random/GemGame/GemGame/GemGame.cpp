#include <iostream>
#include <ctime>

using std::cout;
using std::cin;

struct Vector2 {
    int x;
    int y;
};

void OutputMap(int(&array)[3][3], bool f = false);


int main()
{
    srand((unsigned)time(NULL));
    int points = 0;
    int Map[3][3] =
    {
        {0, 0, 0},
        {0, 0, 0},
        {0, 0, 0}
    };

    // Make 3 random gems to collect
    for (int amount = (rand() % 3) + 1; amount > 0; amount--) {
        bool nopass = true;
        Vector2* randomPos = new Vector2;
        do {
            randomPos->x = (rand() % 3);
            randomPos->y = (rand() % 3);

            if (Map[randomPos->y][randomPos->x] == 0) {
                nopass = false;
                Map[randomPos->y][randomPos->x] = (rand() % 2) + 1; // 1 or 2
            }
        } while (nopass);

        delete randomPos;
        randomPos = nullptr;
    }

    for (int guess = 5; guess > 0; guess--) {
        cout << "You have " << guess << " guess remaining!\n";
        bool invalid = true;
        cout << "Current Map :\n";
        OutputMap(Map);
        do {
            int x, y;
            bool nopass = true;
            Vector2* guessPos = new Vector2;
            do {
                cout << "Enter guess in format \"X Y\" : ";
                cin >> x >> y;

                if (x < 0 || x > 2) {
                    continue;
                }
                if (y < 0 || y > 2) {
                    continue;
                }

                guessPos->x = x;
                guessPos->y = y;

                if (Map[guessPos->y][guessPos->x] != -1 && Map[guessPos->x][guessPos->y] != 0) {
                    nopass = false;
                    cout << "You found a gem, +" << (Map[guessPos->y][guessPos->x] + 1) * 5 << " points!!!\n";
                    points += Map[guessPos->y][guessPos->x] * 5;
                    Map[guessPos->y][guessPos->x] = -1;
                }
                else if (Map[guessPos->y][guessPos->x] == 0) {
                    nopass = false;
                    Map[guessPos->y][guessPos->x] = -1;

                    cout << "You found nothing...\n";
                }
                else {
                    cout << "Invalid input...\n";
                }
            } while (nopass);
            invalid = false;
            delete guessPos;
            guessPos = nullptr;

        } while (invalid);
    }

    // Print final map and points and gg
    OutputMap(Map, true);
    cout << "You scored " << points << " points!!!\n";
    return 0;
}

void OutputMap(int(&Map)[3][3], bool f) {
    for (int x = 0; x < 3; x++) {
        for (int y = 0; y < 3; y++) {
            if (Map[y][x] == -1) {
                cout << "X";
            }
            else {
                if (f) {
                    if (Map[y][x] == 1 || Map[y][x] == 2) {
                        cout << "G";
                    }
                    else {
                        cout << "N";
                    }
                }
                else {
                    cout << "?";
                }
            }
            cout << "  ";
        }
        cout << "\n";
    }
}
