using System;
using System.Collections.Generic;

///<summary>
/// 1. 길이가 동일한 최단거리 = BFS = 너비 우선 탐색
/// 2. 처음 정점 거리 설정 후 큐 삽입
/// 3. 상, 하, 좌, 우 방향에 따른 인접 노드 탐색
/// 4. 이동 가능 여부 조건 확인
/// 5. 다음 이동할 노드 거리 측정 및 이동
///</summary>

//Queue에 담을 Node 자료형
public class Node
{
    //row = 행, col = 열
    public int row;
    public int col;
    
    public Node(int row, int col)
    {
        this.row = row;
        this.col = col;
    }
}

class Solution {
    
    // dr = 행 위 아래, dc = 열 왼쪽, 오른쪽
    // distance = 정점 별 이동 거리
    public int[] dr = {-1, 1, 0, 0};
    public int[] dc = {0, 0, -1, 1};
    
    public int[,] distance;
    
    public int solution(int[,] maps) {

        //정점 별 이동 거리 초기화
        distance = new int[maps.GetLength(0), maps.GetLength(1)];
        for (int row = 0; row < distance.GetLength(0); row++)
        {
            for (int col = 0; col < distance.GetLength(1); col++)
            {
                distance[row, col] = -1;
            }
        }
        
        BFX(maps, 0,0);
        
        Console.WriteLine($"최단 거리 : {distance[maps.GetLength(0)-1, maps.GetLength(1)-1]}");
        
        return distance[maps.GetLength(0)-1, maps.GetLength(1)-1];
    }
    
    //맵 너비 탐색 함수
    public void BFX(int[,] maps, int startRow, int startCol)
    {
        //인접 노드를 저장할 큐
        Queue<Node> queue = new Queue<Node>();
        
        //초기 값 거리 설정 및 큐 삽입
        distance[startRow, startCol] = 1;
        queue.Enqueue(new Node(startRow, startCol));
        
        //큐가 빌 때 까지 반복
        while(queue.Count > 0)
        {
            //큐의 front 값 꺼냄
            Node current = queue.Dequeue();
            
            //(상 하 좌 우)방향에 있는 인접노드 탐색
            for(int dir = 0; dir<dr.Length; dir++)
            {
                int nextRow = current.row + dr[dir];
                int nextCol = current.col + dc[dir];
                
                //다음 정점 좌표가 맵을 벗어날 경우 스킵
                if(nextRow < 0 || nextRow >= maps.GetLength(0) || nextCol < 0 || nextCol >= maps.GetLength(1))
                    continue;
                
                //벽에 막혀있을 경우 스킵
                if(maps[nextRow, nextCol] == 0)
                    continue;
                
                //이미 거리가 측정된 경우 스킵
                if(distance[nextRow, nextCol] != -1)
                    continue;
                
                //이전 거리 값 + 1 및 방문하지 않은 인접한 다음 정점 큐 삽입
                distance[nextRow, nextCol] = distance[current.row, current.col] + 1;
                queue.Enqueue(new Node(nextRow, nextCol));
            }
        }
        
    }
}