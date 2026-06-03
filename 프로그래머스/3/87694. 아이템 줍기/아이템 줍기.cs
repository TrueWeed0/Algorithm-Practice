using System;
using System.Collections.Generic;

///<summary>
/// 1. 최단 거리 탐색 문제 + 이동 거리는 일정 = BFS 너비 우선 탐색
/// 2. x,y 좌표값은 1~50 사이 -> 101크기의 맵 생성
/// 3. 도형 테두리의 굴곡진 부분을 표현하기 위해선 좌표, 이동 값에 *2를 해줘야 함
/// 3. 각 사각형 크기만큼 맵 채움
/// 4. 사각형 테두리만 남기기 위해 내부만 비움
/// 5. 생성된 맵 BFS 탐색 (탐색 시 캐릭터 좌표 *2 적용)
/// 6. 아이템 거리 출력 (거리 출력 시 아이템 좌표*2 후 최종 거리 * 1/2)
///</summary>

//좌표 클래스
public class Pos
{
    public int x;
    public int y;
    
    public Pos(int x, int y) { this.x = x; this.y = y;}
}

public class Solution {
    
    // map = 50 * 2 사이즈의 테두리, 내부, 외부 표시 정보
    // distance = 각 좌표 별 거리
    // dx, dy = 상 하 좌 우
    public int[,] map = new int[101,101];
    public int[,] distance = new int[101,101];
    public int[] dx = {-1, 1, 0, 0};
    public int[] dy = {0, 0, -1, 1};
    
    public int solution(int[,] rectangle, int characterX, int characterY, int itemX, int itemY) {
        int answer = 0;
        
        //캐릭터, 아이템 좌표 * 2
        characterX *= 2;
        characterY *= 2;
        itemX *= 2;
        itemY *= 2;
        
        //거리 초기화
        for(int i = 0; i<distance.GetLength(0); i++)
        {
            for(int j = 0; j<distance.GetLength(1); j++)
            {
                distance[i,j] = -1;
            }
        }
        
        CreateMap(rectangle);
        
        BFS(characterX, characterY);
        
        //최종 거리 제출 시 * 1/2;
        answer = distance[itemX, itemY]/2;
        Console.WriteLine($"최단 거리 길이 : {answer}");
        return answer;
    }
    
    public void BFS(int startX, int startY)
    {
        Queue<Pos> queue = new Queue<Pos>();
        
        distance[startX,startY] = 0;
        queue.Enqueue(new Pos(startX, startY));
        
        while(queue.Count > 0)
        {
            Pos current = queue.Dequeue();
            
            for(int dir = 0; dir<dx.Length; dir++)
            {
                int nextX = current.x + dx[dir];
                int nextY = current.y + dy[dir];
                
                if(nextX < 0 || nextX >= map.GetLength(0) || nextY < 0 || nextY >= map.GetLength(1))
                    continue;
                
                if(map[nextX,nextY] != 1)
                    continue;
                
                if(distance[nextX,nextY] != -1)
                    continue;
                
                distance[nextX,nextY] = distance[current.x, current.y] + 1;
                queue.Enqueue(new Pos(nextX,nextY));
            }
        }
    }
    
    //맵 생성 함수
    public void CreateMap(int[,] rectangle)
    {
        //맵에 각 사각형 별로 테두리, 내부, 외곽 표시
        for(int i = 0; i<rectangle.GetLength(0); i++)
        {
            //그래프 탐색 시 테두리 굴곡에 대한 처리를 위해 좌표 * 2
            int x1 = rectangle[i,0] * 2;
            int y1 = rectangle[i,1] * 2;
            int x2 = rectangle[i,2] * 2;
            int y2 = rectangle[i,3] * 2;

            for(int x = x1; x<=x2; x++)
            {
                for(int y = y1; y<=y2; y++)
                {
                
                    //x or y값이 테두리에 위치했을 경우
                    // map -> 0 = 외부, 1 = 테두리, 2 = 내부
                    bool isOutLine = x == x1 || x == x2 || y == y1 || y == y2;
                    if(isOutLine)
                    {
                        if(map[x,y] != 2)
                            map[x,y] = 1;
                    }
                    else
                    {
                        map[x,y] = 2;
                    }
                }
            }
        }
    }
}