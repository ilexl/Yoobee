#include <iostream>
using namespace std;

int main()
{
    long start = 0, max = 0, difference = 0;

    cout << "Arithmetic Sequence!\n";

    cout << "Enter start value : ";
    cin >> start;

    cout << "Enter max value : ";
    cin >> max;

    cout << "Enter difference value : ";
    cin >> difference;


    long counted = (max - start) / difference;
    //cout << counted;

    long counter = 1;
    while (counter <= counted + difference) {
        long value = start + (counter - 1) * difference;
        if (counter * difference < (max) - difference) {
            cout << value << ", ";
        }
        else {
            cout << value;
        }
        counter++;
    }
}
