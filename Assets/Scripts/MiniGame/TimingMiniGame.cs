using UnityEngine;

public class TimingMiniGame : MonoBehaviour
{
    private bool isTouchingTarget = false;
    private bool gameEnded = false;

    [SerializeField] float speed;

    void Update()
    {
        if (gameEnded) return;

        transform.Translate(Vector2.right * (speed * Time.deltaTime));

        if (Input.GetKeyDown(KeyCode.Space))
        {
            gameEnded = true; 

            if (isTouchingTarget)
            {
                Debug.Log("Trafione w punkt!");
            }
            else
            {
                Debug.Log("Pudło!");
            }
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Target"))
        {
            isTouchingTarget = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Target"))
        {
            isTouchingTarget = false;
        }
    }
}