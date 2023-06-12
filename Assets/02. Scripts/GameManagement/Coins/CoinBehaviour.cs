using System;
using UnityEngine;

public class CoinBehaviour : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Index of the player that gets more points from this coin")]
    private int _relatedPlayerIndex;

    public Action<CoinBehaviour> OnCoinGot;

    Transform _spawnReference;

    public Transform SpawnReference => _spawnReference;
    public int RelatedPlayerIndex => _relatedPlayerIndex;

    public void SetSpawnReference(Transform reference)
    {
        _spawnReference = reference;
    }

    public void CoinGot()
    {
        OnCoinGot?.Invoke(this);
    }
}
