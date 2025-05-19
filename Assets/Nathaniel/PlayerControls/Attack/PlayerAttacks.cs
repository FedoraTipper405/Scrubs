using System.Collections;
using UnityEngine;

public class PlayerAttacks : MonoBehaviour
{
    [SerializeField]  Animator animator;

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

    BaseAtkCollider[] leftColScript = new BaseAtkCollider[4];
    BaseAtkCollider[] rightColScript = new BaseAtkCollider[4];

    bool isAttackingRight = true;
    bool canInput = true;
    [SerializeField] int[] spartanKickArray = new int[3];
    [SerializeField] int[] HammerPunchArray = new int[3];
    [SerializeField] int[] RoundHouseArray = new int[3];
    [SerializeField] int[] SanjiArray = new int[3];
    [SerializeField] int[] JabArray = new int[3];
    [SerializeField] int[] ChargedPunchArray = new int[3];

    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        animator = GetComponent<Animator>();//xh add
    }
    void Start()
    {

        comboArray[0] = 10;
        comboArray[1] = 10;
        comboArray[2] = 10;
        for (int i = 0; i < leftColliderArray.Length; i++)
        {
            leftColScript[i] = leftColliderArray[i].GetComponent<BaseAtkCollider>();
        }
        for (int i = 0; i < rightColliderArray.Length; i++)
        {
            rightColScript[i] = rightColliderArray[i].GetComponent<BaseAtkCollider>();
        }
        for (int i = 0; i < leftColliderArray.Length; i++)
        {
            leftColliderArray[i].SetActive(false);
        }
        for (int i = 0; i < rightColliderArray.Length; i++)
        {
            rightColliderArray[i].SetActive(false);
        }
    }
    public void DirectionToHit(Vector2 input)
    {
        if(input.x > 0)
        {
            isAttackingRight = false;
        }
        else if(input.x < 0)
        {
            isAttackingRight = false;
        }
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
                BasicPunch();
            }
            else if(comboArray[0] == 1)
            {
                BasicKick();
            }
        }
        if (currentComboIndex == 2)
        {
            if (comboArray[0] == 1 && comboArray[1] == 1)
            {
                SecondKick();
            }
            if (comboArray[0] == 0 && comboArray[1] == 1)
            {
                SecondKick();
            }
            if (comboArray[0] == 0 && comboArray[1] == 0)
            {
                SecondPunch();
            }
            if (comboArray[0] == 1 && comboArray[1] == 0)
            {
                SecondPunch();
            }
        }
            if(currentComboIndex == 3)
            {
            if (comboArray[0] == spartanKickArray[0] && comboArray[1] == spartanKickArray[1] && comboArray[2] == spartanKickArray[2])
            {
                SpartanKick(0, 0);
            }
            //else if (comboArray[0] == HammerPunchArray[0] && comboArray[1] == HammerPunchArray[1] && comboArray[2] == HammerPunchArray[2])
            //{
            //HammerPunch(1,1);
            //}
            //else if (comboArray[0] == RoundHouseArray[0] && comboArray[1] == RoundHouseArray[1] && comboArray[2] == RoundHouseArray[2])
            //{
            //RoundHouse(2,2);
            //}
            //else if (comboArray[0] == SanjiArray[0] && comboArray[1] == SanjiArray[1] && comboArray[2] == SanjiArray[2])
            //{
            //SanjiTableTop(3,3);
            //}
            else if (comboArray[0] == JabArray[0] && comboArray[1] == JabArray[1] && comboArray[2] == JabArray[2])
            {
                StraightJab(0, 4);
            }
            //    else if( comboArray[0] == ChargedPunchArray[0] && comboArray[1] == ChargedPunchArray[1] && comboArray[2] == ChargedPunchArray[2])
            //    {
            //    ChargedPunch(0,5);
            //}
            else if (comboArray[2] == 0)
            {
                BasicPunch();
            }else if (comboArray[2] == 1)
            {
                BasicKick();
            }
            else
            {
                canInput = true;
            }

                ClearComboArray();
          
            }
        
    }
    public void BasicPunch()
    {
        StartCoroutine(BPunchSequence());
    }
    IEnumerator BPunchSequence()
    {
        //play basic punch animation
        animator.SetTrigger("isPunch");

        yield return new WaitForSeconds(.25f);
        if(isAttackingRight)
        {
            rightColliderArray[0].SetActive(true);
            rightColScript[0].PrepareForAttack(5, 0);
            yield return new WaitForSeconds(.15f);
            rightColliderArray[0].SetActive(false);
        }
        else
        {
            leftColliderArray[0].SetActive(true);
            leftColScript[0].PrepareForAttack(5, 0);
            yield return new WaitForSeconds(.15f);
            leftColliderArray[0].SetActive(false);
        }
        canInput = true;

    }
    public void BasicKick()
    {
        StartCoroutine(BKickSequence());
    }
    IEnumerator BKickSequence()
    {

        //player basic kick animation
        animator.SetTrigger("isKick");

        yield return new WaitForSeconds(.25f);
        if (isAttackingRight)
        {
            rightColliderArray[0].SetActive(true);
            rightColScript[0].PrepareForAttack(5, 0);
            yield return new WaitForSeconds(.15f);
            rightColliderArray[0].SetActive(false);
        }
        else
        {
            leftColliderArray[0].SetActive(true);
            leftColScript[0].PrepareForAttack(5, 0);
            yield return new WaitForSeconds(.15f);
            leftColliderArray[0].SetActive(false);
        }
        canInput = true;
    }
    public void SecondPunch()
    {
        StartCoroutine(SPunchSequence());
    }
    IEnumerator SPunchSequence()
    {
        //play special punch animation
        animator.SetTrigger("isPunch");//for now just have punch

        yield return new WaitForSeconds(.25f);
        if (isAttackingRight)
        {
            rightColliderArray[0].SetActive(true);
            rightColScript[0].PrepareForAttack(5, 0);
            yield return new WaitForSeconds(.15f);
            rightColliderArray[0].SetActive(false);
        }
        else
        {
            leftColliderArray[0].SetActive(true);
            leftColScript[0].PrepareForAttack(5, 0);
            yield return new WaitForSeconds(.15f);
            leftColliderArray[0].SetActive(false);
        }
        canInput = true;
    }
    public void SecondKick()
    {
        StartCoroutine(SKickSequence());
    }
    IEnumerator SKickSequence()
    {
        //play special kick animation
        animator.SetTrigger("isKick");

        yield return new WaitForSeconds(.25f);
        if (isAttackingRight)
        {
            rightColliderArray[0].SetActive(true);
            rightColScript[0].PrepareForAttack(5, 0);
            yield return new WaitForSeconds(.15f);
            rightColliderArray[0].SetActive(false);
        }
        else
        {
            leftColliderArray[0].SetActive(true);
            leftColScript[0].PrepareForAttack(5, 0);
            yield return new WaitForSeconds(.15f);
            leftColliderArray[0].SetActive(false);
        }
        canInput = true;
    }
    public void SpartanKick(int colliderIndex, int comboIndex)
    {
        Debug.Log("spart");
        StartCoroutine(SpartanSequence(colliderIndex, comboIndex));
    }
    IEnumerator SpartanSequence(int colliderIndex, int comboIndex)
    {

        //Play spartan Kick Animation
        animator.SetTrigger("isKick");

        yield return new WaitForSeconds(.3f);
        if (isAttackingRight)
        {
            rightColliderArray[colliderIndex].SetActive(true);
            rightColScript[colliderIndex].PrepareForAttack(soComboArray[comboIndex].damage, soComboArray[comboIndex].knockback);
            yield return new WaitForSeconds(.15f);
            rightColliderArray[colliderIndex].SetActive(false);
        }
        else
        {
            leftColliderArray[colliderIndex].SetActive(true);
            leftColScript[colliderIndex].PrepareForAttack(soComboArray[comboIndex].damage, soComboArray[comboIndex].knockback);
            yield return new WaitForSeconds(.15f);
            leftColliderArray[colliderIndex].SetActive(false);
        }
        canInput = true;
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
        StartCoroutine(JabSequence(colliderIndex, comboIndex)); 
    }
    IEnumerator JabSequence(int colliderIndex, int comboIndex)
    {
        //play jab combo finisher animation
        animator.SetTrigger("isPunch");
        yield return new WaitForSeconds(.3f);
        if (isAttackingRight)
        {
            rightColliderArray[colliderIndex].SetActive(true);
            rightColScript[colliderIndex].PrepareForAttack(soComboArray[comboIndex].damage, soComboArray[comboIndex].knockback);
            yield return new WaitForSeconds(.15f);
            rightColliderArray[colliderIndex].SetActive(false);
        }
        else
        {
            leftColliderArray[colliderIndex].SetActive(true);
            leftColScript[colliderIndex].PrepareForAttack(soComboArray[comboIndex].damage, soComboArray[comboIndex].knockback);
            yield return new WaitForSeconds(.15f);
            leftColliderArray[colliderIndex].SetActive(false);
        }
        canInput = true;
    }
    public void ChargedPunch(int colliderIndex, int comboIndex)
    {
        Debug.Log("supaaaa");
    }

    public void KickInput()
    {
        if(canInput)
        {
            timeWithoutInput = 0;
            comboArray[currentComboIndex] = 1;
            currentComboIndex++;
            CheckComboArray();
            canInput = false;
        }
       
    }
    public void PunchInput()
    {
        if (canInput)
        {
            timeWithoutInput = 0;
            comboArray[currentComboIndex] = 0;
            currentComboIndex++;
            CheckComboArray();
            canInput = false;
        }
    }
}
