using UnityEngine;

public class PipePopulate : MonoBehaviour
{
    public GameObject pipePrefab;

    [SerializeField]
    private float m_populateRate, m_timer, m_instantiatePositionX, m_instantiatePositionLowerY, m_instantiatePositionUpperY;
    private bool m_stopPopulation = false;

    private void Update()
    {
        if (m_stopPopulation)
        {
            return;
        }

        PopulateOverTime();
    }

    void PopulateOverTime()
    {        
        m_timer += Time.deltaTime;            
        if (m_populateRate <= m_timer)
        {
            PopulatePipe();
            m_timer = 0;
        }
    }

    void PopulatePipe()    
    {
        GameObject p_wall = Instantiate(pipePrefab, new Vector3(m_instantiatePositionX, Random.Range(m_instantiatePositionLowerY, m_instantiatePositionUpperY), 0), new Quaternion(0, 0, 0, 0));       
        if(p_wall != null)
        {
            GameManager.singleton.AddWall(p_wall);
        }        
    }

    public void ResumeBuilding()
    {
        m_stopPopulation = false;
        m_timer = m_populateRate;
    }

    public void StopBuilding()
    {
        m_stopPopulation = true;
    }
}
