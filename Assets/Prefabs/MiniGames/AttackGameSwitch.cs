using UnityEngine;

public class AttackGameSwitch : MonoBehaviour
{
    [SerializeField] private GameObject attackGameObject;

    private void OnEnable()
    {
        attackGameObject.SetActive(true);
    }

    private void OnDisable()
    {
        attackGameObject.SetActive(false);
    }
}
