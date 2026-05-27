using System;
using System.Linq;
using System.Collections.Generic;

public class Solution 
{
    public int solution(int n, Func<int, string> submit)
    {
        //candidates = 후보군
        //submitResult = 제출 결과, guess = 질의 숫자, count = 제출 횟수 
        List<int> candidates = new List<int>();
        String submitResult = "";
        int guess = 0;
        int count = 0;

        //후보군 초기화
        InitCandidates(candidates);

        //n회 실행
        while(count < n)
        {
            //후보군 1개 시 정답 반환
            if(candidates.Count == 1)
            {
                //Console.WriteLine($"Result : {candidates[0]} / Count {count}");
                return candidates[0];
            }
            
            //최적 질의 숫자 선택 및 제출 과정
            guess = FindBestGuess(candidates);
            submitResult = submit(guess);
            count++;
            if(submitResult == "4S 0B")
            {
                //Console.WriteLine($"Result : {guess} / Count {count}");
                return guess;
            }
            //후보군 업데이트
            UpdateCandidates(candidates, guess, submitResult);
        }
        
        if(candidates.Count == 1)
        {
            //Console.WriteLine($"Result : {candidates[0]} / Count {count}");
            return candidates[0];
        }
        
        return 0;
    }
    
    //후보군 초기화 함수
    public void InitCandidates(List<int> candidates)
    {
        for(int i = 1000; i<9999; i++)
        {
            if(CheckDuplication(i))
                continue;
            
            candidates.Add(i);
        }
        
        //Console.WriteLine($"Make Candidates Count {candidates.Count}");
    }
    
    //후보군 업데이트 함수
    public void UpdateCandidates(List<int> candidates, int guess, string result)
    {
        for (int i = candidates.Count - 1; i >= 0; i--)
        {
            //후보가 정답일 때의 결과가 실제 제출 결과와 다르면 후보군에서 제외
            if (CheckResult(guess, candidates[i]) != result)
            {
                candidates.RemoveAt(i);
            }
        }

        //Console.WriteLine($"Update Candidates Count : {candidates.Count}");
    }
    
    //최적 질의 숫자 찾기 함수
    public int FindBestGuess(List<int> candidates)
    {
        //allNumber = 질의 후보 목록, groupCountDict = 결과 그룹별 후보 갯수
        //result = 검사 결과(S B), bestGuess = 최적 질의 숫자, worstCount = 최악의 경우 남는 후보 수
        List<int> allNumber = candidates.ToList();
        Dictionary<string, int> groupCountDict = new Dictionary<string, int>();
        string result = "";
        int bestGuess = candidates[0];
        int worstCount = int.MaxValue;
        
        // 각 후보를 질의 숫자로 가정하고 결과별 후보 개수 계산
        foreach(int guess in allNumber)
        {
            groupCountDict = new Dictionary<string, int>();
            
            foreach(int candidate in candidates)
            {
                
                result = CheckResult(guess, candidate);
                
                // 비교 결과별 후보 개수 누적
                if(!groupCountDict.ContainsKey(result))
                {
                    groupCountDict[result] = 0;
                }
                
                groupCountDict[result]++;
            }
            
            //최악의 경우 남는 후보 수가 가장 적은 숫자를 선택
            if(worstCount > groupCountDict.Values.Max())
            {
                worstCount = groupCountDict.Values.Max();
                bestGuess = guess;
            }
        }
        
        //Console.WriteLine($"Find Best Guess : {bestGuess}");
        return bestGuess;
    }
    
    //각 자리 숫자의 중복 및 0 포함 여부 검사 함수
    public bool CheckDuplication(int num)
    {
        // numList = num 각 자리별 숫자
        // digit = 자리별 숫자
        
        List<int> numList = new List<int>();
        int digit = 0;
        while(num > 0)
        {
            digit = num % 10;
            //이미 사용한 숫자이거나 0이면 제외
            if(numList.Contains(digit) || digit == 0)
                return true;
            
            numList.Add(digit);
            num = num / 10;
        }
        
        return false;
    }
    
    //두 숫자의 Strike/Ball 결과 계산 함수
    public string CheckResult(int num1, int num2)
    {
        // s = STRIKE 횟수, b = BALL 횟수
        // numList1 = num1 자리별 저장, numList2 = num2 자리별 저장
        int s = 0;
        int b = 0;
        
        List<int> numList1 = new List<int>(){num1 / 1000, num1 / 100 % 10, num1 / 10 % 10, num1 % 10};
        List<int> numList2 = new List<int>(){num2 / 1000, num2 / 100 % 10, num2 / 10 % 10, num2 % 10};
        
        //각 자리별 위치, 포함 검사
        for(int i = 0; i<numList1.Count; i++)
        {
            if(numList2.Contains(numList1[i]))
            {
                if(numList1[i] == numList2[i])
                {
                    s++;
                }
                else
                {
                    b++;
                }
            }
        }
        
        return $"{s}S {b}B";
    }
}