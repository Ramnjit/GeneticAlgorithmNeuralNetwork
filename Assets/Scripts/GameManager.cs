using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    public PipePopulate m_pipePopulate;
    private int m_numberOfBirds;
    private float m_bestFitness;
    private float m_currentFitness;

    private List<GameObject> m_walls = new List<GameObject>();   
    public static List<String> m_neuralNetworkResultsList = new List<String>();
    public static List<String> m_numberOfBirdsList = new List<String>();
    public static List<String> m_currentFitnessList = new List<String>();
    public static GameManager singleton;

    private void Awake()
    {
        if (singleton == null){
            singleton = this;
        }
    }

    void Update()
    {
        if (m_numberOfBirds <= 0)
        {
            EndTest();            
            return;
        }
        
        SetCurrentFitness(m_currentFitness + Time.deltaTime);
    }

    private void EndTest()
    {
        RecordCSV();
        m_walls.Clear();
        m_pipePopulate.StopBuilding();

        if (m_bestFitness < m_currentFitness)
        {
            m_bestFitness = m_currentFitness;
        }
        GeneticManger.singleton.StartRepopulition();
    }

    //Writes test data to a CSV
    private void RecordCSV()
    {
        string p_filePath = GetFilePath();
        System.IO.StreamWriter writer = new System.IO.StreamWriter(p_filePath);
        writer.WriteLine("birds, neural network result (jump), currentFitness");
        for (int i = 0; i < m_currentFitnessList.Count; i++)
        {
            writer.WriteLine(m_numberOfBirdsList[i] + "," + m_neuralNetworkResultsList[i] + "," + m_currentFitnessList[i]);
        }

        writer.Flush();
        writer.Close();
    }

    public void StartTest(int p_populationLength)
    {
        m_numberOfBirds = p_populationLength;
        m_pipePopulate.ResumeBuilding();
        SetCurrentFitness(0);        
    }

    public void AddWall(GameObject p_wall)
    {
        m_walls.Add(p_wall);
    }

    public GameObject GetPipeByIndex(int index)
    {
        return m_walls[index];
    }

    private string GetFilePath()
    {
        return Application.dataPath + "/CSV" + "output_csv_file.csv";
    }

    public void SetNumberOfBirds(int value)
    {
        m_numberOfBirds = value;
    }

    public int GetNumberOfBirds()
    {
        return m_numberOfBirds;
    }

    public void SetCurrentFitness(float value)
    {
        m_currentFitness = value;
    }

    public float GetCurrentFitness()
    {
        return m_currentFitness;
    }

    public void SetBestFitness(float value)
    {
        m_bestFitness = value;
    }

    public float GetBestFitness()
    {
        return m_bestFitness;
    }
}
