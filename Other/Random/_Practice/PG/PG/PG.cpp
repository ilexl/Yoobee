#include <iostream>
#include <string>

using std::cin;
using std::cout;
using std::string;
using std::endl;

int main() {
	int* pointer = new int; // int stored in the heap
	*pointer = 951;

	cout << pointer << endl;
	cout << *pointer << endl;

	delete pointer;
	pointer = nullptr;
}
