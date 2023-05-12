#include <iostream>
#include <cmath>

using std::cout;
using std::cin;
using std::sin;
using std::cos;
using std::tan;

void Trig(char f);
// Trig is in radians

int main()
{
    std::cout << "Trig Calculator\n";
    bool running = true;
    while (running) {
        cout << "options:\n";
        cout << "s - sin(x)\n";
        cout << "c - cos(x)\n";
        cout << "t - tan(x)\n";
        cout << "e - exit\n";
        bool invalidInput = true;
        char input = ' ';
        do {
            cin >> input;
            switch (input) {
            case 'e':
                invalidInput = false;
                running = false;
                break;
            case 'c':
                invalidInput = false;
                Trig(input);
                break;
            case 's':
                invalidInput = false;
                Trig(input);
                break;
            case 't':
                invalidInput = false;
                Trig(input);
                break;
            default:
                cout << "Invalid input...\n";
                std::cin.clear();
                break;
            }
        } while (invalidInput);
        
    }
}

void Trig(char f) {
    try {
        double input = 0;
        switch (f) {
            case 's':
                cout << "Sin ";
                break;

            case 'c':
                cout << "Cos ";

                break;

            case 't':
                cout << "Tan ";

                break;

            default:
                break;
        }
        cin >> input;
        double output = 0;
        switch (f) {
        case 's':
            output = sin(input);
            break;

        case 'c':
            output = cos(input);
            break;

        case 't':
            output = tan(input);
            break;

        default:
            break;
        }
        switch (f) {
        case 's':
            cout << "Sin(" << input << ") = " << output << "\n";
            break;

        case 'c':
            cout << "Cos(" << input << ") = " << output << "\n";
            break;

        case 't':
            cout << "Tan(" << input << ") = " << output << "\n";
            break;

        default:
            break;
        }
    }
    catch (...) {
        cout << "Error...\n";
        std::cin.clear();
    }
}