using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject prefab;
    public float interval;
    public Transform moveTarget;
    public Transform container;
    
    public string friendLayer;
    public string enemyLayer;    
    

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Spawn());
    }

    // Update is called once per frame
    void Update()
    {
    }

    private IEnumerator Spawn()
    {
        while (true)
        {
            yield return new WaitForSeconds(interval);

            GameObject instance = Instantiate(prefab, container);
            instance.layer = LayerMask.NameToLayer(friendLayer);
            if (instance.TryGetComponent(out UnitBehaviour unitBehaviour))
            {
                unitBehaviour.SetMoveTarget(moveTarget);
                unitBehaviour.checkLayerMask = LayerMask.GetMask(enemyLayer);
            }
            
        }
    }
}