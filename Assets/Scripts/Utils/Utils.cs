using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utils
{

    public static List<int> GenerateRandomNumbers(int count, int minValue, int maxValue)
    {
        List<int> possibleNumbers = new List<int>();
        List<int> chosenNumbers = new List<int>();

        for (int index = minValue; index < maxValue; index++)
            possibleNumbers.Add(index);

        while (chosenNumbers.Count < count)
        {
            int position = UnityEngine.Random.Range(0, possibleNumbers.Count);
            chosenNumbers.Add(possibleNumbers[position]);
            possibleNumbers.RemoveAt(position);
        }
        return chosenNumbers;
    }


    public static IEnumerator CheckAnimationCompleted(Animator anim, string CurrentAnim, Action Oncomplete)
    {
        bool flag = true;
        while (flag)
        {
            AnimatorStateInfo animInfo = anim.GetCurrentAnimatorStateInfo(0);
            if (animInfo.IsName(CurrentAnim) && animInfo.normalizedTime > 1.0f)
            {
                flag = false;
            }

            yield return null;
        }
           
        if (Oncomplete != null)
            Oncomplete();
    }
}
