using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState
{
    protected PlayerStateMachine stateMachine;
    protected Player player;

    protected Rigidbody2D rb;

    protected float horizontalInput;
    protected float verticalInput;

    private string animBoolName;

    protected float stateTimer;
    protected bool triggerCalled;

    //constructor olusturdum, bu sayede animasyonlarimi girilen degerlere göre degiştirmek icin kullanicam.
    public PlayerState(Player _player,PlayerStateMachine _stateMachine, string _animBoolName)
       //PlayerState ile oyuncu, durum makinesi ve animasyon bilgilerine ulaşıcam ve girilen degerlerle bu animasyonlari anlik degistiricem.
    {
        this.player = _player;
        this.stateMachine = _stateMachine;
        this.animBoolName = _animBoolName;
    }

    //MonoBehaviour'dan kalitim almiyorum bu yuzden Update , Enter ve Exit'i kullanamiyorum.
    //Fakat Player MonoBehaviour'dan kalitim aldigi için bu Class'ta sadece yerleri belli olsun diye fonksiyon isimlerini aynı yazdim.
    public virtual void Enter() 
    {
        player.anim.SetBool(animBoolName,true);
        rb = player.rb;
        //Animasyon cagirilana kadar false olucak
        triggerCalled = false;
    }

    public virtual void Update() {

        stateTimer -= Time.deltaTime;

        horizontalInput = Input.GetAxisRaw("Horizontal");
        //Blendtreedeki dusme yada ziplamayi kullanmak icin
        verticalInput = Input.GetAxisRaw("Vertical");
        player.anim.SetFloat("Vertical", rb.velocity.y); 
    }
 
    //Animasyonlar bir kere aktif edildiginde, surekli aktif olmamasi icin animasyonu bitiren kod 
    public virtual void AnimationFinishTrigger()
    {
        triggerCalled = true;
    }
    public virtual void Exit() 
    {
        player.anim.SetBool(animBoolName, false);
    }
}

