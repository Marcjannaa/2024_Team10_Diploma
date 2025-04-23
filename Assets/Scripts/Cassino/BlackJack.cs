using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class BlackJack : MonoBehaviour
{
    private List<Card> _deck = new List<Card>();
    private GameObject _player;
    private bool _inGame;
    private int _currentBet;
    [SerializeField] private int _maxBet;
    private int _playerScore;
    private int _dealerScore;
    private bool _standFinished = false;
    private Card _dealerSecretCard;
    [SerializeField] private GameObject _BJ_UI;
    private GameObject _betPanel;
    private GameObject _gamePanel;
    [SerializeField] private TMP_Text _playerCoinText;
    [SerializeField] private TMP_Text _currentBetText;
    [SerializeField] private TMP_Text _playerScoreText;
    [SerializeField] private TMP_Text _dealerScoreText;
    [SerializeField] private GameObject _betPanelFocus;
    [SerializeField] private GameObject _gamePanelFocus;

    private bool _inBJRound;
    private bool _initialAction;
    private bool _gameOver;

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
        EventSystem.current.SetSelectedGameObject(_betPanelFocus);
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

            if (_playerScore == 21)
            {
                GameWon(true);
                yield return new WaitForSeconds(1);
                CloseGame();
                break;
            }

            if (_playerScore > 21)
            {
                GameOver();
                yield return new WaitForSeconds(1);
                CloseGame();
                break;
            }

            if (_dealerScore == 21)
            {
                GameOver();
                yield return new WaitForSeconds(1);
                CloseGame();
                break;
            }

            if (_dealerScore > 21)
            {
                GameWon(false);
                yield return new WaitForSeconds(1);
                CloseGame();
                break;
            }

            if (_standFinished)
            {
                if (_playerScore > _dealerScore)
                {
                    GameWon(false);
                    yield return new WaitForSeconds(1);
                    CloseGame();
                    break;
                }
                GameOver();
                yield return new WaitForSeconds(1);
                CloseGame();
                break;
            }

            yield return null;
        }
    }

    private void GameOver()
    {
        _gameOver = true;
        print("ps: " + _playerScore + " ds: " + _dealerScore);
        Debug.Log("game lost");
    }

    private void GameWon(bool blackjack)
    {
        _gameOver = true;
        print("ps: " + _playerScore + " ds: " + _dealerScore);
        if (blackjack)
        {
            int val = (int)(_currentBet * 2.5);
            _player.GetComponent<Player_Stats>().Coins.Modify(val);
        }
        else
        {
            _player.GetComponent<Player_Stats>().Coins.Modify(_currentBet * 2);
        }
        Debug.Log("game won");
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
                    // 2-10
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
        _playerCoinText.text = "Coins " + _player.GetComponent<Player_Stats>().Coins.Value;
    }

    public void PlaceBet()
    {
        if (_currentBet > 0)
        {
            _player.GetComponent<Player_Stats>().Coins.Modify(-_currentBet);
            _betPanel.SetActive(false);
            _gamePanel.SetActive(true);
            EventSystem.current.SetSelectedGameObject(_gamePanelFocus);
            _inBJRound = true;
        }
    }

    public void Hit()
    {
        if (_gameOver || !_inBJRound) return;
    
        _playerScore += PickRandomCard().GetValue();
        SetPlayerScoreText();
    }


    public void Stand()
    {
        if (_gameOver || !_inBJRound) return;

        _dealerScore += _dealerSecretCard.GetValue();
        SetDealerScoreText();
        while (_dealerScore < 17)
        {
            _dealerScore += PickRandomCard().GetValue();
            SetDealerScoreText();
        }
        _standFinished = true;
    }


    public void CloseGame()
    {
        ResetGameState();
        _inGame = false;
        _BJ_UI.SetActive(false);
        _player.GetComponent<PlayerController>().LeaveBJ();
        print("coins: " + _player.GetComponent<Player_Stats>().Coins.Value);
    }
    
    
    private void ResetGameState()
    {
        _deck.Clear();
        _playerScore = 0;
        _dealerScore = 0;
        _currentBet = 0;
        _inBJRound = false;
        _initialAction = true;
        _standFinished = false;
        _dealerSecretCard = null;
        _gameOver = false;

        if (_betPanel != null) _betPanel.SetActive(false);
        if (_gamePanel != null) _gamePanel.SetActive(false);
    
        SetPlayerScoreText();
        SetDealerScoreText();
        SetBetText("0");
    }
    
}
