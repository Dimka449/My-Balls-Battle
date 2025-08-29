using System.Collections;
using UnityEngine;
using Mirror;

public class ArenaShrinker : NetworkBehaviour
{
    [SerializeField] private Vector3 startScale = new Vector3(40f, 0.3f, 40f);
    [SerializeField] private float secondsToReduce = 0.1f;
    [SerializeField] private float reductionStep = 0.04f;
    [SerializeField] private float targetRadius = 10f;

    private Coroutine _currentCoroutine;

    [Server]
    private void Awake()
    {
        transform.localScale = startScale;
    }

    [Server]
    private void Start()
    {
        CustomNetworkManager.SubOnMaxClientsWereConnected(StartIReduce);
    }

    [Server]
    private void StartIReduce()
    {
        if (_currentCoroutine != null) return;
        StartCoroutine(IReduce());
    }

    [Server]
    private IEnumerator IReduce()
    {
        while (targetRadius < transform.localScale.x)
        {
            Vector3 localScale = transform.localScale;
            transform.localScale = new Vector3(localScale.x -= reductionStep, localScale.y, localScale.z -= reductionStep);
            yield return new WaitForSeconds(secondsToReduce);
        }
    }

    private void OnDestroy()
    {
        if (NetworkServer.active)
        {
            CustomNetworkManager.UnsubOnMaxClientsWereConnected(StartIReduce);
        }
    }
}
