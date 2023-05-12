#include <iostream>

void Ex1();
void Ex2();
void Ex3();
void Ex4();

int main()
{
    int selection;
    std::cout << "Enter selection (1-4) : ";
    std::cin >> selection;

    switch (selection) {
        case 1:
            Ex1();
            break;
        case 2:
            Ex2();
            break;
        case 3:
            Ex3();
            break;
        case 4:
            Ex4();
            break;
        default:
            std::cout << "That was not an option!";
            break;
    }

    return 0;
}

void Ex1() {
    double z, a, x, b;

    std::cout << "Enter values to solve z = a*x + b\n";

    std::cout << "a=";
    std::cin >> a;

    std::cout << "x=";
    std::cin >> x;

    std::cout << "b=";
    std::cin >> b;

    z = (a * x) + b;
    std::cout << "z = a*x + b" << std::endl;
    std::cout << "z = " << a << "*" << x << " + " << b << std::endl;
    std::cout << "z = " << z << std::endl;
}

void Ex2() {
    double a = 0, r = 0;
    const double PI = 3.1415;

    std::cout << "Find area of the circle!" << std::endl;
    std::cout << "Radius = ";
    std::cin >> r;

    a = PI * (r * r);
    std::cout << "Area = " << a << std::endl;
}

void Ex3() {
    double simpleInterst = 0, principle = 0, interestRate = 0, time = 0;
    std::cout << "Simple Interest Calculator" << std::endl;

    std::cout << "Principle = ";
    std::cin >> principle;

    std::cout << "Interest Rate (0.00 - 1.00) = ";
    std::cin >> interestRate;

    std::cout << "Time = ";
    std::cin >> time;

    simpleInterst = principle * (1 + (interestRate * time));

    std::cout << "Simple Interest = " << simpleInterst;
}

void Ex4() {
    double z = 0, x = 0, y = 0;
    std::cout << "z = x^2 + y^2" << std::endl;

    std::cout << "x = ";
    std::cin >> x;

    std::cout << "y = ";
    std::cin >> y;

    z = (pow(x, 2) + pow(y, 2));

    std::cout << "z = " << z << std::endl;
}