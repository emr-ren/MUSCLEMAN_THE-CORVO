using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DarkSideHotKeyController : MonoBehaviour
{
    private SpriteRenderer sr;

    private KeyCode myHotKey;
    private TextMeshProUGUI myText;

    private Transform myEnemy;
    private DarkSideSkillController darkSide;

    public void SetupHotKey(KeyCode _myNewHotKey, Transform _myEnemy, DarkSideSkillController _myDarkSide  )
    {
        sr = GetComponent<SpriteRenderer>();
        myText = GetComponentInChildren<TextMeshProUGUI>();

        myEnemy = _myEnemy;
        darkSide = _myDarkSide;

        myHotKey = _myNewHotKey;
        myText.text = _myNewHotKey.ToString();
    }

    private void Update()
    {
        if (Input.GetKeyDown(myHotKey))
        {
            darkSide.AddEnemyToList(myEnemy);

            myText.color = Color.clear;
            sr.color = Color.clear;
        }
    }
}
