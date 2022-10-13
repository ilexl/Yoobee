#include <iostream>

using std::string;
using std::cin;
using std::cout;

double GetValue(string consoleText);
char GetOperator(string consoleText);

int main()
{
    std::cout << "Calculator\n\n";
    double a, b, out = 0;
    char op;
    bool valid = false;

    a = GetValue("Enter value A : ");
    op = GetOperator("Enter operation (+ - * /)");
    b = GetValue("Enter value B : ");

    switch (op) {
    case '+':
        out = a + b;
        valid = true;
        break;
    case '-':
        out = a - b;
        valid = true;
        break;
    case '*':
        out = a * b;
        valid = true;
        break;
    case '/':
        if (b == 0) {
            cout << "Invalid input...\n";
        }
        else {
            out = a / b;
            valid = true;
        }
        break;
    default:
        cout << "Invalid input...\n";
        break;
    }

    if (valid) {
        cout << "Output : " << out << "\n";
    }
}

double GetValue(string consoleText) {
    cout << consoleText;
    double in;
    cin >> in;
    return in;
}

char GetOperator(string consoleText) {
    cout << consoleText;
    char in;
    cin >> in;
    return in;
}