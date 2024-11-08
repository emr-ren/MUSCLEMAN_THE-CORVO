using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerGroundState
//PlayerState'ten kalitim aldim
//Olsturdugum constructorlari PlayerIdleState'e (alt + enter ile) cagirdim.
//PlayerState'te bulunan girdi,cikti ve guncelleme fonksiyonlarini da cagirip kalitim alicam bunun için (alt + enter ile) Generate ovveride'i kullandım.
{
    public PlayerIdleState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }
    //PlayerState'in ozelliklerine ve metodlarina erismem için base kullandim.
    public override void Enter()
    {
        base.Enter();

        //idle'dan sonra haraktet etmemesi icin 
        player.Immobility();
        
    }

    public override void Update()
    {
        base.Update();

        if (horizontalInput == player.xScale && player.IsWallDetected())
            return;


        //X Input'u 0 a esit degil ve oyuncu musaitise state'i degistir
        if (horizontalInput != 0   &&  !player.isBusy)
        { stateMachine.ChangeState(player.moveState); }
    }
    public override void Exit()
    {
        base.Exit();
    }
}
