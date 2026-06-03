using System;
using System.Collections.Generic;
using System.Linq;

///<summary>
/// 1. 최소 단계 과정 -> BFS 너비 우선 탐색
/// 2. words에서 각각 알파벳이 1개만 다른것 중심으로 그래프 생성
/// 3. begin에서 각 그래프의 최단 이동 횟수 기록
/// 4. target의 최단 이동 횟수 반환
///</summary>
public class Solution {
    
    //distance = 정점으로 이동하는 최단 거리
    //graph = words에서 알파벳 1개를 변경하여 바꿀 수 있는 인접 리스트 형 그래프
    public int[] distance;
    public List<int>[] graph;
    
    public int solution(string begin, string target, string[] words) {
        int answer = 0;
        
        if(Array.IndexOf(words,target) == -1)
            return answer;
        
        //begin을 포함한 전체 단어 리스트 생성
        List<string> allString = new List<string>();
        allString.Add(begin);
        allString.AddRange(words.ToList());
        //graph, distance 초기화
        graph = new List<int>[allString.Count];
        distance = new int[allString.Count];
        
        //전체 단어들을 서로 비교하여 그래프 생성
        for(int i = 0; i<allString.Count; i++)
        {
            graph[i] = new List<int>();
            distance[i] = -1;
            for(int j = 0; j<allString.Count; j++)
            {
                if(allString[i].Length != allString[j].Length)
                    continue;
                
                if(CanConvert(allString[i], allString[j]))
                {
                    graph[i].Add(j);
                }
            }
        }
        
        BFX(words, 0);
        
        int idx = allString.IndexOf(target);
        answer = distance[idx];
        Console.WriteLine($"최단 횟수 : {answer}");
        
        return answer;
    }
    
    public void BFX(string[] words, int start)
    {
        //인접 노드를 저장할 큐 생성
        Queue<int> queue = new Queue<int>();
        
        //start 노드 거리 설정 및 큐 삽입
        distance[start] = 0;
        queue.Enqueue(start);
        
        //큐가 비어질 때 까지 반복
        while(queue.Count > 0)
        {
            //큐의 front 꺼냄
            int current = queue.Dequeue();
            
            //현재 해당하는 노드의 인접 노드 거리 설정 및 큐 삽입
            foreach(int next in graph[current])
            {
                if(distance[next] != -1)
                    continue;
                
                distance[next] = distance[current] + 1;
                queue.Enqueue(next);
            }
        }
        
    }
    
    //알파벳 1개를 변경하여 단어를 바꿀 수 있는지 검사 함수
    public bool CanConvert(string a, string b)
    {
        int result = 0;

        for (int i = 0; i < a.Length; i++)
        {
            if (a[i] != b[i])
                result++;
        }
        
        //다른 글자가 1개일 경우 가능
        return result == 1;
    }
}