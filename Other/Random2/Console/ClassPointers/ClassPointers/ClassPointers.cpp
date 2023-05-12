#include <iostream>
#include <vector>

class Base
{
public:
    int id = 7;
};


int main()
{
    // non working
    std::vector<Base*> BasesNON;

    for (int i = 0; i < 10; i++) {
        Base base;
        base.id = i;
        BasesNON.push_back(&base);
    }

    for (Base* b : BasesNON) {
        std::cout << b->id;
    }

    std::cout << std::endl;

    // working
    std::vector<Base*> Bases;

    for (int i = 0; i < 10; i++) {
        Base* base = new Base();
        base->id = i;
        Bases.push_back(base);
    }

    for (Base* b : Bases) {
        std::cout << b->id;
    }

}