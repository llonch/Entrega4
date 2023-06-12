using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class CoinObtainer : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The audio source with the coin sound")]
    private AudioSource _coinAudio;

    [SerializeField]
    private PlayerInput _playerInput;

    [SerializeField]
    [Tooltip("How many points does a not related to the player coin gives?")]
    private int _pointsPerStandardCoin = 1;

    [SerializeField]
    [Tooltip("How many points does a related to the player coin gives?")]
    private int _pointsPerRelatedCoin = 3;

    public Action<int> OnCoinObtained;
    private int _coins = 0;

    public int Coins => _coins;

    private void Start()
    {
        _playerInput = gameObject.GetComponent<PlayerInput>();
    }

    // When the player collides with a trigger
    private void OnTriggerEnter(Collider other)
    {
        // If the trigger is of type coin
        if (other.gameObject.CompareTag("Coin"))
        {
            CoinBehaviour cb = other.gameObject.GetComponent<CoinBehaviour>();
            int points = _pointsPerStandardCoin;
            if (cb.RelatedPlayerIndex == _playerInput.playerIndex)
            {
                points = _pointsPerRelatedCoin;
            }
            // Add a coin and call everyone who listens to the coin being obtained
            _coins += points;
            OnCoinObtained?.Invoke(_coins);
            // Then return the coin to the pool
            CoinBehaviour coinBehaviour = other.GetComponent<CoinBehaviour>();
            coinBehaviour.CoinGot();
            if (_coinAudio != null) _coinAudio.Play();
        }
    }
}
