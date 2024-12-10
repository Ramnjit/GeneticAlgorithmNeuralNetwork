using UnityEngine;

public class PipePrefab : MonoBehaviour
{
    [SerializeField]
    private float m_speed;

    private GameManager m_gameManger;
    private void Start()
    {
        m_gameManger = GameObject.FindGameObjectWithTag("GameManger").GetComponent<GameManager>();
    }

    void Update()
    {
        MovePipe();
        DestroyPipe();
    }

    private void MovePipe()
    {
        transform.Translate(Vector2.left * Time.deltaTime * m_speed);
    }

    private void DestroyPipe()
    {
        if (m_gameManger.GetNumberOfBirds() <= 0)
        {
            Destroy(gameObject);
        }
    }
}