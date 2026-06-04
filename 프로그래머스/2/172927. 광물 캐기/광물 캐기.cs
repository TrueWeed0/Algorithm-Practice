using System;
using System.Collections.Generic;

///<summary>
/// 1. 5개씩 광물을 잘라 그룹화 함
/// 2. 각 그룹별 광물 갯수, 최대 피로도 계산
/// 3. 피로도 순으로 내림차순 정렬
/// 4. 좋은 곡괭이로 정렬된 그룹에서 피로도 계산
///</summary>

//그룹 정보
public class GroupInfo
{
    // diaCount = 다이아 갯수
    // ironCount = 철 갯수
    // stoneCount = 돌 갯수
    // score = 최대 피로도
    public int diaCount;
    public int ironCount;
    public int stoneCount;
    public int score;
    
    public GroupInfo(){ }
}

public class Solution {
    public int solution(int[] picks, string[] minerals) {
        // answer = 피로도
        // pick = 현재 선택중인 곡괭이 idx
        // groupInfo 광물 그룹 별 정보

        int answer = 0;
        int pick = 0;
        List<GroupInfo> groupInfo = MakeGroup(picks, minerals);
        
        for(int i = 0; i<groupInfo.Count; i++)
        {
            //현재 들고 있는 곡괭이의 갯수가 0이면 곡괭이 교체
            while (pick < picks.Length && picks[pick] <= 0)
            {
                pick++;
            }
            
            //더 이상 들 곡괭이가 없으면 종료
            if (pick >= picks.Length)
                break;
            
            //해당 그룹에 광물별 피로도 계산
            answer += CalcFatigue(pick, "diamond") * groupInfo[i].diaCount;
            answer += CalcFatigue(pick, "iron") * groupInfo[i].ironCount;
            answer += CalcFatigue(pick, "stone") * groupInfo[i].stoneCount;

            //곡괭이 갯수 차감
            picks[pick]--;
        }

        return answer;
    }
    
    //광물 순서 그룹별 점수 계산 함수
    public List<GroupInfo> MakeGroup(int[] picks, string[] minerals)
    {
        List<GroupInfo> result = new List<GroupInfo>();
        GroupInfo nowInfo = new GroupInfo();
        
        //곡괭이 갯수 만큼 캘 수 있는 광물 수 계산
        int pickCount = picks[0] + picks[1] + picks[2];
        int maxMineralCount = Math.Min(minerals.Length, pickCount * 5);
        int count = 0;
        
        for(int i = 0; i<maxMineralCount; i++)
        {
            string mineral = minerals[i];
            if(count == 5)
            {
                //5개 씩 그룹화 하여 그룹 정보 생성
                result.Add(nowInfo);
                nowInfo = new GroupInfo();
                count = 0;
            }
            
            //광물 별 최대 피로도 계산
            switch(mineral)
            {
                case "diamond":
                    nowInfo.diaCount++;
                    nowInfo.score += 25;
                    break;
                case "iron":
                    nowInfo.ironCount++;
                    nowInfo.score += 5;
                    break;
                case "stone":
                    nowInfo.stoneCount++;
                    nowInfo.score += 1;
                    break;
            }
            
            count++;
        }
        
        //count가 0보다 크면 그룹 정보 있음
        if(count > 0)
             result.Add(nowInfo);
        
        //점수 별 내림차순 정렬
        result.Sort((a,b)=> b.score.CompareTo(a.score));

        return result;
    }
    
    //곡괭이, 광물 별 피로도 계산
    public int CalcFatigue(int pick, string mineral)
    {
        if(pick == 0)
            return 1;
        
        switch(mineral)
        {
            case "diamond":
                if(pick == 1)
                    return 5;
                else
                    return 25;
                break;
            case "iron":
                if(pick == 1)
                    return 1;
                else
                    return 5;
                break;
            case "stone":
                if(pick == 1)
                    return 1;
                else
                    return 1;
                break;
        }
        
        return 0;
    }
}