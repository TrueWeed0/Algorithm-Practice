#include <iostream>
#include <cmath>

using namespace std;

int main()
{
    int testCount, x1, x2, y1, y2, r1, r2;
    double d;
    cin >> testCount;
    while (testCount--)
    {
        cin >> x1 >> y1 >> r1 >> x2 >> y2 >> r2;
        d = sqrt(pow(x2 - x1, 2) + pow(y2 - y1, 2));
        if (d > (r1 + r2) || d < fabs(r1 - r2))
        {
            cout << 0 << endl;
        }
        else if (r1 == r2 && d == 0)
        {
            cout << -1 << endl;
        }
        else if (d == (r1 + r2) || d == fabs(r1 - r2))
        {
            cout << 1 << endl;
        }
        else
        {
            cout << 2 << endl;
        }
    }

    return 0;
}

