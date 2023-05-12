#include <iostream>

int main()
{
    int* pointer = new int;
    *pointer = 0;

    std::cout << "Pointer points to address - 0x" << pointer << " in the heap which holds the value - " << *pointer << "\n";
    
    delete pointer;
    pointer = nullptr;

    return 0;
}