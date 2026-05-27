using System;
using System.Linq;
using System.Collections.Generic;

public class Solution 
{
    List<int> allNumber = new List<int>();
    
    public int solution(int n, Func<int, string> submit)
    {
        List<int> candidates = new List<int>();
        String submitResult = "";
        int guess = 1234;
        int count = 0;
        bool resultCheck = false;
        
        InitAllNumber();
        candidates = allNumber.ToList();

        while(count < n)
        {
            if(candidates.Count == 1)
            {
                Console.WriteLine($"Result : {candidates[0]} / Count {count}");
                return candidates[0];
            }
            
            guess = FindBestGuess(candidates);
            submitResult = submit(guess);
            count++;
            if(submitResult == "4S 0B")
            {
                Console.WriteLine($"Result : {guess} / Count {count}");
                return guess;
            }
            UpdateCandidates(candidates, guess, submitResult);
        }
        
        if(candidates.Count == 1)
        {
            Console.WriteLine($"Result : {candidates[0]} / Count {count}");
            return candidates[0];
        }
        
        return 0;
    }
    
    public void InitAllNumber()
    {
        for(int i = 1000; i<9999; i++)
        {
            if(CheckDuplication(i))
                continue;
            
            allNumber.Add(i);
        }
        
        //Console.WriteLine($"Make Candidates Count {candidates.Count}");
    }
    
    public void UpdateCandidates(List<int> candidates, int guess, string result)
    {
        for (int i = candidates.Count - 1; i >= 0; i--)
        {
            if (CheckResult(guess, candidates[i]) != result)
            {
                candidates.RemoveAt(i);
            }
        }

        //Console.WriteLine($"Update Candidates Count : {candidates.Count}");
    }
    
    public int FindBestGuess(List<int> candidates)
    {
        Dictionary<string, int> groupCountDict = new Dictionary<string, int>();
        string result = "";
        int bestGuess = candidates[0];
        int worstCount = int.MaxValue;
        
        foreach(int guess in allNumber)
        {
            groupCountDict = new Dictionary<string, int>();
            
            foreach(int candidate in candidates)
            {
                result = CheckResult(guess, candidate);
                
                if(!groupCountDict.ContainsKey(result))
                {
                    groupCountDict[result] = 0;
                }
                
                groupCountDict[result]++;
            }
            
            if(worstCount > groupCountDict.Values.Max())
            {
                worstCount = groupCountDict.Values.Max();
                bestGuess = guess;
            }
        }
        
        Console.WriteLine($"Find Best Guess : {bestGuess}");
        return bestGuess;
    }
    
    public bool CheckDuplication(int num)
    {
        List<int> numList = new List<int>();
        int digit = 0;
        while(num > 0)
        {
            digit = num % 10;
            if(numList.Contains(digit) || digit == 0)
                return true;
            
            numList.Add(digit);
            num = num / 10;
        }
        
        return false;
    }
    
    public string CheckResult(int num1, int num2)
    {
        int s = 0;
        int b = 0;
        
        List<int> numList1 = new List<int>(){num1 / 1000, num1 / 100 % 10, num1 / 10 % 10, num1 % 10};
        List<int> numList2 = new List<int>(){num2 / 1000, num2 / 100 % 10, num2 / 10 % 10, num2 % 10};
        
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