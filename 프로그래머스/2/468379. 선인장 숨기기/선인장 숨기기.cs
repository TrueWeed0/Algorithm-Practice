using System;
using System.Collections.Generic;

/// <summary>
/// 1. 각 행 마다 가로 w 크기 만큼 비 맞는 순서의 최솟값 기록
/// 2. 각 열 마다 세로 h 크기 만큼 비 맞는 순서의 최솟값 기록
/// 3. 각 그룹의 최솟값 중 제일 큰 값의 그룹을 선택
/// </summary>

public class Solution {

    public int[] solution(int m, int n, int h, int w, int[,] drops) {
        int[] answer = new int[2];
        int[][] rainTime = new int[m][];
        int[][] rowMin = new int[m][];
        int max = drops.GetLength(0) + 1;

        for(int i = 0; i<m; i++)
        {
            rainTime[i] = new int[n];
            for(int j = 0; j<n; j++)
            {
                rainTime[i][j] = max;
            }
        }
        
        for(int i = 0; i<drops.GetLength(0); i++)
        {
            int row = drops[i,0];
            int col = drops[i,1];
            
            rainTime[row][col] = i;
        }
        
        //행 마다 각 너비 만큼 최솟값 그룹 저장 (윈도우 슬라이딩)
        for(int i = 0; i<m; i++)
        {
            rowMin[i] = new int[n-w+1];
            LinkedList<int> deque = new LinkedList<int>();
            
            for(int j = 0; j<n; j++)
            {
                //현재 값 보다 큰 값들은 뒤에서 제거
                while(deque.Count > 0 && rainTime[i][deque.Last.Value] >= rainTime[i][j])
                {
                    deque.RemoveLast();
                }
                
                deque.AddLast(j);
                
                //현재 윈도우 범위를 벗어난 인덱스 제거
                if(deque.First.Value <= j-w)
                {
                    deque.RemoveFirst();
                }
                
                //윈도우 크기 w가 되면 최솟값 기록
                if(j >= w-1)
                {
                    int col = j - w + 1;
                    rowMin[i][col] = rainTime[i][deque.First.Value];
                }
            }
        }
        
        int[][] rectMin = new int[m-h+1][];
        
        for (int i = 0; i <= m - h; i++)
        {
            rectMin[i] = new int[n - w + 1];
        }
        
        for(int col = 0; col <= n - w; col++)
        {
            LinkedList<int> deque = new LinkedList<int>();
            
            for(int row = 0; row <m; row++)
            {
                while(deque.Count > 0 && rowMin[deque.Last.Value][col] >= rowMin[row][col])
                {
                    deque.RemoveLast();
                }
                
                deque.AddLast(row);
                
                if(deque.First.Value <= row - h)
                {
                    deque.RemoveFirst();
                }
                
                if(row >= h-1)
                {
                    int startRow = row - h + 1;
                    rectMin[startRow][col] = rowMin[deque.First.Value][col];
                }
            }
        }
        
        int maxTime = 0;
        for(int i = 0; i<rectMin.Length; i++)
        {
            for(int j = 0; j<rectMin[i].Length; j++)
            {
                if(maxTime < rectMin[i][j])
                {
                    answer[0] = i;
                    answer[1] = j;
                    maxTime = rectMin[i][j];
                }
            }
        }
        
        return answer;
    }
    
    public void PrintMap(int[][] rainTime, int max)
    {
        string result = "";
        for(int i = 0; i<rainTime.Length; i++)
        {
            for(int j = 0; j<rainTime[i].Length; j++)
            {
                if(rainTime[i][j] == max)
                {
                    result += "X ";
                }
                else
                {
                    result += rainTime[i][j].ToString() + " ";
                }
            }
            result+="\n";
        }
        
        Console.WriteLine(result);
    }
    
    /*public void FindAbleNode(int m, int n, int h, int w, int time, int[,] drops)
    {
        //List<Node> ableNodeList = new List<Node>();
        String resultTxt = "";
        Node newNode = new Node(-1,-1);
        if(time == 0)
        {
            for(int i = 0; i<m; i++)
            {
                if(i + h > m)
                {
                    continue;
                }

                for(int j = 0; j<n; j++)
                {
                    newNode = new Node(-1,-1);
                    if(j + w <= n)
                    {
                        newNode.x = i;
                        newNode.y = j;

                        ableNodeList.Add(newNode);
                    }

                }

            }
        }
        
        for(int i = ableNodeList.Count - 1; i>=0; i--)
        {
            if(ableNodeList[i].x <= drops[time,0] && drops[time,0] <= ableNodeList[i].x + h - 1)
            {
                if(ableNodeList[i].y <= drops[time,1] && drops[time,1] <= ableNodeList[i].y + w - 1)
                {
                    if(ableNodeList.Count - 1 > 0)
                        ableNodeList.RemoveAt(i);
                }
            }
        }
    }*/
    
}