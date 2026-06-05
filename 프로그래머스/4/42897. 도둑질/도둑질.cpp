#include <iostream>
#include <string>
#include <vector>

using namespace std;

///<summary>
/// 1. dp[i] = Max(dp[i-2] + money[i], dp[i-1])
/// 2. 나열된 배열 형태가 아닌 원형 형태
/// 3. 원형 형태임으로 첫 집 방문에 따라 마지막 집 방문여부 결정됨
/// 4. 첫 집 방문에 따른 케이스를 나누고 두 케이스 중 최댓값 반환
///</summary>

//DP
int DP(const vector<int>& money, int start, int end)
{
    int pre1 = 0; //dp[i-1]
    int pre2 = 0; //dp[i-2]
    int current = 0;//dp[i]
    
    for(int i = start; i<end; i++)
    {
        current = max(pre2 + money[i], pre1);
        
        pre2 = pre1;
        pre1 = current;
    }
    
    return pre1;
}

int solution(vector<int> money) {

    //첫 번째 집 방문 케이스
    int case1 = DP(money, 0, money.size() - 1);
    //첫 번째 집 방문 안한 케이스
    int case2 = DP(money, 1, money.size());

    return max(case1, case2);
}