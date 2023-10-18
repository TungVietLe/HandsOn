using UnityEngine;

public class NewBallAtStart : MonoBehaviour
{
    [SerializeField] Transform m_startPos;
    [SerializeField] GameObject m_ballPrefab;

    private Vector3 startPos;
    private void Awake()
    {
        startPos = m_startPos.position;
    }
    public void SpawnNewBall()
    {
        Instantiate(m_ballPrefab, startPos, Quaternion.identity);
    }
}
