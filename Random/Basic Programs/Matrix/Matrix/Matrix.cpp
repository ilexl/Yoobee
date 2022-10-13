#include <iostream>
#include <vector>

using std::vector;

class Matrix {
    public:
        class Row {
        public:
            vector <int> values;
            Row(vector <int> input) {
                this->values = input;
            }
        };

        vector <Row> rows;
        Matrix(vector <Row> input) {
            this->rows = input;
        }

        static Matrix Add(Matrix m1, Matrix m2) {
            vector <Row> temp; // Row array

            int i = 0;
            for (Row row : m1.rows) {

                vector <int> collumnsValues; // Row data

                int j = 0;
                for (int value : row.values) {
                    // Creates values in each collumn for the row
                    collumnsValues.push_back(m1.rows[i].values[j] + m2.rows[i].values[j]);
                    j++;
                }

                temp.push_back(collumnsValues);
                i++;
            }

            Matrix tempM = Matrix(temp);
            
            return tempM;
        }

        static Matrix Subtract(Matrix m1, Matrix m2) {
            vector <Row> temp; // Row array
            int i = 0;
            for (Row row : m1.rows) {

                vector <int> collumnsValues; // Row data
                int j = 0;
                for (int value : row.values) {
                    // Creates values in each collumn for the row
                    collumnsValues.push_back(m1.rows[i].values[j] - m2.rows[i].values[j]);
                    j++;
                }

                temp.push_back(collumnsValues);
                i++;
            }


            Matrix tempM = Matrix(temp);

            return tempM;
        }

        static Matrix Multiply(Matrix m1, Matrix m2) {
            vector <Row> temp; // Row array
           
            if (m1.rows[0].values.size() != m2.rows.size()) {
                std::cout << "Matrix's can NOT multiply as they do NOT conform...";
                throw std::invalid_argument("Matrix miss multiplied...");
            }

            for (Row row : m1.rows) {
                vector <int> tempInt;
                for (int value : m2.rows[0].values) {
                    tempInt.push_back(0);
                }
                temp.push_back(tempInt);
            }

            Matrix tempM = Matrix(temp);

            temp.clear();

            int i = 0;
            for (Row row : m1.rows) {

                vector <int> collumnsValues; // Row data
                int j = 0;
                for (int value : m2.rows[i].values) {
                    int k = 0;
                    for (Row row : m2.rows) {
                        tempM.rows[i].values[j] = tempM.rows[i].values[j] + m1.rows[i].values[k] * m2.rows[k].values[j];
                        k++;
                    }
                    j++;
                }

                temp.push_back(collumnsValues);
                i++;
            }

            return tempM;

        }

        void Display() {
            std::cout << "\n";
            int rowCounter = 1;
            for (Row row : this->rows) {
                std::cout << "Row " << rowCounter << " : ";
                for (int value : row.values) {
                    std::cout << value << "\t";
                }
                std::cout << "\n";
                rowCounter++;
            }

            std::cout << "\n";
        }
};

int main()
{
    Matrix matrix1 = Matrix(
        (vector<Matrix::Row>{  
            (vector<int>{ 8, 4, 2 }), 
            (vector<int>{ 6, 3, 6 })
        })
    );
    std::cout << "matrix1\n";
    matrix1.Display();

    Matrix matrix2 = Matrix(
        (vector<Matrix::Row>{  
            (vector<int>{ 5, 2 }),
            (vector<int>{ 3, 4 }),
            (vector<int>{ 6, 8 })
        })
    );
    std::cout << "matrix2\n";
    matrix2.Display();

    // matrix 3
    Matrix matrix3 = Matrix::Multiply(matrix1, matrix2);
    std::cout << "matrix3\n";
    matrix3.Display();
}