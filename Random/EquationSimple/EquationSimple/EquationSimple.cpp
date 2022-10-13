#include <iostream>
#include <cmath>

using std::cout;
using std::cin;

enum operatorMath {
    add = 0,
    subtract = 1,
    multiply = 2,
    divide = 3,
    modulus = 4,
    null = -1
};

double equation(double a, double b, operatorMath op);

int main()
{
    while (true) {
        cout << "Calculator : \n";

        char in = ' ';
        do {
            cout << "Would you like to perform an calculation - y or n?\n";
            cin >> in;
            if (in <= 'a') {
                in += 32;
            }
        } while (in != 'y' && in != 'n');
        if (in == 'n') {
            cout << "Okay (:\n";
            break;
        }

        cout << "Enter an equation # op #, i.e 1 / 2\n\n";

        cout << "Equation : ";
        double a, b, out = 0;
        operatorMath op;
        char c;

        cin >> a >> c >> b;
        switch (c) {
        case '*':
        case 'x':
        case 'X':
            op = multiply;
            break;
        case '+':
            op = add;
            break;
        case '-':
            op = subtract;
            break;
        case '%':
            op = modulus;
            break;
        case '/':
            op = divide;
            break;
        default:
            op = null;
            break;
        }

        if (op == null) {
            cout << "Invalid operation...\n";
        }
        else {
            out = equation(a, b, op);
        }

        cout << "Output : " << out << "\n\n";
    }
    
}

double equation(double a, double b, operatorMath op) {
    switch (op)
    {
    case add:
        return (a + b);
        break;
    case subtract:
        return (a - b);
        break;
    case multiply:
        return (a * b);
        break;
    case divide:
        if (b != 0) {
            return (a / b);
        }
        else {
            cout << "Cannot divide by 0...\n";
        }
        break;
    case modulus:
        if (b != 0) {
            return (std::fmod(a, b));
        }
        else {
            cout << "Cannot divide by 0...\n";
        }
        break;
    default:
        break;
    }
    return (double)0;
}
