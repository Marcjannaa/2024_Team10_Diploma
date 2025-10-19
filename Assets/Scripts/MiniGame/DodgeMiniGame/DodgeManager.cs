using UnityEngine;

public class DodgeManager : MonoBehaviour
{
    private float _timer;
    public float gameTime = 10f;
    
    private void OnEnable()
    {
        _timer = 0f;
    }

    private void Update()
    {
        _timer += Time.unscaledDeltaTime;
        if (_timer > gameTime)
        {
            Win();
        }
    }

    public static void Lose()
    {
        CombatManager.OnDodgeEnded(false);
    }
    
    public static void Win()
    {
        CombatManager.OnDodgeEnded(true);
    }
}
