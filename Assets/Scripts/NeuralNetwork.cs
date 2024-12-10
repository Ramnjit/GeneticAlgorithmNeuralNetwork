using System;
using UnityEngine;

public class NeuralNetwork : MonoBehaviour
{    
    private float m_bird;
    private float m_distance;
    private float m_lowerPipe;
    private float m_upperPipe;
    private float m_velocity;
    private Rigidbody2D m_rigidBody;

    private float PIPE_OFFSET_INDEX = 5.0f;
    private float PIPE_GAP_INDEX = 1.25f; 
    private float BIRD_OFFSET_INDEX = 5.0f;
    private int NEURAL_NETWORK_ROUNDED_INDEX = 6;

 
    private void Awake()
    {
        m_rigidBody = GetComponent<Rigidbody2D>();
    }

    public float GetNeuralNetworkResult(Dna p_Dna, Vector2 p_currentWallPosition)
    {
        CalculateInput(p_currentWallPosition);
        float p_neuralNetResult = CalculateNeuralNetworkResult(p_Dna);
        GameManager.m_neuralNetworkResultsList.Add(p_neuralNetResult.ToString());
        return p_neuralNetResult;
    }

    private void CalculateInput(Vector2 p_currentWallPos)
    {
        m_lowerPipe = (p_currentWallPos.y + PIPE_OFFSET_INDEX);
        m_upperPipe = (PIPE_OFFSET_INDEX - p_currentWallPos.y) - PIPE_GAP_INDEX;
        m_bird = transform.position.y + BIRD_OFFSET_INDEX;
        m_distance = p_currentWallPos.x - transform.position.x;
        m_velocity = m_rigidBody.velocity.y; 
    }

    private float CalculateNeuralNetworkResult(Dna p_Dna)
    {
        float weight1 = p_Dna.weights[0];
        float weight2 = p_Dna.weights[1];
        float weight3 = p_Dna.weights[2];
        float weight4 = p_Dna.weights[3];
        float weight5 = p_Dna.weights[4];

        float neuralNetworkResult =
            (float)Math.Round(((float)Math.Round((m_distance * weight1), NEURAL_NETWORK_ROUNDED_INDEX)
            + (float)Math.Round((m_lowerPipe * weight2), NEURAL_NETWORK_ROUNDED_INDEX)
            + (float)Math.Round((m_upperPipe * weight3), NEURAL_NETWORK_ROUNDED_INDEX)
            + (float)Math.Round((m_bird * weight4), NEURAL_NETWORK_ROUNDED_INDEX)
            + (float)Math.Round((m_velocity * weight5), NEURAL_NETWORK_ROUNDED_INDEX)), NEURAL_NETWORK_ROUNDED_INDEX);

        return neuralNetworkResult;
    }




}
