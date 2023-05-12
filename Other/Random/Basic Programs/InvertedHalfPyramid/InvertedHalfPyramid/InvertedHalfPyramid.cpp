#include <iostream>
#include <string>

using namespace std;
int main()
{
    int size;

    cout << "Enter Triangle Size (1-2147483647) : ";
    cin >> size;

    for (int i = size; i >= 1; i--) {
        for (int j = i; j >= 1; j--) {
            std::cout << j << " ";
        }
        std::cout << "\n";
    }

    return 0;
}

