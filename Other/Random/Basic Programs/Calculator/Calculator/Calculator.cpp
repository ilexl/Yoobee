#include <iostream>
#include <string>

double Add(double val1, double val2);
double Multiply(double val1, double val2);
double Minus(double val1, double val2);
double Divide(double val1, double val2);
bool CheckExit(std::string val);
std::string OutputOp(char op, double storedValue);

int main()
{
    bool running = true, passed;
    double temp, _temp, storedValue = 0;
    std::string input;
    char op = ' ';
    
    std::cout << "Simple Calculator!\n\n";
    
    while (running) {
        try {
            std::cout << "Stored value = " << storedValue << "\n";
            std::cout << "Enter opertation (+, -, *, /, exit / e) : ";
            
            input = "";
            std::getline(std::cin, input);
           
            if (CheckExit(input)) {
                running = false;
                break;
            }

            if (input == "+") {
                op = '+';
                input = OutputOp(op, storedValue);
                temp = stod(input);
                _temp = Add(storedValue, temp);
            }
            else if (input == "-") {
                op = '-';
                input = OutputOp(op, storedValue);
                temp = stod(input);
                _temp = Minus(storedValue, temp);
            }
            else if (input == "*") {
                op = '*';
                input = OutputOp(op, storedValue);
                temp = stod(input);
                _temp = Multiply(storedValue, temp);
            }
            else if (input == "/") {
                op = '/';
                input = OutputOp(op, storedValue);
                temp = stod(input);
                if (temp == 0) {
                    throw std::invalid_argument("Invalid input...");
                }
                _temp = Divide(storedValue, temp);
            }
            else {
                throw std::invalid_argument("Invalid input...");
            }

            std::cout << storedValue << " " << op << " " << temp << " = " << _temp << "\n\n";
            storedValue = _temp;
        }
        catch (...) {
            std::cout << "Invalid input/s... \n\n";
            continue;
        }
    }
    
    return 0;
}

std::string OutputOp(char op, double storedValue) {
    std::string input;
    std::cout << storedValue << " " << op << " ";

    input = "";
    std::getline(std::cin, input);
    return input;
}

double Add(double val1, double val2) {
    double result = val1 + val2;

    return result;
}

double Multiply(double val1, double val2) {
    double result = val1 * val2;

    return result;
}

double Minus(double val1, double val2) {
    double result = val1 - val2;

    return result;
}

double Divide(double val1, double val2) {
    double result = val1 / val2;

    return result;
}

bool CheckExit(std::string val) {
    if (val == "exit" || val == "e" || val == "Exit" || val == "E") {
        return true;
    }
    else {
        return false;
    }
}