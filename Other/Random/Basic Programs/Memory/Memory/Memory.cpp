#include <iostream>
#include <string>

int main()
{
	float x = 10.5;
	int y;
	x = y; // implicitly casted from int to float

	int a = 10, b = 8;
	float c = (float)a / (float)b;
	// explicitly casted from int to float
}
