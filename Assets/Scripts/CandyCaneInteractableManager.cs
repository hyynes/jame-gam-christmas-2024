using UnityEngine;
using Unity.VisualScripting;
using UnityEditor;

public class CandyCaneBehaviourManager : MonoBehaviour
{
    public static CandyCaneBehaviourManager instance { get; private set; }
    
    private CandyCaneObject[] allInteractables;

    [SerializeField] private float UseRadius = 10;
    
    // Start is called once before he first execution of Update after the MonoBehaviour is created
    void Start()
    {
        allInteractables = FindObjectsByType<CandyCaneObject>(FindObjectsSortMode.None);
    }

    // Update is called once per frame
    void Update()
    {
        // assume first object in index is nearest
        CandyCaneObject nearest = allInteractables[0];
        float distanceToNearest = Vector2.Distance(gameObject.transform.position, nearest.transform.position);

        // iterate through all objects
        for (int i = 1; i < allInteractables.Length; i++)
        {
            float distanceToCurrent =
                Vector2.Distance(gameObject.transform.position, allInteractables[i].transform.position);

            // if the current distance is smaller, then replace the current selected object
            if (distanceToCurrent < distanceToNearest)
            {
                nearest = allInteractables[i];
                distanceToNearest = distanceToCurrent;
            }
        }
    }
}
