using UnityEngine;

// I would code this as an interface but apparently abstract classes are better
/*
 * These objects are placed around the world to augment the player's movement capabilities, where the player can choose to interact with them.
 */
public abstract class CandyCaneObject : MonoBehaviour
{
    public abstract void Interact();
    public float UseRadius = 10;
}
