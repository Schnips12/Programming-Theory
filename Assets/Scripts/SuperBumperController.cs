using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// INHERITANCE
public class SuperBumperController : BumperController
{

    // POLYMORPHISM override
    // basically this just changes the value of the intensity of the bump...
    // TODO Change the action (delayed bump with a coroutine, modifying the way the player can accelerate, etc)
    public override void ActionOnPlayer(PlayerController player, Vector3 direction)
    {
        player.AddForce(- direction, 15);
    }


}
