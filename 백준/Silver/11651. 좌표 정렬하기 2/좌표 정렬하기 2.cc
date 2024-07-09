#include <stdio.h>
#include <vector>
#include <algorithm>
using namespace std;

struct Pos
{
    int x, y;
};

bool Compare(Pos pos1, Pos pos2)
{
    if (pos1.y < pos2.y)
    {
        return true;
    }
    else if (pos1.y == pos2.y)
    {
        return  (pos1.x < pos2.x) ? true : false;
    }

    return false;
}

int main()
{
    int posCount;
    vector<Pos> posVector;
    scanf("%d", &posCount);

    while (posCount--)
    {
        Pos newPos;
        scanf("%d %d", &newPos.x, &newPos.y);
        posVector.push_back(newPos);
    }
    
    sort(posVector.begin(), posVector.end(), Compare);
    
    for(auto pos : posVector)
        printf("%d %d\n", pos.x, pos.y);

    vector<Pos>().swap(posVector);
}

