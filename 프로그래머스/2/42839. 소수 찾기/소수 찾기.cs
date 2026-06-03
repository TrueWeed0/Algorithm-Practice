using System;
using System.Collections.Generic;

///<summary>
/// 1. 숫자/집합 조합 -> DFS -> 깊이 우선 탐색
/// 2. 모든 노드 탐색하여 조합 구성
/// 3. 조합 후 소수 여부 확인
///</summary>
public class Solution {
    
    public bool[] visited;
    public List<int> nums = new List<int>();
    
    public int solution(string numbers) {
        int answer = 0;
        visited = new bool[numbers.Length];
        
        DFS(numbers, "");
        
        for(int i = 0; i<nums.Count; i++)
        {
            if(nums[i] > 1 && CheckPrimeNumber(nums[i]))
            {
                answer++;
                Console.WriteLine($"소수 : {nums[i]}");
            }
        }
        
        return answer;
    }
    
    //소수 확인 함수
    public bool CheckPrimeNumber(int num)
    {
        //2부터 자기 자신 전까지 모두 나눴을 때 0이 아니어야 함
        for(int i = 2; i<num ; i++)
        {
            if(num % i == 0)
                return false;
        }
        
        return true;
    }
    
    //깊이 우선 탐색
    public void DFS(string numbers, string current)
    {
        //current가 공백이 아닌 경우
        if(current.Length > 0)
        {
            int num = int.Parse(current);
            //중복되지 않은 수일 경우 추가
            if(!nums.Contains(num))
                nums.Add(num);
        }

        for(int i = 0; i<numbers.Length; i++)
        {
            if(visited[i])
                continue;
            
            //현재 탐색한 노드 표시
            visited[i] = true;
            DFS(numbers, current + numbers[i]);
            //탐색 종료 후 표시 해제
            visited[i] = false;
        }
            
    }
}