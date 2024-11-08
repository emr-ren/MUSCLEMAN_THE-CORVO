using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DarkSideSkillController : MonoBehaviour
{
    [SerializeField] private GameObject hotKeyPrefab;
    [SerializeField] private List<KeyCode> KeyCodeList;

    private float maxSize;
    private float growSpeed;
    private float closeSpeed;
    private float darkSideAttackTimer;
    
    private bool canGrow = true;
    private bool canClose;
    private float theDarkSideTimer;
    private bool canCreateHotKeys = true;
    private bool canDarkSideAttack;
    private int darkSideNumerOfAttacks;
    private bool playerCanInvisible = true;

    private float darkSideAttackCooldown;
    
    
    private List<Transform> targets = new List<Transform>();
    private List<GameObject> createdHotKey = new List<GameObject>();

    public bool playerCanExitState { get; private set; }

    public void SetupDarkSide(float _maxSize, float _growSpeed, float _closeSpped, int _darkSideNumerOfAttacks, float _darkSideAttackCooldown, float _theDarkSideTimer)
    {
        maxSize = _maxSize;
        growSpeed = _growSpeed;
        closeSpeed = _closeSpped;
        darkSideNumerOfAttacks = _darkSideNumerOfAttacks;
        darkSideAttackCooldown = _darkSideAttackCooldown;
        theDarkSideTimer = _theDarkSideTimer;
    }

    private void Update()
    {
        darkSideAttackTimer -= Time.deltaTime;
        theDarkSideTimer -= Time.deltaTime;

        if (theDarkSideTimer < 0)
        {
            theDarkSideTimer = Mathf.Infinity;

            if (targets.Count > 0)
                DarkSideAttack();
            else
                toExitDarkSide();
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            DarkSideAttack();
        }

        DarkSideAttackMethod();

        DarkSideField();
    }

    private void DarkSideField()
    {
        if (canGrow && !canClose)
        {
            transform.localScale = Vector2.Lerp(transform.localScale, new Vector2(maxSize, maxSize), growSpeed * Time.deltaTime);
        }

        if (canClose)
        {
            transform.localScale = Vector2.Lerp(transform.localScale, new Vector2(-1, -1), closeSpeed * Time.deltaTime);

            if (transform.localScale.x < 0)
                Destroy(gameObject);

        }
    }

    private void DarkSideAttack()
    {
        if (targets.Count <= 0)
            return; 

        

        //Sadece secilen dusmanlara yada dusmana saldiri icin DestroyHotKeys();
        DestroyHotKeys();
        canDarkSideAttack = true;
        canCreateHotKeys = false;

        if (playerCanInvisible)
        {
            playerCanInvisible = false;
        
            // Dark Side kullanırken kaybolması icin
            PlayerManager.instance.player.fx.MakeTransprent(true);
        }

    }

    private void DarkSideAttackMethod()
    {
        if (darkSideAttackTimer < 0 && canDarkSideAttack && darkSideNumerOfAttacks > 0 )
        {
            darkSideAttackTimer = darkSideAttackCooldown;

            int randomIndex = Random.Range(0, targets.Count);
            float xOffset;

            if (Random.Range(0, 100) > 50)
            {
                xOffset = 1;
            }
            else
            {
                xOffset = -1;
            }

            PlayerSkillManager.instance.cloneCorvo.CreateCloneCorvo(targets[randomIndex], new Vector3(xOffset, -0.7f, 0));
            darkSideNumerOfAttacks--;

            if (darkSideNumerOfAttacks <= 0)
            {
                //toExitDarkSide fonksiyonunu biraz bekletmek icin 
                Invoke("toExitDarkSide",1f);
            }

        }
    }

    private void toExitDarkSide()
    {
        DestroyHotKeys();
        playerCanExitState = true;
        canClose = true;
        canDarkSideAttack = false;
    }

    //Sadece secilen dusmanlara yada dusmana saldiri icin
    private void DestroyHotKeys()
    {
        if (createdHotKey.Count <= 0)
        {
            return;
        }
        for (int i = 0; i < createdHotKey.Count; i++)
        {
            Destroy(createdHotKey[i]);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Enemy>() != null)
        {
            collision.GetComponent<Enemy>().FreezeTime(true);

            CreateHotKey(collision);

        }
    }

    private void OnTriggerExit2D(Collider2D collision) => collision.GetComponent<Enemy>()?.FreezeTime(false);
    /*eski exit2d 
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<Enemy>() != null)
            collision.GetComponent<Enemy>().FreezeTime(false);
    }
    */

    private void CreateHotKey(Collider2D collision)
    {
        if (KeyCodeList.Count <= 0)
        {
            Debug.LogWarning("LISTEDE YETERLI TUS YOK");
            return;
        }

        if (!canCreateHotKeys)
        {
            return;
        }

        GameObject newHotKey = Instantiate(hotKeyPrefab, collision.transform.position + new Vector3(0, 2), Quaternion.identity);
        createdHotKey.Add(newHotKey);


        KeyCode choosenKey = KeyCodeList[Random.Range(0, KeyCodeList.Count)];
        KeyCodeList.Remove(choosenKey);

        DarkSideHotKeyController darkSideHotKeyScript = newHotKey.GetComponent<DarkSideHotKeyController>();

        darkSideHotKeyScript.SetupHotKey(choosenKey, collision.transform, this);
    }

    public void AddEnemyToList(Transform _enemyTransform) => targets.Add(_enemyTransform);
}
 