using System;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    [SerializeField]
    private float m_jumpPower;
    private Dna m_dna;
    private int m_currentPipeIndex = 0;
    private NeuralNetwork m_neuralNetBird;

    private float NEURAL_NETWORK_VALUE_INDEX = 0.5f;
    private float CURRENT_PIPE_OFFSET = 0.8f;


    private void Awake()
    {
        m_currentPipeIndex = 0;
        m_neuralNetBird = GetComponent<NeuralNetwork>();
    }

    public void SetDna(Dna dna)
    {
        this.m_dna = dna;
    }

    void Update()
    {
        Vector2 currentPipePosition = GetCurrentPipePos();

        float NeuralNetResult = m_neuralNetBird.GetNeuralNetworkResult(m_dna, currentPipePosition);

        //Mimics Human Input based on neural network result
        if (NeuralNetResult > NEURAL_NETWORK_VALUE_INDEX)
        {           
            Jump();
        }

    }

    private Vector2 GetCurrentPipePos()
    {
        GameObject currentPipe = GameManager.singleton.GetPipeByIndex(m_currentPipeIndex);

        Vector2 currentPipePos = currentPipe.transform.position;
        currentPipePos.x += CURRENT_PIPE_OFFSET;

        if (CheckIfCrossTheCurrentPipe(currentPipePos))
        {
            m_currentPipeIndex++;
            currentPipePos = currentPipe.transform.position;
            currentPipePos.x += CURRENT_PIPE_OFFSET;
        }

        return currentPipePos;
    }

    private bool CheckIfCrossTheCurrentPipe(Vector2 currentPipePos)
    {
        return currentPipePos.x - transform.position.x <= 0;
    }

    private void Jump()
    {
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        GetComponent<Rigidbody2D>().AddForce(Vector2.up * m_jumpPower);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            Death();
        }
    }

    private void Death()
    {
        int p_numberOfBirds = GameManager.singleton.GetNumberOfBirds();
        GameManager.singleton.SetNumberOfBirds(p_numberOfBirds - 1);
        m_dna.fitness = GameManager.singleton.GetCurrentFitness();
        Destroy(gameObject);
    }
}
