using System;

public class Solution {

    public int answer;
    
    //완전 탐색 , DFS
    public void DFS(int[] numbers, int target, int idx, int sum)
    {
        //idx가 그래프 끝일 경우
        if(idx == numbers.Length)
        {
            //지금까지 모두 합한 결과가 target과 일치 시
            if(target == sum)
            {
                answer++;
            }
            return;
        }
        //numbers[idx] 값이 음수일 경우
        DFS(numbers, target, idx + 1, sum - numbers[idx]);
        //numbers[idx] 값이 양수일 경우
        DFS(numbers, target, idx + 1, sum + numbers[idx]);
    }
    
    public int solution(int[] numbers, int target) {
        answer = 0;
        
        DFS(numbers, target, 0, 0);
        
        return answer;
    }
}