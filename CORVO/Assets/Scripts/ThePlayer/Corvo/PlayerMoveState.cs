using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : PlayerGroundState
{
    public PlayerMoveState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }
    public override void Update()
    {
        base.Update();

        //Belirledigimiz x hizin = Player'in hareket hızıyla carpimi  ,  karakterin Rigidbody2D'sinin yer cekimi (free fall)
        player.SetVelocity(horizontalInput*player.moveSpeed, rb.velocity.y);
                                                            

        /*Duvara kosmayi engelleme eski kod
         * Yuruken duvara geldiginde kosmayacak idlea gecicek
        if (horizontalInput == player.xScale && player.IsWallDetected())
            player.stateMachine.ChangeState(player.idleState);
        */

        //eger X Input 0'a esit ise idlestate'i dondur      //Yuruken duvara geldiginde kosmayacak, idlea gecicek
        if (horizontalInput == 0 || player.IsWallDetected())
        { stateMachine.ChangeState(player.idleState); }

    }
    public override void Exit()
    {
        base.Exit();
    }

}
