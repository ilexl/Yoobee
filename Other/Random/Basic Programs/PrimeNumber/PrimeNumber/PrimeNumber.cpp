#include <iostream>
#include <string>
using namespace std;

int main() {
    int n, i;

    cout << "Enter a positive integer: ";
    cin >> n;

    int counter = 0;
    for (i = 1; i <= n; ++i) {
        if (n % i == 0)
            counter++;
    }

    //cout << counter << endl;

    if (counter > 2) {
        cout << n << " is NOT a prime number!";
    }
    else {
        cout << n << " is a prime number!";
    }

    return 0;
}