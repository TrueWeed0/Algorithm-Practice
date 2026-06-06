using System;
using System.Collections.Generic;

public class Solution {
    public int[] solution(int[] array, int[,] commands) {
        int[] answer = new int[commands.GetLength(0)];
        List<int> sortList = new List<int>();
        for(int i = 0; i<commands.GetLength(0); i++)
        {
            int start = commands[i,0]-1;
            int end = commands[i,1]-1;
            int idx = commands[i,2]-1;
            
            for(int j =start; j<=end; j++)
            {
                sortList.Add(array[j]);
            }
            
            sortList.Sort((a,b) => a.CompareTo(b));
            answer[i] = sortList[idx];
            sortList.Clear();
        }
        return answer;
    }
}