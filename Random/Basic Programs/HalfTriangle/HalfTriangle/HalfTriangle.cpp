#include <iostream>
#include <string>

using namespace std;
int main()
{
    int size;

    cout << "Enter Triangle Size (1-2147483647) : ";
    cin >> size;

    for (int i = 1; i <= size; i++) {
        for (int j = 1; j <= i; j++) {
            cout << "*";
        }
        cout << endl;
    }
}

