#include <iostream>
#include <ctime>

int main()
{
    srand(time(NULL));

    int size = 0;
    std::cout << "Enter array size : ";
    std::cin >> size;

    int* myArray = new int[size];

    for (int i = 0; i < size; i++) {
        int input = 0;
        //std::cout << "Enter value " << (i + 1) << " : ";
        //std::cin >> input;
        input = rand() % 100;
        myArray[i] = input;
    }

    std::cout << "Array = {";
    int sum = 0;
    for (int i = 0; i < size; i++) {
        std::cout << myArray[i];
        if (i == (size - 1)) {
            std::cout << "}\n";
        }
        else {
            std::cout << ", ";
        }
        sum += myArray[i];
    }

    int counter = 0;
    int countW = 0, countF = 0, countOp = 0;
    while (counter < size) {
        for (int i = 0; i < size-1; i++) {
            countF++;
            if (myArray[i] > myArray[i + 1]) {
                countOp++;
                int temp = myArray[i];
                myArray[i] = myArray[i + 1];
                myArray[i + 1] = temp;
            }
        }
        countW++;
        counter++;
    }

    std::cout << "Array = {";
    sum = 0;
    for (int i = 0; i < size; i++) {
        std::cout << myArray[i];
        if (i == (size - 1)) {
            std::cout << "}\n";
        }
        else {
            std::cout << ", ";
        }
        sum += myArray[i];
    }

    std::cout << "Loop Count : " << countW << ", " << "Comparison Count : " << countF << ", " << "Operation Count : " << countOp;

    delete[] myArray;
    return 0;
}
