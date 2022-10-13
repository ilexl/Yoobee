#include <iostream>
#include <ctime>
#include <string>

using std::cin;
using std::cout;
using std::string;

int main()
{

    int shiphandeler[2][6] = {
        {0,1,0,0,0,0,},
        {1,0,0,1,0,1,}
        //4 ships
    };

    int hit = 0;
    cout << "\n\n\n loading battle ships....\n\n\n";
    cout << "select coordinate \n\n if selected correctly you will sink a ship\n\n";

    while (hit < 4) {
        cout << "choose a row between 0 and 1: ";
        int row;
        cin >> row;

        cout << "choose a collom between 0 and 5: ";
        int col;
        cin >> col;

        if (shiphandeler[row][col]) {
            shiphandeler[row][col] = 0;
            hit++;
            cout << "ship hit :]  " << (4 - hit) << " ships left.\n\n";
        }
        else {
            cout << "you missed \n\n";
        }
    }
    cout << "you win :D";
}
