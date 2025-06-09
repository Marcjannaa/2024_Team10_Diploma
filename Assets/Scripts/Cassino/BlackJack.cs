using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

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
    [SerializeField] private GameObject _betPanel;
    [SerializeField] private GameObject _gamePanel;
    [SerializeField] private GameObject _winPanel;
    [SerializeField] private GameObject _losePanel;

    [SerializeField] private TMP_Text _playerCoinText;
    [SerializeField] private TMP_Text _currentBetText;
    [SerializeField] private TMP_Text _playerScoreText;
    [SerializeField] private TMP_Text _dealerScoreText;

    [SerializeField] private GameObject _betPanelFocus;
    [SerializeField] private GameObject _gamePanelFocus;

    [SerializeField] private GameObject _playerCardListContainer;
    [SerializeField] private GameObject _dealerCardListContainer;

    [SerializeField] private GameObject cardPrefab;
    [SerializeField] private List<Sprite> cardSprites;

    private Dictionary<string, Sprite> _spriteLookup;

    private bool _inBJRound;
    private bool _initialAction;
    private bool _gameOver;

    public void StartGame(GameObject player)
    {
        _deck = CreateDeck();
        Debug.Log("Deck created with " + _deck.Count + " cards.");
        _player = player;
        _BJ_UI.SetActive(true);


        _inGame = true;
        _inBJRound = false;
        _initialAction = true;

        _betPanel.SetActive(true);
        EventSystem.current.SetSelectedGameObject(_betPanelFocus);
        SetPlayerCoinsText();

        _spriteLookup = new Dictionary<string, Sprite>();
        foreach (var sprite in cardSprites)
        {
            string name = sprite.name.Trim().ToLower();
            Debug.Log($"[Sprite Load] {name}");
            if (!_spriteLookup.ContainsKey(name))
                _spriteLookup.Add(name, sprite);
        }

        StartCoroutine(BlackJackLoop());
    }

    private IEnumerator BlackJackLoop()
    {
        while (_inGame)
        {
            if (_initialAction && _inBJRound)
            {
                _initialAction = false;

                Card playerCard1 = PickRandomCard();
                _playerScore += playerCard1.GetValue();
                SpawnCardUI(playerCard1, _playerCardListContainer.transform);

                Card dealerCard1 = PickRandomCard();
                _dealerScore += dealerCard1.GetValue();
                SpawnCardUI(dealerCard1, _dealerCardListContainer.transform);

                Card playerCard2 = PickRandomCard();
                _playerScore += playerCard2.GetValue();
                SpawnCardUI(playerCard2, _playerCardListContainer.transform);

                _dealerSecretCard = PickRandomCard(); 

                SetPlayerScoreText();
                SetDealerScoreText();
            }

            if (_playerScore == 21)
            {
                GameWon(true);
                StartCoroutine(ShowResultAndClose(_winPanel));
                break;
            }

            if (_playerScore > 21)
            {
                GameOver();
                StartCoroutine(ShowResultAndClose(_losePanel));
                break;
            }

            if (_dealerScore == 21)
            {
                GameOver();
                StartCoroutine(ShowResultAndClose(_losePanel));
                break;
            }

            if (_dealerScore > 21)
            {
                GameWon(false);
                StartCoroutine(ShowResultAndClose(_winPanel));
                break;
            }

            if (_standFinished)
            {
                if (_playerScore > _dealerScore)
                {
                    GameWon(false);
                    StartCoroutine(ShowResultAndClose(_winPanel));
                }
                else
                {
                    GameOver();
                    StartCoroutine(ShowResultAndClose(_losePanel));
                }
                break;
            }

            yield return null;
        }
    }


    private void GameOver()
    {
        _gameOver = true;
        Debug.Log("game lost");
        StartCoroutine(ShowResultAndClose(_losePanel));
    }



    private void GameWon(bool blackjack)
    {
        _gameOver = true;
        if (blackjack)
            Player_Stats.Coins.Modify((int)(_currentBet * 2.5));
        else
            Player_Stats.Coins.Modify(_currentBet * 2);

        Debug.Log("game won");
        StartCoroutine(ShowResultAndClose(_winPanel));
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
                int value = name == "Ace" ? 11 : (int.TryParse(name, out int val) ? val : 10);
                deck.Add(new Card(color, name, value));
            }
        }

        return deck;
    }

    private void SpawnCardUI(Card card, Transform container)
    {
        string spriteName = $"{card.GetName().ToLower()}_{card.GetColor().ToLower()}"; 
        if (_spriteLookup.TryGetValue(spriteName, out Sprite sprite))
        {
            GameObject cardGO = Instantiate(cardPrefab, container);
            Image img = cardGO.GetComponent<Image>();
            img.sprite = sprite;
        }
        else
        {
            Debug.LogWarning("Brak sprite dla: " + spriteName);
        }
    }

    public void IncreaseBet()
    {
        if (_currentBet < _maxBet && _currentBet < Player_Stats.Coins.Value)
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

    public void SetPlayerScoreText() => _playerScoreText.text = "Player Score: " + _playerScore;
    public void SetDealerScoreText() => _dealerScoreText.text = "Dealer Score: " + _dealerScore;
    public void SetBetText(string text) => _currentBetText.text = text;
    public void SetPlayerCoinsText() => _playerCoinText.text = "Coins " + Player_Stats.Coins.Value;

    public void PlaceBet()
    {
        if (_currentBet > 0)
        {
            Player_Stats.Coins.Modify(-_currentBet);
            _betPanel.SetActive(false);
            _gamePanel.SetActive(true);
            EventSystem.current.SetSelectedGameObject(_gamePanelFocus);
            _inBJRound = true;
        }
    }

    public void Hit()
    {
        if (_gameOver || !_inBJRound) return;

        Card card = PickRandomCard();
        _playerScore += card.GetValue();
        SpawnCardUI(card, _playerCardListContainer.transform);
        SetPlayerScoreText();
    }

    public void Stand()
    {
        if (_gameOver || !_inBJRound) return;

        _dealerScore += _dealerSecretCard.GetValue();
        SpawnCardUI(_dealerSecretCard, _dealerCardListContainer.transform);
        SetDealerScoreText();

        while (_dealerScore < 17)
        {
            Card card = PickRandomCard();
            _dealerScore += card.GetValue();
            SpawnCardUI(card, _dealerCardListContainer.transform);
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
        Debug.Log("coins: " + Player_Stats.Coins.Value);
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
        
        foreach (Transform child in _playerCardListContainer.transform)
            Destroy(child.gameObject);
        foreach (Transform child in _dealerCardListContainer.transform)
            Destroy(child.gameObject);

        SetPlayerScoreText();
        SetDealerScoreText();
        SetBetText("0");
    }
    
    private IEnumerator ShowPanelTemporarily(GameObject panel)
    {
        yield return new WaitForSeconds(1f);
        panel.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        panel.SetActive(false);
    }
    
    private IEnumerator ShowResultAndClose(GameObject panel)
    {
        yield return new WaitForSeconds(1f);
        panel.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        panel.SetActive(false);
        CloseGame();
    }


}