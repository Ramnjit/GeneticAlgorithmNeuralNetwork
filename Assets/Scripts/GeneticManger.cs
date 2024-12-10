using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneticManger : MonoBehaviour
{
    [SerializeField]
    private int m_populationLength, m_weightsLength;
        
    private int m_generationNumber;

    [SerializeField]
    private float m_mutationRate;

    public GameObject m_birdPrefab;
    private bool m_isRepopulition = true;
    private Dna[] m_population;
    private Dna[] m_bestPopulationSelect;

    public static GeneticManger singleton;

    private int BIRD_POSITION_OFFSET = -7;

    private void Awake()
    {
        if (singleton == null)
        {
            singleton = this;
        }
    }

    private void Start()
    {
        CreatePopulation();        
        GameManager.singleton.SetNumberOfBirds(m_populationLength);
        GameManager.singleton.SetCurrentFitness(0);
        m_isRepopulition = false;
    }

    // Creates the initial population of Birds and assigns them DNA and weights.
    private void CreatePopulation()
    {
        m_population = new Dna[m_populationLength];

        for (int i = 0; i < m_populationLength; i++)
        {
            m_population[i] = new Dna();
            m_population[i].weights = new float[m_weightsLength];

            for (int j = 0; j < m_weightsLength; j++)
            {
                m_population[i].weights[j] = UnityEngine.Random.Range(-1.0f, 1.0f);
            }

            m_population[i].fitness = 0;
            GameObject p_birdGameObject = Instantiate(m_birdPrefab, new Vector3(BIRD_POSITION_OFFSET, 0, 0), new Quaternion(0, 0, 0, 0));
            m_population[i].bird = p_birdGameObject;
            p_birdGameObject.GetComponent<Bird>().SetDna(m_population[i]);
        }
    }

    public void StartRepopulition()
    {
        if (m_isRepopulition)
        {
            return;
        }
        m_isRepopulition = true;
        StartCoroutine(RepopulateBirds());
    }

    // Repopulates with new generation of Bird Population, with DNA from parents and potentially new weights.
    IEnumerator RepopulateBirds()
    {
        yield return new WaitForSeconds(2f);

        SortPopulation();
        SelectBestPopulition();
        CrossOverTheBestPopulationSelection();
        FillBestPopulation();
        FillRemainingPopulation();
        Mutate();

        GameManager.singleton.StartTest(m_populationLength);
        yield return new WaitForSeconds(2f);
        InstantiateBirds();
        m_isRepopulition = false;
        m_generationNumber++;
       
    }

    // Sorts Birds by how long they survived (best fitness level)
    private void SortPopulation()
    {
        for (int i = 0; i < m_population.Length; i++){
            for (int j = i + 1; j < m_population.Length; j++){
                if (m_population[i].fitness < m_population[j].fitness){
                    Dna Temp = m_population[i];
                    m_population[i] = m_population[j];
                    m_population[j] = Temp;
                }
            }
        }
    }

    // Selects the top 25% of birds
    private void SelectBestPopulition()
    {
        m_bestPopulationSelect = new Dna[m_population.Length / 4];

        for (int i = 0; i < m_populationLength / 4; i++)
        {
            m_bestPopulationSelect[i] = new Dna();
            m_bestPopulationSelect[i] = m_population[i];
        }
    }

    //Algorithm to cross populations with weights from a pair of parents with the best fitness
    private void CrossOverTheBestPopulationSelection()
    {
        m_population = new Dna[m_populationLength];

        for (int i = 0; i < m_populationLength / 4; i++)
        {
            int a = UnityEngine.Random.Range(0, m_bestPopulationSelect.Length);

            m_population[i] = new Dna();
            m_population[i].weights = new float[m_weightsLength];

            int Mid = UnityEngine.Random.Range(1, m_weightsLength - 1);

            for (int j = 0; j < Mid; j++)
            {
                m_population[i].weights[j] = m_bestPopulationSelect[i].weights[j];
            }
            for (int j = Mid; j < m_weightsLength; j++)
            {
                m_population[i].weights[j] = m_bestPopulationSelect[a].weights[j];
            }
        }
    }

    // Fills population list with the best birds
    private void FillBestPopulation()
    {
        int index = 0;

        for (int i = m_populationLength / 4; i < m_populationLength / 2; i++)
        {
            m_population[i] = new Dna();

            m_population[i].weights = new float[m_bestPopulationSelect[0].weights.Length];

            for (int j = 0; j < m_weightsLength; j++)
            {
                m_population[i].weights[j] = m_bestPopulationSelect[index].weights[j];
            }
            index++;
        }
    }

    // Fills the rest of the population
    private void FillRemainingPopulation()
    {
        for (int i = m_populationLength / 2; i < m_populationLength; i++)
        {
            m_population[i] = new Dna();

            m_population[i].weights = new float[m_bestPopulationSelect[0].weights.Length];

            for (int j = 0; j < m_population[i].weights.Length; j++)
            {
                m_population[i].weights[j] = UnityEngine.Random.Range(-1.0f, 1.0f);
            }
        }
    }

    // Mutation algorithm which alters weights of birds randomly
    private void Mutate()
    {
        for (int i = 0; i < m_populationLength; i++)
        {
            Dna dna = m_population[i];
            for (int j = 0; j < m_weightsLength; j++)
            {
                if (UnityEngine.Random.Range(0.0f, 1.0f) < m_mutationRate)
                {
                    dna.weights[j] = UnityEngine.Random.Range(-1.0f, 1.0f);
                }
            }
        }
    }

    private void InstantiateBirds()
    {
        for (int i = 0; i < m_populationLength; i++)
        {
            GameObject prefab = Instantiate(m_birdPrefab, new Vector3(-7, 0, 0), new Quaternion(0, 0, 0, 0));
            m_population[i].bird = prefab;
            m_population[i].fitness = 0;
            prefab.GetComponent<Bird>().SetDna(m_population[i]);
        }
    }

    public int GetGenerationNumber()
    {
        return m_generationNumber;
    }
}
