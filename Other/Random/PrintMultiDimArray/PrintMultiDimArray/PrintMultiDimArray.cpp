#include <iostream>

using std::cout;

int main()
{
    int MultiDimensionArray[5][5] =
    {
        {0, 1, 2, 3, 4},
        {5, 6, 7, 8, 9},
        {10, 11, 12, 13, 14},
        {15, 16, 17, 18, 19},
        {20, 21, 22, 23, 24}
    };

    cout << "Multi Dimension Array:\n";
    for (int row = 0; row < 5; row++) {
        cout << "------ROW " << row + 1 << "------\n";

        for (int collumn = 0; collumn < 5; collumn++) {
            cout << "Collumn " << collumn + 1 << " : " << MultiDimensionArray[row][collumn] << "\n";
        }

        cout << "\n";
    }

    return 0;
}