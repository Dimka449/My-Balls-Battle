using System.Collections;
using UnityEngine;

public class ArenaShrinker : MonoBehaviour
{
    [SerializeField] private float secondsToReduce = 0.1f;
    [SerializeField] private float reductionStep = 0.04f;
    [SerializeField] private float targetRadius = 10f;

    private void Start()
    {
        StartCoroutine(IReduce());
    }

    private IEnumerator IReduce()
    {
        while (targetRadius < transform.localScale.x)
        {
            Vector3 localScale = transform.localScale;
            transform.localScale = new Vector3(localScale.x -= reductionStep, localScale.y, localScale.z -= reductionStep);
            yield return new WaitForSeconds(secondsToReduce);
        }
    }
}
