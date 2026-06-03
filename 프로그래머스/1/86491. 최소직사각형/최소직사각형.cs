using System;

///<summary>
/// 1. 가로, 세로 중 가장 큰 값 선정
/// 2. 그 다음 최댓값 선정 시 가로, 세로 중 작은 값과만 비교하여 최댓값 선정
/// 3. 선정한 값 제출
///</summary>


public class Solution {
    public int solution(int[,] sizes) {
        int answer = 0;
        
        // firstMaxValue = 가장 큰 값
        // secondMaxValue = 두 번째로 선택한 큰 값
        int firstMaxValue = -1;
        int secondMaxValue = -1;
        
        //가로, 세로 중 제일 큰 값 선택
        for(int i = 0; i<sizes.GetLength(0); i++)
        {
            if(firstMaxValue < sizes[i,0] || firstMaxValue < sizes[i, 1])
            {
                firstMaxValue = sizes[i,0] > sizes[i, 1] ? sizes[i,0] : sizes[i,1];
            }
        }
        
        //가로, 세로 중에서 작은 값들 기준으로 큰 값 선정
        for(int i = 0; i<sizes.GetLength(0); i++)
        {
            int compareValue = sizes[i,0] > sizes[i,1] ? sizes[i,1] : sizes[i,0];
            if(secondMaxValue < compareValue)
            {
                secondMaxValue = compareValue;
            }
        }
        
        answer = firstMaxValue * secondMaxValue;
        return answer;
    }
}