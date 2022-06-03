#include <iostream>
#include "fundamentals.hpp"
using namespace std;
using namespace fundamentals;
int main()
{
    freopen("test.circ","w",stdout);
    init();
    IO(false,2,1);
    IO(false,2,3);
    wire(true,2,1,4);
    wire(true,2,3,4);
    OR(9,2);
    wire(true,9,2,4);
    IO(true,13,2);
    end();
}