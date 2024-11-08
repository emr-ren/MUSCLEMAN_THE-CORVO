using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    //Tum kodlara erismem icin
    public static PlayerManager instance;
    public Player player;

    private void Awake()
    {   //Sadece bir tane manager olması icin yok ettim skillde engellemesin
        if (instance != null)
            Destroy(instance.gameObject);
        instance = this;
    }

}
