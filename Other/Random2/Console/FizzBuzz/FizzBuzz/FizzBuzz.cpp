#include <iostream>

using std::cout;
using std::endl;

int main()
{

    for (int i = 1; i <= 100; i++) {
        if (i % 3 == 0 || i % 5 == 0) { // check if either match
            if (i % 3 == 0 && i % 5 == 0) { // if both match
                cout << "FIZZBUZZ" << endl;
            }
            else {
                if (i % 3 == 0) {
                    cout << "FIZZ" << endl;
                }
                else if (i % 5 == 0) {
                    cout << "BUZZ" << endl;
                }
                else {
                    cout << "YOU FUCKED THIS SHOULDNT PRINT EVER (:\n";
                }
            }
        }
        else {
            cout << i << endl; // print non match
        }
        
    }

}

