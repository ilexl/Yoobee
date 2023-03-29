#pragma once
class Shape
{
private:
	static int counter; // only this class
protected:
	int width, height; // this class and children classes
public:
	Shape(int _width, int _height); // seen anywhere 
	virtual int area() = 0; // pure virtual
};

