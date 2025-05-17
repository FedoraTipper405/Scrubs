using System;
using System.Collections;
using UnityEngine;

public class PlayerAttacks : MonoBehaviour
{
    [SerializeField]
    int[] comboArray = new int[3];

    [SerializeField] float timeToClearCombo;
    float timeWithoutInput;
    [SerializeField] int currentComboIndex = 0;
    [SerializeField] SOCombo[] soComboArray;

    [SerializeField]
    GameObject[] leftColliderArray;
    [SerializeField]
    GameObject[] rightColliderArray;

    bool isAttackingRight = true;

    [SerializeField] int[] spartanKickArray = new int[3];
    [SerializeField] int[] HammerPunchArray = new int[3];
    [SerializeField] int[] RoundHouseArray = new int[3];
    [SerializeField] int[] SanjiArray = new int[3];
    [SerializeField] int[] JabArray = new int[3];
    [SerializeField] int[] ChargedPunchArray = new int[3];
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        comboArray[0] = 10;
        comboArray[1] = 10;
        comboArray[2] = 10;
    }
    void ClearComboArray()
    {
        comboArray[0] = 10;
        comboArray[1] = 10;
        comboArray[2] = 10;
        currentComboIndex = 0;
    }
    // Update is called once per frame
    void Update()
    {
        if(timeWithoutInput < timeToClearCombo)
        {
            timeWithoutInput += Time.deltaTime;
        }
        else
        {
            ClearComboArray();
        }
    }
    void CheckComboArray()
    {
        if (currentComboIndex == 1)
        {
            if (comboArray[0] == 0)
            {
                //basic punch
            }
            else if(comboArray[0] == 1)
            {
                //basic kick
            }
        }
        if (currentComboIndex == 2)
        {
            if (comboArray[0] == 0 && comboArray[1] == 0)
            {
                //kick 1
            }
            if (comboArray[1] == 0 && comboArray[1] == 0)
            {
                //kick 2
            }
            if (comboArray[0] == 0 && comboArray[0] == 0)
            {
                //upper cut
            }
            if (comboArray[1] == 0 && comboArray[0] == 0)
            {
                //hook
            }
        }
            if(currentComboIndex == 3)
            {
            if (comboArray[0] == spartanKickArray[0] && comboArray[1] == spartanKickArray[1] && comboArray[2] == spartanKickArray[2] )
                {
                SpartanKick(0,0);
                }
                else if (comboArray[0] == HammerPunchArray[0] && comboArray[1] == HammerPunchArray[1] && comboArray[2] == HammerPunchArray[2])
                {
                HammerPunch(1,1);
                }
                else if (comboArray[0] == RoundHouseArray[0] && comboArray[1] == RoundHouseArray[1] && comboArray[2] == RoundHouseArray[2])
                {
                RoundHouse(2,2);
                }
                else if (comboArray[0] == SanjiArray[0] && comboArray[1] == SanjiArray[1] && comboArray[2] == SanjiArray[2])
                {
                SanjiTableTop(3,3);
                }
                else if (comboArray[0] == JabArray[0] && comboArray[1] == JabArray[1] && comboArray[2] == JabArray[2])
                {
                StraightJab(0,4);
                }
                else if( comboArray[0] == ChargedPunchArray[0] && comboArray[1] == ChargedPunchArray[1] && comboArray[2] == ChargedPunchArray[2])
                {
                ChargedPunch(0,5);
                }

                ClearComboArray();
          
            }
        
    }
    public void SpartanKick(int colliderIndex, int comboIndex)
    {
        Debug.Log("spart");
    }
    IEnumerator SpartanSequence(int colliderIndex, int comboIndex)
    {
        yield return new WaitForSeconds(.3f);
    }
    public void HammerPunch(int colliderIndex, int comboIndex)
    {
        Debug.Log("ham");
    }
    public void RoundHouse(int colliderIndex, int comboIndex)
    {
        Debug.Log("rund");
    }
    public void SanjiTableTop(int colliderIndex, int comboIndex)
    {
        Debug.Log("fourth strongest");
    }
    public void StraightJab(int colliderIndex, int comboIndex)
    {
        Debug.Log("jab");
    }
    public void ChargedPunch(int colliderIndex, int comboIndex)
    {
        Debug.Log("supaaaa");
    }

    public void KickInput()
    {
        timeWithoutInput = 0;
        comboArray[currentComboIndex] = 1;
        currentComboIndex++;
        CheckComboArray();
    }
    public void PunchInput()
    {
        timeWithoutInput = 0;
        comboArray[currentComboIndex] = 0;
        currentComboIndex++;
        CheckComboArray();
    }
}
