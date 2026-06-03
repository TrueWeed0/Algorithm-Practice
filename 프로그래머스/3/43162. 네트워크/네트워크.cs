using System;

///<summary>
/// 1. 연결 여부를 묻는 문제(네트워크 갯수) = 깊이 탐색 = DFS
/// 2. 0번 -> 1 ~ n번 까지 모두 순회 후 방문 여부 체크
/// 3. 순회 중 연결이 안되어 있을 경우 순회 무시
/// 4. DFS 한 번이 끝나면 하나의 네트워크 탐색이 완료된 것
/// 5. solution에서 0번 ~ n-1번 컴퓨터를 순회하며, 아직 방문하지 않은 컴퓨터를 발견할 때마다 answer++ 한다
///</summary>
public class Solution {
    
    //visited = 방문 여부
    public bool[] visited;

    public int solution(int n, int[,] computers) {
        int answer = 0;

        visited = new bool[n];
        
        for(int i = 0; i<n; i++)
        {
            if(visited[i])
                continue;
            
            DFS(computers,i,n);
            answer++;
        }
        
        return answer;
    }
    
    
    public void DFS(int[,] computers,int current, int n)
    {
        visited[current] = true;
        
        for(int next = 0; next<n; next++)
        {
            if(computers[current, next] == 0)
                continue;
            
            if(visited[next])
                continue;
            
            DFS(computers, next, n);
        }
        
    }
}
