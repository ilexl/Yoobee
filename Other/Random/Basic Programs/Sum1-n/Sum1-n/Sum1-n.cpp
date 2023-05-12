#include <iostream>
#include <string>

int main()
{
    int n, total = 0;
    std::cout << "Calculate 1 + 2 + 3 + ...  + n\n";
    std::cout << "Enter value n : ";
    std::cin >> n;

    std::string calcStr = "";

    for (int i = 1; i <= n; i++) {
        if (i != n) {
            calcStr.append(std::to_string(i) + " + ");
        }
        else {
            calcStr.append(std::to_string(i));
        }
        total += i;
    }

    std::cout << calcStr << " = " << total;

}
