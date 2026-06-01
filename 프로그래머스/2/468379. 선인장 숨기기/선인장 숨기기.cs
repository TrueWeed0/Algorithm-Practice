using System;
using System.Collections.Generic;

/// <summary>
/// 1. 각 칸이 몇 번째에 비를 맞는지 기록
/// 2. 각 행마다 가로 w칸 구간의 최솟값을 기록
/// 3. 행마다 기록한 최솟값을 세로 h칸씩 비교하여 h*w 영역의 최솟값을 기록
/// 4. 각 그룹의 최솟값 중 제일 큰 값의 그룹을 선택
/// </summary>

public class Solution {

    // rainTime = 각 칸이 비를 맞는 시간 기록
    // m = 행 크기, n = 열 크기, h = 높이, w = 너비
    // maxTime = 비 맞는 시간의 최댓값
    public int[][] rainTime;
    public int m,n,h,w;
    public int maxTime;
    
    public int[] solution(int m, int n, int h, int w, int[,] drops) {
        // answer = 최종 좌표값
        // rowMin = 가로 w칸 구간의 비 맞는 시간의 최솟값
        // rectMin = h*w 영역의 최솟값
        int[] answer = new int[2];
        int[][] rowMin = new int[m][];
        int[][] rectMin = new int[m-h+1][];

        rainTime = new int[m][];
        maxTime = drops.GetLength(0) + 1;
        this.m = m;
        this.n = n;
        this.h = h;
        this.w = w;

        Init(drops);        
        SetRowMin(rowMin);
        SetRectMin(rectMin, rowMin);        
        answer = FindMaxTime(rectMin);
        
        return answer;
    }
    
    // 각 칸이 비를 맞는 시간 기록 함수
    public void Init(int[,] drops)
    {
        //모든 칸을 maxTime 기록
        for(int i = 0; i<m; i++)
        {
            rainTime[i] = new int[n];
            for(int j = 0; j<n; j++)
            {
                rainTime[i][j] = maxTime;
            }
        }
        
        //drops 순서대로 각 타일이 비를 맞은 시간 기록
        for(int i = 0; i<drops.GetLength(0); i++)
        {
            int row = drops[i,0];
            int col = drops[i,1];
            
            rainTime[row][col] = i;
        }
    }
    
    //각 행마다 가로 w칸 구간의 최솟값을 기록하는 함수
    public void SetRowMin(int[][] rowMin)
    {
        //(윈도우 슬라이딩)
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
    }
    
    // Row 기준으로 h*w 영역의 최솟값을 계산하는 함수
    public void SetRectMin(int[][] rectMin, int[][] rowMin)
    {
        //rectMin 크기 초기화
        for (int i = 0; i <= m - h; i++)
        {
            rectMin[i] = new int[n - w + 1];
        }
        
        //각 col마다 세로 h칸 슬라이딩 최솟값 계산
        for(int col = 0; col <= n - w; col++)
        {
            LinkedList<int> deque = new LinkedList<int>();
            
            //같은 col에서 rowMin을 세로 h칸씩 비교하여 h*w 영역의 최솟값 계산
            for(int row = 0; row <m; row++)
            {
                //현재 값 보다 큰 값들은 뒤에서 제거
                while(deque.Count > 0 && rowMin[deque.Last.Value][col] >= rowMin[row][col])
                {
                    deque.RemoveLast();
                }

                deque.AddLast(row);
                
                //현재 높이 범위를 벗어난 인덱스 제거
                if(deque.First.Value <= row - h)
                {
                    deque.RemoveFirst();
                }
                
                //세로 h칸 윈도우가 완성되면 최솟값 기록
                if(row >= h-1)
                {
                    int startRow = row - h + 1;
                    rectMin[startRow][col] = rowMin[deque.First.Value][col];
                }
            }
        }
    }
    
    // 각 h*w 영역의 최솟값 중 가장 큰 값을 가진 시작 row, col 반환 함수
    public int[] FindMaxTime(int[][] rectMin)
    {
        int[] result = new int[2];
        int maxTime = 0;
        for(int row = 0; row<rectMin.Length; row++)
        {
            for(int col = 0; col<rectMin[row].Length; col++)
            {
                if(maxTime < rectMin[row][col])
                {
                    result[0] = row;
                    result[1] = col;
                    maxTime = rectMin[row][col];
                }
            }
        }
        
        return result;
    }

}
