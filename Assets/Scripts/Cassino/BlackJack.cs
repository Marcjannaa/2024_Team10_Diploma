using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;



public class BlackJack : MonoBehaviour
{
    private List<Card> _deck = new List<Card>();
    private GameObject _player;
    private bool _inGame;
    private int _currentBet;
    [SerializeField] private int _maxBet;
    private int _playerScore;
    private int _dealerScore;
    private Card _dealerSecretCard;
    [SerializeField] private GameObject _BJ_UI;
    private GameObject _betPanel;
    private GameObject _gamePanel;
    [SerializeField] private TMP_Text _palyerCoinText;
    [SerializeField] private TMP_Text _currentBetText;
    [SerializeField] private TMP_Text _playerScoreText;
    [SerializeField] private TMP_Text _dealerScoreText;
    
    
    private bool _inBJRound;
    private bool _initialAction;
    
    
    

    public void StartGame(GameObject player)
    {
        _deck = CreateDeck();
        Debug.Log("Deck created with " + _deck.Count + " cards.");
        PrintDeck();
        _player = player;
        _BJ_UI.SetActive(true);
        _betPanel = _BJ_UI.transform.Find("BetPanel").gameObject;
        _gamePanel = _BJ_UI.transform.Find("GamePanel").gameObject;
        
        
        _inGame = true;
        _inBJRound = false;
        _initialAction = true;
        
        _betPanel.SetActive(true);
        SetPlayerCoinsText();
        StartCoroutine(BlackJackLoop());

    }

    private IEnumerator BlackJackLoop()
    {
        while (_inGame)
        {
            if (_initialAction && _inBJRound)
            {
                _initialAction = false;
                _playerScore += PickRandomCard().GetValue();
                _dealerScore += PickRandomCard().GetValue();
                _playerScore += PickRandomCard().GetValue();
                _dealerSecretCard = PickRandomCard();
                
                SetPlayerScoreText();
                SetDealerScoreText();
            }

            if (_playerScore > 21)
            {
                GameOver();
            }

            if (_playerScore == 21)
            {
                GameWon();
            }

            if (_dealerScore == 21)
            {
                GameOver();
            }
            
            if (_dealerScore > 21)
            {
                GameWon();
            }
            yield return null;
        } 
        
        _BJ_UI.SetActive(false);
        _player.GetComponent<PlayerController>().LeaveBJ();
    }

    private void GameOver()
    {
        Debug.Log("game lost");
        //CloseGame();
    }

    private void GameWon()
    {
        Debug.Log("game won");
        //CloseGame();
    }

    Card PickRandomCard()
    {
        if (_deck.Count == 0)
        {
            Debug.LogWarning("Deck is empty!");
            return null;
        }

        int index = Random.Range(0, _deck.Count);
        Card pickedCard = _deck[index];
        _deck.RemoveAt(index);
        return pickedCard;
    }
    

    List<Card> CreateDeck()
    {
        List<Card> deck = new List<Card>();
        string[] colors = { "Hearts", "Diamonds", "Clubs", "Spades" };
        string[] names = { "2", "3", "4", "5", "6", "7", "8", "9", "10", "Jack", "Queen", "King", "Ace" };

        foreach (string color in colors)
        {
            foreach (string name in names)
            {
                int value;

                if (int.TryParse(name, out value))
                {
                    
                }
                else if (name == "Ace")
                {
                    value = 11;
                }
                else
                {
                    value = 10;
                }

                Card newCard = new Card(color, name, value);
                deck.Add(newCard);
            }
        }

        return deck;
    }
    
    void PrintDeck()
    {
        foreach (Card card in _deck)
        {
            Debug.Log($"{card.GetName()} of {card.GetColor()} (value: {card.GetValue()})");
        }
    }

    public void IncreaseBet()
    {
        if (_currentBet < _maxBet && _currentBet < _player.GetComponent<Player_Stats>().Coins.Value)
        {
            _currentBet += 1;
            SetBetText(_currentBet.ToString());
        }
    }

    public void DecreaseBet()
    {
        if (_currentBet > 0)
        {
            _currentBet -= 1;
            SetBetText(_currentBet.ToString());
        }
    }
    
    public void SetPlayerScoreText()
    {
        _playerScoreText.text = "Player Score: " + _playerScore;
    }
    
    public void SetDealerScoreText()
    {
        _dealerScoreText.text = "Dealer Score: " + _dealerScore;
    }
    
    public void SetBetText(string text)
    {
        _currentBetText.text = text;
    }
    
    public void SetPlayerCoinsText()
    {
        _palyerCoinText.text = "Coins " + _player.GetComponent<Player_Stats>().Coins.Value;
    }

    public void PlaceBet()
    {
        _player.GetComponent<Player_Stats>().Coins.Modify(-_currentBet);
        _betPanel.SetActive(false);
        _gamePanel.SetActive(true);
        _inBJRound = true;
    }

    public void Hit()
    {
        _playerScore += PickRandomCard().GetValue();
        SetPlayerScoreText();
    }

    public void Stand()
    {
        _dealerScore += _dealerSecretCard.GetValue();
        SetDealerScoreText();
        while (_dealerScore < 17)
        {
            _dealerScore += PickRandomCard().GetValue();
            SetDealerScoreText();
        }
    }

    public void CloseGame()
    {
        _currentBet = 0;
        _BJ_UI.SetActive(false);
        _player.GetComponent<PlayerController>().LeaveBJ();
    }
}
