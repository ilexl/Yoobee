#include <iostream>
using namespace std;
int main()
{
    int start = 1, seq = 0, difference = 2;

    cout << "Pairs Sequence!\n";

    cout << "Enter sequence value : ";
    cin >> seq;

    int counter = 1;
    while (counter <= seq) {
        long value = start + (counter - 1) * difference;
        if (counter < seq) {
            cout << value << "-" << (value + 1) << " + ";
        }
        else {
            cout << value << "-" << (value + 1);
        }
        counter++;
    }
}
