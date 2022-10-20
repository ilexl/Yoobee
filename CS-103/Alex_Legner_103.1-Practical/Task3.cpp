#include <iostream>
#include <string>

using std::cin;
using std::cout;
using std::string;
using std::getline;
using std::ws;
using std::endl;

// function prototypes
void GetMatrixInput(double(&array)[3][3], const int HEIGHT, const int WIDTH);
double** Add(double(&arrayOne)[3][3], double(&arrayTwo)[3][3], const int HEIGHT, const int WIDTH);
double** Multiply(double** matrix, int multiple, const int HEIGHT, const int WIDTH);
void Display(double** matrix, const int HEIGHT, const int WIDTH);

int main()
{
	// Declare matrices in memory
	const int HEIGHT = 3; // const value accross program
	const int WIDTH = 3;  // const value accross program
	double A[HEIGHT][WIDTH], B[HEIGHT][WIDTH]; // two init arrays

	// get matrix input
	cout << "Matrix A:\n";
	GetMatrixInput(A, HEIGHT, WIDTH);

	cout << endl; // console spacing

	// get matrix input
	cout << "Matrix B:\n";
	GetMatrixInput(B, HEIGHT, WIDTH);

	// Get pointers from A & B
	double** APointer = new double* [HEIGHT];
	double** BPointer = new double* [HEIGHT];
	// Duplicate A & B to their Pointers
	for (int i = 0; i < HEIGHT; ++i) {
		APointer[i] = new double[WIDTH];
		BPointer[i] = new double[WIDTH];
		for (int j = 0; j < WIDTH; ++j) {
			APointer[i][j] = A[i][j];
			BPointer[i][j] = B[i][j];
		}
	}

	// Display matrices
	cout << "Matrix A:\n";
	Display(APointer, HEIGHT, WIDTH);

	cout << endl; // console spacing

	// Display matrices
	cout << "Matrix B:\n";
	Display(BPointer, HEIGHT, WIDTH);

	// calculations
	double** C = Add(A, B, HEIGHT, WIDTH); // get new matrix array by adding A + B

	cout << endl; // console spacing

	cout << "C = A + B:\n";
	cout << "Matrix C:\n";
	Display(C, HEIGHT, WIDTH);
	C = Multiply(C, 3, HEIGHT, WIDTH); // get new matrix array by multiplying C * 3
	
	cout << endl; // console spacing

	cout << "Matrix C = (A + B) * 3\n";
	cout << "Matrix C:\n";
	Display(C, HEIGHT, WIDTH);
	
	// delete from memory
	delete C, delete APointer, delete BPointer;
	C = nullptr, APointer = nullptr, BPointer = nullptr;

	return 0;
}

// Gets inputs for every value within a matrix
void GetMatrixInput(double(&array)[3][3], const int HEIGHT, const int WIDTH) {
	for (int i = 0; i < HEIGHT; i ++) {
		// Each row
		cout << "Row " << i + 1 << endl; // Console output
		for (int j = 0; j < WIDTH; j++) {
			// Each item in the row
			cout << "Item " << j + 1 << " : "; // Console output
			
			// Force valid input double
			double input = 0;
			bool invalidInput = true;
			while (invalidInput) {
				string rawInput;
				getline(cin >> ws, rawInput);
				try {
					input = stod(rawInput);
					invalidInput = false;
				}
				catch (...) {
					invalidInput = true;
					cout << "\nInvalid Input...\n\n";
					cout << "Item " << j + 1 << " : ";
				}
			}
			array[i][j] = input;
		}
		cout << endl; // Console spacing
	}
}

// Adds 2 matrices together
double** Add(double(&arrayOne)[3][3], double(&arrayTwo)[3][3], const int HEIGHT, const int WIDTH) {
	double** result = new double* [HEIGHT];
	for (int i = 0; i < HEIGHT; ++i) {
		result[i] = new double[WIDTH];
	}

	for (int i = 0; i < HEIGHT; i++) {
		// Each row
		for (int j = 0; j < WIDTH; j++) {
			double t = arrayOne[i][j] + arrayTwo[i][j]; // add together
			result[i][j] = t; // store in result
		}
	}

	return result;
}

// Multiplies a matrix by a constant
double** Multiply(double** matrix, int multiple, const int HEIGHT, const int WIDTH) {
	double** result = matrix;
	for (int i = 0; i < HEIGHT; i++) {
		// Each row
		for (int j = 0; j < WIDTH; j++) {
			result[i][j] *= 3;
		}
	}
	return result;
}

// Displays a matrix
void Display(double** matrix, const int HEIGHT, const int WIDTH) {
	for (int i = 0; i < HEIGHT; i++) {
		// Each row
		for (int j = 0; j < WIDTH; j++) {
			cout << matrix[i][j] << "\t";
		}
		cout << endl;
	}
}