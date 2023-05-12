#include <iostream>
#include "Employee.h"

using std::cout;

int main()
{
    Employee employees[5];

    for (int i = 1; i <= 5; i++) {
        Employee employee = Employee(i);
        employee.getData();
        employees[i - 1] = employee;
    }

    for (int i = 1; i <= 5; i++) {
        employees[i - 1].displayData();
    }
}


