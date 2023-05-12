#include <iostream>

int main()
{
    int x = 0, y = 0, z = 0;

    std::cout << "Algebra equations : \n";

    // z = 3xy - 8 + 2y
    x = 0, y = 0, z = 0;
    std::cout << "z = 3xy - 8 + 2y\n";
    std::cout << "Enter value x : ";
    std::cin >> x;
    std::cout << "Enter value y : ";
    std::cin >> y;
    z = (3 * x * y) - 8 + (2 * y);
    std::cout << "z = 3*" << x << "*" << y << " - 8 + 2*" << y << "\n";
    std::cout << "z = " << z << "\n";

    std::cout << "\n";

    // z = x^2 + 3x + y^2
    x = 0, y = 0, z = 0;
    std::cout << "z = x^2 + 3x + y^2\n";
    std::cout << "Enter value x : ";
    std::cin >> x;
    std::cout << "Enter value y : ";
    std::cin >> y;
    z = (x * x) + (3 * x) + (y * y);
    std::cout << "z = " << x << "^2 + 3*" << x << " + " << y << "^2\n";
    std::cout << "z = " << z << "\n";

    std::cout << "\n";

    // z = 2xy - 5y + 3
    x = 0, y = 0, z = 0;
    std::cout << "z = 2xy - 5y + 3\n";
    std::cout << "Enter value x : ";
    std::cin >> x;
    std::cout << "Enter value y : ";
    std::cin >> y;
    z = (2 * x * y) - (5 * y) + (3);
    std::cout << "z = 2*" << x << "*"<<y<<" - 5*"<<y<<" + 3\n";
    std::cout << "z = " << z << "\n";

    return 0;
}